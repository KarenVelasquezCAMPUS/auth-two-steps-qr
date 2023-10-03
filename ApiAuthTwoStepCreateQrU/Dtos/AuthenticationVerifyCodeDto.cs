using System.ComponentModel.DataAnnotations;

namespace ApiAuthTwoStepCreateQrU.Dtos;
public class AuthenticationVerifyCodeDto
{
    [Required]
    public string Code { get; set; } = String.Empty; //""

    [Required]
    public long Id { get; set; }
}