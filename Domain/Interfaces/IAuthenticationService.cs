using Domain.Entities;

namespace Domain.Interfaces;
public interface IAuthenticationService
{
    byte[] CreateQR(ref User u);
    bool VerifyCode(string secret, string code);
}