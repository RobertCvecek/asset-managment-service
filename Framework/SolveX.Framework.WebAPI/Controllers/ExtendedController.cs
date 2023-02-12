using Microsoft.AspNetCore.Mvc;
using SolveX.Framework.Domain.Exceptions;
using System.Security.Claims;

namespace SolveX.Framework.WebAPI.Controllers;

/// <summary>
/// Adds methods that provide manipulation over JWT tokens data
/// </summary>
public abstract class AuthenticationController : ControllerBase
{
    /// <summary>
    /// Fetches user identifier from jwt token
    /// </summary>
    /// <returns>User identifier</returns>
    /// <exception cref="UnauthorizedException">Exception is throw if the identifier was incorrect</exception>
    protected int GetUserId() => Int32.Parse((User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedException("Incorect user identifier in token")).Value);
}
