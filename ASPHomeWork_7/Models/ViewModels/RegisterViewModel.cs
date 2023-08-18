using System.ComponentModel.DataAnnotations;

namespace ASPHomeWork_7.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [MinLength(5)]
    public string Login { get; set; }

    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
