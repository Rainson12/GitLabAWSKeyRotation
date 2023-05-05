using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Authentication;
using Microsoft.AspNet.Identity;

namespace GitLabAWSKeyRotation.Application.Authentication.Commands.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<string>>
    {
        IApplicationRepository _applicationRepository { get; set; }
        IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(IJwtTokenGenerator tokenGenerator, IApplicationRepository applicationRepository)
        {
            _jwtTokenGenerator = tokenGenerator;
            _applicationRepository = applicationRepository;
        }

        public async Task<ErrorOr<string>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.Delay(new Random().Next(0, 100)); // Random delay to prevent timing attacks
            var app = await _applicationRepository.Get();
            if(app == null)
                return Domain.Common.Errors.Authentication.NotConfigured;

            var hasher = new PasswordHasher();
            var verificationResult = hasher.VerifyHashedPassword(app.AuthKey, query.password);
            if (verificationResult == PasswordVerificationResult.Success)
            {
                return _jwtTokenGenerator.GenerateToken();
            }
            return Domain.Common.Errors.Authentication.IncorrectAuthKey;
        }
    }
}
