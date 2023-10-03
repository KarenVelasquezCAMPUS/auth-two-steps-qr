using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private readonly IAuthenticationService _authenticationService;
    public UserController
    (
        ILogger<UserController> logger,
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService
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
            _Logger.LogError(ex.Message);
            return BadRequest("some wrong");
        }                        
    }

    /* [HttpGet("Verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]    
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult> Verify([FromBody] AuthVerifyCodeDto data)
    {        
        try
        {
            User u = await _UnitOfWork.Users.FindFirst(x => x.Id == data.Id);
            if(u.TwoFactorSecret == null){
                throw new ArgumentNullException(u.TwoFactorSecret);
            }
            var isVerified = _Auth.VerifyCode(u.TwoFactorSecret, data.Code);            

            if(isVerified == true){
                return Ok("authenticated!!");
            }

            return Unauthorized();
        }
        catch (Exception ex){
            _Logger.LogError(ex.Message);
            return BadRequest("some wrong");
        }                        
    } */
}
