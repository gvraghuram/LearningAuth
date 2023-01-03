using CSharpFunctionalExtensions;
using MediatR;
using NASSOne.Platform.Application.Dtos.Requests;
using NASSOne.Platform.Application.Dtos.Responses;
using NASSOne.Platform.Core.Errors;

namespace NASSOne.Platform.Application.CQRS.Commands
{
    public class CreateApplicationCommand : IRequest<Result<ApplicationResponseDto, Error>>
    {
        public ApplicationRequestDto ApplicationRequestDto { get; }

        public CreateApplicationCommand(ApplicationRequestDto applicationRequestDto) => applicationRequestDto = applicationRequestDto;
    }
}
