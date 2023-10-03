using Domain.Entities;

namespace Domain.Interfaces;
public interface IAuthenticationService
{
    byte[] CreateQR(ref User user);
    bool VerifyCode(string secret, string code);
}