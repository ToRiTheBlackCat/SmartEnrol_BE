using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AccountSignupModel
{
    [Required]
    public string? AccountName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? ConfirmPassword { get; set; }
}
