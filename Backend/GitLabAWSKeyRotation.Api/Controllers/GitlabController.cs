using GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitLabAWSKeyRotation.Api.Controllers
{
    [Route("Gitlab")]
    public class GitLabController : ApiController
    {
        private readonly ISender _mediator;

        public GitLabController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Rotation/Register")]
        public async Task<IActionResult> RegisterRotation(Contracts.Gitlab.RegisterRotationRequest request)
        {
            var command = new RegisterRotationCommand(request.environment, request.accessKeyIdVariableName, request.accessSecretVariableName, request.CodeRepositoryId, request.IamId, request.AwsAccountId);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("Repository/Register")]
        public async Task<IActionResult> RegisterRepository(Contracts.Gitlab.RegisterRepositoryRequest request)
        {
            var command = new RegisterRepositoryCommand(request.url, request.identifier, request.token);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }
    }
}
