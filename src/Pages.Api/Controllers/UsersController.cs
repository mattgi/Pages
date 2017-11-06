using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using Microsoft.AspNetCore.Mvc;
using Pages.Api.Domain.Users;
using Pages.Api.Domain.Users.Commands;
using Pages.Api.Infrastructure.Reflection;
using Pages.Api.Models.Write;

namespace Pages.Api.Controllers
{
  [Route("[controller]")]
  public class UsersController : Controller
  {
    private readonly ICommandBus _commandBus;

    public UsersController(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
      
      return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]CreateUserWriteModel request)
    {
      // Map a request into a Command (using some borrowed code from Squidex to auto map between models
      var command = SimpleMapper.Map(request, new CreateUserCommand(UserId.New));

      // publish the command to the bus
      await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

      // return 204/
      return Ok();
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Post(string id, [FromBody]UpdateUserEmailWriteModel request)
    {
      // Map a request into a Command (using some borrowed code from Squidex to auto map between models
      var command = SimpleMapper.Map(request, new UpdateUserEmailCommand(UserId.With(id)));

      // publish the command to the bus
      await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

      // return 204/
      return Ok();
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
