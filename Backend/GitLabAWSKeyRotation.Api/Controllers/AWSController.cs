using GitLabAWSKeyRotation.Application.AWS.Account.Commands.Register;
using GitLabAWSKeyRotation.Application.AWS.IAM.Commands.Register;
using GitLabAWSKeyRotation.Application.AWS.IAM.Queries;
using GitLabAWSKeyRotation.Contracts.AWS.IAM;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitLabAWSKeyRotation.Api.Controllers
{
    [Route("AWS")]
    public class AWSController : ApiController
    {
        private readonly ISender _mediator;

        public AWSController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Account/Register")]
        public async Task<IActionResult> RegisterAccount(Contracts.AWS.Account.RegisterAccountRequest request)
        {
            var command = new RegisterAccountCommand(request.displayName, request.accountId);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("Accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var command = new AccountsQuery();
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }


        [HttpPost]
        [Route("IAM/Register")]
        public async Task<IActionResult> RegisterIAM(RegisterIamRequest request)
        {
            var command = new RegisterIamCommand(request.accountId, request.name, request.accessKeyId, request.accessSecret, request.rotationInDays);
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("Accounts/{accountId}/IAMs")]
        public async Task<IActionResult> GetIAMs([FromRoute] Guid accountId)
        {
            var command = new IAMsQuery(Domain.AWS.ValueObjects.AccountId.Create(accountId));
            var response = await _mediator.Send(command);

            // Todo map responses
            return response.Match(
                response => Ok(response),
                errors => Problem(errors));
        }
    }
}
