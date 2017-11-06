using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;
using Newtonsoft.Json;

namespace Pages.Api.Domain.Users.Commands
{
  [CommandVersion("UpdateUserEmail", 1)]
  public class UpdateUserEmailCommand : Command<UserAggregate, UserId>
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }


    [JsonConstructor]
    public UpdateUserEmailCommand(UserId aggregateId) : base(aggregateId)
    {
    }
}

  public class UpdateUserEmailCommandHandler : CommandHandler<UserAggregate, UserId, UpdateUserEmailCommand>
  {
    public override Task ExecuteAsync(UserAggregate aggregate, UpdateUserEmailCommand command, CancellationToken cancellationToken)
    {
      aggregate.UpdateEmail(command.Email);
      return Task.FromResult(0);
    }
  }
}
