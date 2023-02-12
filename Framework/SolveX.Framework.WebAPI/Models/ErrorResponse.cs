
namespace SolveX.Framework.WebAPI.Models;

public class ErrorResponse
{
    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Htttp response code
    /// </summary>
    public int Code { get; set; }
}
