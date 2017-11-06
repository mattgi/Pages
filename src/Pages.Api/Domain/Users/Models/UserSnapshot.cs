using EventFlow.Snapshots;

namespace Pages.Api.Domain.Users
{
  [SnapshotVersion("User", 1)]
  public class UserSnapshot : ISnapshot
  {
    public UserSnapshot()
    {
    }
  }
}