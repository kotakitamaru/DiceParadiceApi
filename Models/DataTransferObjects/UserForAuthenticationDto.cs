﻿namespace DiceParadiceApi.Models.DataTransferObjects;

public class UserForAuthenticationDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}