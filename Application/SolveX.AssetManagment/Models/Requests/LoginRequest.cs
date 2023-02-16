using System.ComponentModel.DataAnnotations;

namespace SolveX.AssetManagment.Models.Requests;

public class LoginRequest
{
    /// <summary>
    /// Users unique user name
    /// </summary>
    [Required]
    public string Username { get; set; } = String.Empty;

    /// <summary>
    /// Users password
    /// </summary>
    [Required]
    public string Password { get; set; } = String.Empty;
}
