using AutoMapper;
using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;
using DiceParadiceApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceParadiceApi.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IMapper _mapper;
    private readonly IAuthenticationManager _authenticationManager;

    public AuthenticationController(IAuthenticationManager authenticationManager, IRepositoryManager repository,
        ILogger<AuthenticationController> logger, IMapper mapper)
    {
        _authenticationManager = authenticationManager;
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserForRegistrationDto userForRegistration)
    {
        _repository.User.CreateUser(userForRegistration);
        await _repository.SaveAsync();
        return StatusCode(201);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(UserForAuthenticationDto user)
    {
        if (!await _authenticationManager.ValidateUser(user))
            return Unauthorized();
        
        var tokenDto = await _authenticationManager.CreateToken(populateExp: true);
        return Ok(tokenDto);
    }
}