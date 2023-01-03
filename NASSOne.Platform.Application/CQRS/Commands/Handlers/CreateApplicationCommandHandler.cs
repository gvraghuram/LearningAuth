using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using NASSOne.Platform.Application.CQRS.Commands.Handlers;
using NASSOne.Platform.Application.Dtos.Requests;
using NASSOne.Platform.Application.Dtos.Responses;
using NASSOne.Platform.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASSOne.Platform.Application.CQRS.Commands.Auth.Handlers
{
    public class CreateApplicationCommandHandler : HandlerBase<ApplicationResponseDto>,
        IRequestHandler<CreateApplicationCommand, Result<ApplicationResponseDto, Error>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateApplicationCommandHandler> _logger;

        public CreateApplicationCommandHandler(IMapper mapper, ILogger<CreateApplicationCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ApplicationResponseDto, Error>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting {GetType().Name}");

                var applicationRequestDto = request.ApplicationRequestDto;

                //Aggregate
                //Validate

                return ToResult(new ApplicationResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error during CreateApplicationCommandHandler.Handle(). {ex.GetType().Name}: {ex.Message}");
                return Result.Failure<ApplicationResponseDto, Error>(Errors.Application.CreateApplicationError());
            }
        }
    }
}
