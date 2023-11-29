using AutoMapper;
using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;

namespace DiceParadiceApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BoardGame, BoardGameDto>();
        CreateMap<BoardGameDto, BoardGame>();
        CreateMap<BoardGameForCreationDto, BoardGame >();
        CreateMap<BoardGameForUpdateDto, BoardGame >();
        CreateMap<UserForRegistrationDto, User>();
    }
}