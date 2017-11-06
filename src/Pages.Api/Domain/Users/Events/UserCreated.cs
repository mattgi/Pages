using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Pages.Api.Domain.Users.Events
{
  [EventVersion("UserCreated", 1)]
  public class UserCreated : AggregateEvent<UserAggregate, UserId>
  {
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public UserCreated(string email, string firstName, string lastName, string password)
    {
      Email = email;
      FirstName = firstName;
      LastName = lastName;
      Password = password;
    }
  }
}
