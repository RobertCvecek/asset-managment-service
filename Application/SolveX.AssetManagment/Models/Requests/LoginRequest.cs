namespace SolveX.AssetManagment.Models.Requests;

public class LoginRequest
{
    /// <summary>
    /// Users unique username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Users password
    /// </summary>
    public string Password { get; set; }
}
