using MediatR;
using Microsoft.AspNetCore.Mvc;
using NASSOne.Platform.Application.CQRS.Commands;
using NASSOne.Platform.Application.Dtos.Requests;
using NASSOne.Platform.Application.Dtos.Responses;
using NASSOne.Platform.Core.Errors;
using System.Net;

namespace NASSOne.Platform.Authorization.API.Controllers
{
    public class ApplicationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicationController> _logger;

        public ApplicationController(IMediator mediator, ILogger<ApplicationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Produces("application/json", "application/problem+json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApplicationResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ApplicationRequestDto applicationRequestDto)
        {
            _logger.LogInformation($"Create application with name: {applicationRequestDto.Name}");

            var result = await _mediator.Send(new CreateApplicationCommand(applicationRequestDto));

            return FromResult(result, HttpStatusCode.Created);
        }
    }
}
