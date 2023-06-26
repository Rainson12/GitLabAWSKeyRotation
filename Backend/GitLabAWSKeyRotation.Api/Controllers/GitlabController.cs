using GitLabApiClient.Models.Oauth.Requests;
using GitLabAWSKeyRotation.Application.Gilab.Queries;
using GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
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
            var command = new RegisterRotationCommand(request.environment, request.accessKeyIdVariableName, request.accessSecretVariableName, request.GitlabAccessTokenId, request.CodeRepositoryId, request.IamId, request.AwsAccountId);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }


        [HttpPost]
        [Route("AccessToken/Register")]
        public async Task<IActionResult> RegisterAccessToken(Contracts.Gitlab.RegisterAccessTokenRequest request)
        {
            var command = new RegisterAccessTokenCommand(request.name, request.token);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("AccessToken/Scan")]
        public async Task<IActionResult> ScanAndRegister(Contracts.Gitlab.ScanRequest request)
        {
            var command = new ScanCommand(request.name, request.token, request.rotationIntervalInDays);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }


        [HttpPost]
        [Route("AccessToken/{accessTokenId}/RegisterRepository")]
        public async Task<IActionResult> RegisterRepository([FromQuery] Guid accessTokenId, [FromBody]Contracts.Gitlab.RegisterRepositoryRequest request)
        {
            var command = new RegisterRepositoryCommand(accessTokenId, request.url, request.name);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("AccessTokens")]
        public async Task<IActionResult> GetAccessTokens()
        {
            var command = new AccessTokensQuery();
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("AccessToken/{accessTokenId}/CodeRepositories")]
        public async Task<IActionResult> GetCodeRepositories([FromRoute] Guid accessTokenId)
        {
            var command = new CodeRepositoriesQuery(AccessTokenId.Create(accessTokenId));
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("AccessToken/{accessTokenId}/CodeRepository/{codeRepositoryId}/Rotations")]
        public async Task<IActionResult> GetRotations([FromRoute] Guid accessTokenId, [FromRoute] Guid codeRepositoryId)
        {
            var command = new RotationsQuery(CodeRepositoryId.Create(codeRepositoryId));
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }
    }
}
