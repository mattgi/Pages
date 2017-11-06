using System.ComponentModel.DataAnnotations;

namespace Pages.Api.Models.Write
{
  public sealed class UpdateUserEmailWriteModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}