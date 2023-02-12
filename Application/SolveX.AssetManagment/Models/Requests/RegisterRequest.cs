using System.ComponentModel.DataAnnotations;

namespace SolveX.AssetManagment.Models.Requests;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Sets the users role to admin if true
    /// </summary>
    public bool isAdmin { get; set; }
}
