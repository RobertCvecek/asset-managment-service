using Microsoft.AspNetCore.Identity;
using SolveX.Business.Users.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace SolveX.Business.Users.Domain.MicroServices;

public class UserPasswordHasher : IPasswordHasher<UserModel>
{
    private readonly byte[] salt = Encoding.UTF8.GetBytes("wjglvz2aK5Te");

    public string HashPassword(UserModel user, string password)
    {
        byte[] hash = SHA256
            .Create()
            .ComputeHash(
                Encoding.UTF8.GetBytes(password)
                .Concat(salt)
                .ToArray()
            );

        StringBuilder builder = new();

        foreach (byte b in hash)
        {
            builder.Append(b.ToString("x2").ToLower());
        }

        return builder.ToString();
    }

    public PasswordVerificationResult VerifyHashedPassword(UserModel user, string hashedPassword, string providedPassword)
    {
        return string.Equals(hashedPassword, HashPassword(user, providedPassword), StringComparison.OrdinalIgnoreCase)
            ? PasswordVerificationResult.SuccessRehashNeeded
            : PasswordVerificationResult.Failed;
    }
}
