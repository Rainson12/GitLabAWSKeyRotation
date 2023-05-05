using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLabAWSKeyRotation.Contracts.Authentication
{
    public record AuthenticateRequest(string password);
}
