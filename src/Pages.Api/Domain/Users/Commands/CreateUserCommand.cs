using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;
using Newtonsoft.Json;

namespace Pages.Api.Domain.Users.Commands
{
  [CommandVersion("CreateUser", 1)]
  public class CreateUserCommand : Command<UserAggregate, UserId>
  {
      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      public string FirstName { get; set; }

      [Required]
      public string LastName { get; set; }

      [Required]
      public string Password { get; set; }

    public CreateUserCommand(UserId aggregateId) : this(aggregateId, CommandId.New)
    {
    }

    public CreateUserCommand(UserId aggregateId, ISourceId sourceId) : base(aggregateId, sourceId)
    {
    }

    [JsonConstructor]
    public CreateUserCommand(UserId aggregateId, SourceId sourceId) : base(aggregateId, sourceId)
    {
    }
  }

  public class CreateUserCommandHandler : CommandHandler<UserAggregate, UserId, CreateUserCommand>
  {
    public override Task ExecuteAsync(UserAggregate aggregate, CreateUserCommand command, CancellationToken cancellationToken)
    {
      aggregate.Create(command.Email, command.FirstName, command.LastName, command.Password);
      return Task.FromResult(0);
    }
  }
}