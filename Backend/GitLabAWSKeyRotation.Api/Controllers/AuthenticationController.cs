using GitLabAWSKeyRotation.Application.Authentication.Commands.Queries;
using GitLabAWSKeyRotation.Application.Authentication.Commands.Setup;
using GitLabAWSKeyRotation.Application.AWS.Account.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitLabAWSKeyRotation.Api.Controllers
{
    [Route("Authenticate")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("Setup")]
        public async Task<IActionResult> RegisterAccount(Contracts.Authentication.SetupRequest request)
        {
            var command = new SetupAuthenticationCommand(request.password);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("IsConfigured")]
        public async Task<IActionResult> IsConfigured()
        {
            var command = new IsConfiguredQuery();
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(Contracts.Authentication.AuthenticateRequest request)
        {
            var command = new LoginQuery(request.password);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }
    }
}
