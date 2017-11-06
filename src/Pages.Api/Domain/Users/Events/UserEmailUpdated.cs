using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Pages.Api.Domain.Users.Events
{
  [EventVersion("UserEmailUpdated", 1)]
  public class UserEmailUpdated : AggregateEvent<UserAggregate, UserId>
  {
    public string Email { get; set; }

    public UserEmailUpdated(string email)
    {
      Email = email;
    }
  }
}
