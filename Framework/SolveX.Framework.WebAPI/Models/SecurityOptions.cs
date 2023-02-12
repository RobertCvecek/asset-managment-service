namespace SolveX.Framework.WebAPI.Models;

public class SecurityOptions
{
    /// <summary>
    /// Secuity key used for signature validation
    /// </summary>
    public string IssuerSigningKey { get; set; } = String.Empty;

    /// <summary>
    /// Token expiry in minutes
    /// </summary>
    public int Expiry { get; set; }
}