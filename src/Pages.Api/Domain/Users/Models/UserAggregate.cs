using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;
using Pages.Api.Domain.Users.Events;

namespace Pages.Api.Domain.Users
{
  [AggregateName("User")]
  public class UserAggregate : AggregateRoot<UserAggregate, UserId> //, UserSnapshot>
  {
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    //public const int SnapshotEveryVersion = 100;

    //public IReadOnlyCollection<UserSnapshotVersion> SnapshotVersions { get; private set; } = new UserSnapshotVersion[] { };

    //public UserAggregate(UserId id, ISnapshotStrategy snapshotStrategy) : base(id, snapshotStrategy)
    //{
    //}

    public UserAggregate(UserId id) : base(id)
    {
      
    }

    public void Apply(UserCreated vent) {
      Email = vent.Email;
      FirstName = vent.FirstName;
      LastName = vent.LastName;
      Password = vent.Password;
    }

    public void Apply(UserEmailUpdated vent)
    {
      Email = vent.Email;
    }

    public void Create(string email, string firstName, string lastName, string password) {
      //Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);
      Emit(new UserCreated(email, firstName, lastName, password));
    }

    public void UpdateEmail(string email)
    {
      Emit(new UserEmailUpdated(email));
    }

    //protected override Task<UserSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    //{
    //  return Task.FromResult(new UserSnapshot());
    //}

    //protected override Task LoadSnapshotAsync(UserSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    //{
    //  return Task.FromResult(0);
    //}
  }
}