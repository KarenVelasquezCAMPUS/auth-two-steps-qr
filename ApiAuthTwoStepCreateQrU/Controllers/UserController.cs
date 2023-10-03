using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace ApiAuthTwoStepCreateQrU.Controllers;
public class UserController : BaseApiController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IAuthService _Auth;
    public UserController
    (
        ILogger<UserController> logger,
        IUnitOfWork unitOfWork //,
        //IAuthService auth
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        //_Auth = auth;
    }

    /* [HttpGet("QR/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult> GetQR(long id)
    {        
        try{
            User u = await _UnitOfWork.Users.FindFirst(x => x.Id == id);
            byte[] QR = _Auth.CreateQR(ref u);            

            _UnitOfWork.Users.Update(u);
            await _UnitOfWork.SaveChanges();
            return File(QR,"image/png");
        }
        catch (Exception ex){
            _Logger.LogError(ex.Message);
            return BadRequest("some wrong");
        }                        
    } */

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
