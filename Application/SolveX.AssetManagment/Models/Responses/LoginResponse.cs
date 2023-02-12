namespace SolveX.AssetManagment.Models.Responses;

public class LoginResponse
{
    /// <summary>
    /// JWT token for user
    /// </summary>
    public string Token { get; set; } = String.Empty;
}
