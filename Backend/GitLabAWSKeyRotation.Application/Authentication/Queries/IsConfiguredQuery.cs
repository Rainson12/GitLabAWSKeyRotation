using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLabAWSKeyRotation.Application.Authentication.Commands.Queries
{
    public record IsConfiguredQuery () : IRequest<ErrorOr<bool>>;
}
