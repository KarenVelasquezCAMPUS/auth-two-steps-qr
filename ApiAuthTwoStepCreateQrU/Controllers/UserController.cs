using ApiAuthTwoStepCreateQrU.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuthTwoStepCreateQrU.Controllers;
public class UserController : BaseApiController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Domain.Interfaces.IAuthenticationService _authenticationService;
    public UserController
    (
        ILogger<UserController> logger,
        IUnitOfWork unitOfWork,
        Domain.Interfaces.IAuthenticationService authenticationService
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
    }

    [HttpGet("QR/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult> GetQR(long id)
    {        
        try{
            User u = await _unitOfWork.Users.FindFirst(p => p.UserId == id);
            byte[] QR = _authenticationService.CreateQR(ref u);            

            _unitOfWork.Users.Update(u);
            await _unitOfWork.SaveChanges();
            return File(QR,"image/png");
        }
        catch (Exception ex){
            _logger.LogError(ex.Message);
            return BadRequest("some wrong");
        }                        
    }

    [HttpGet("Verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]    
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult> Verify([FromBody] AuthenticationVerifyCodeDto data)
    {        
        try
        {
            User u = await _unitOfWork.Users.FindFirst(p => p.UserId == data.Id);
            if(u.TwoStepSecret == null)
            {
                throw new ArgumentNullException(u.TwoStepSecret);
            }
            var isVerified = _authenticationService.VerifyCode(u.TwoStepSecret, data.Code);            

            if(isVerified == true)
            {
                return Ok("authenticated!!");
            }

            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest("some wrong");
        }                        
    }
}
