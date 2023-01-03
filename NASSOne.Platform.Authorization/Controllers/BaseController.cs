using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NASSOne.Platform.Core.Errors;
using System.Net;

namespace NASSOne.Platform.Authorization.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult FromResult<T>(Result<T, Error> result,
            HttpStatusCode successStatusCode = HttpStatusCode.OK, string identifier = null)
            where T : class
        {
            if (result.IsSuccess)
            {
                return ActionResultFromStatusCode(successStatusCode, result.Value, identifier);
            }

            return BadRequest(result.Error);
        }

        private IActionResult ActionResultFromStatusCode<T>(HttpStatusCode statusCode, T value = null,
            string identifier = null)
            where T : class
        {
            return statusCode switch
            {
                HttpStatusCode.OK => value != null ? (IActionResult)Ok(value) : Ok(),
                HttpStatusCode.Created => Created(new Uri($"{Request.GetDisplayUrl()}/{identifier}"), value),
                HttpStatusCode.Accepted => value != null ? (IActionResult)Accepted(value) : Accepted(),
                HttpStatusCode.NoContent => NoContent(),
                _ => throw new ArgumentException("Unsupported Status Code", nameof(statusCode))
            };
        }
    }
}
