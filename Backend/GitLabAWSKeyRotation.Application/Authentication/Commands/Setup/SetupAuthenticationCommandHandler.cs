using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Authentication;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNet.Identity;

namespace GitLabAWSKeyRotation.Application.Authentication.Commands.Setup
{
    public class SetupAuthenticationCommandHandler : IRequestHandler<SetupAuthenticationCommand, ErrorOr<string>>
    {
        IApplicationRepository _applicationRepository { get; set; }
        IJwtTokenGenerator _jwtTokenGenerator;
        public SetupAuthenticationCommandHandler(IJwtTokenGenerator tokenGenerator, IApplicationRepository applicationRepository)
        {
            _jwtTokenGenerator = tokenGenerator;
            _applicationRepository = applicationRepository;
        }

        public async Task<ErrorOr<string>> Handle(SetupAuthenticationCommand command, CancellationToken cancellationToken)
        {
            var app = await _applicationRepository.Get();
            if (app == null)
            {
                var hasher = new PasswordHasher();
                var hash = hasher.HashPassword(command.password);
                app = Domain.Application.Application.Create(hash);
                await _applicationRepository.Create(app);

                return _jwtTokenGenerator.GenerateToken();
            }
            return Domain.Common.Errors.Authentication.AlreadyConfigured;
        }
    }
}
