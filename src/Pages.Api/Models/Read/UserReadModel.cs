using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;
using EventFlow.ReadStores;
using Pages.Api.Domain.Users;
using Pages.Api.Domain.Users.Events;

namespace Pages.Api.Models.Read
{
  [MongoDbCollectionName("users")]
  public class UserReadModel : IMongoDbReadModel, IAmReadModelFor<UserAggregate, UserId, UserCreated>, IAmReadModelFor<UserAggregate, UserId, UserEmailUpdated>
  {
    public string _id { get; set; }

    public long? _version { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserCreated> domainEvent)
    {
      _id = domainEvent.AggregateIdentity.Value;
      Email = domainEvent.AggregateEvent.Email;
      FirstName = domainEvent.AggregateEvent.FirstName;
      LastName = domainEvent.AggregateEvent.LastName;
    }

    public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserEmailUpdated> domainEvent)
    {
      _id = domainEvent.AggregateIdentity.Value;
      Email = domainEvent.AggregateEvent.Email;
    }
  }
}
