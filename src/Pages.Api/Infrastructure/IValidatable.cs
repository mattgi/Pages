using System;
using System.Collections.Generic;

namespace Pages.Api.Infrastructure
{
  public interface IValidatable
  {
    void Validate(IList<ValidationError> errors);
  }
}
