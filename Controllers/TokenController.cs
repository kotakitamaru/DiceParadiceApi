using DiceParadiceApi.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace DiceParadiceApi.Controllers;
[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private IAuthenticationManager _authenticationManager;

    public TokenController(IAuthenticationManager authenticationManager)
    {
        _authenticationManager = authenticationManager;
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _authenticationManager.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}