using AutoMapper;
using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace DiceParadiceApi.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController: ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IMapper _mapper;
    public AuthenticationController (ILogger<AuthenticationController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

}