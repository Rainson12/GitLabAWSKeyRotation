namespace GitLabAWSKeyRotation.Domain.Common.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {

        }

#pragma warning disable CS8618
        public AggregateRoot()
        {

        }
#pragma warning restore CS8618
    }
}
