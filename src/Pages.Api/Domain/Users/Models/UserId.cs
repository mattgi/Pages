using EventFlow.Core;

namespace Pages.Api.Domain.Users
{
  public class UserId : Identity<UserId>
  {
    public UserId(string value) : base(value)
    {
    }
  }
}