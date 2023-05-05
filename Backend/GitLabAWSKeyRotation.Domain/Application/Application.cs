using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GitLabAWSKeyRotation.Domain.Application
{
    public sealed class Application : AggregateRoot<ValueObjects.ApplicationId>
    {
        private Application(ValueObjects.ApplicationId id, string authKey) : base(id)
        {
            AuthKey = authKey;
        }

        public string AuthKey { get; set; }

        public static Application Create(string authKey)
        {
            return new(ValueObjects.ApplicationId.CreateUnique(), authKey);
        }

#pragma warning disable CS8618
        public Application(string authKey)
        {
            AuthKey = authKey;
        }
#pragma warning restore CS8618
    }
}
