using DiceParadiceApi.Repository.Extensions;

namespace DiceParadiceApi.Repository;

public interface IRepositoryManager
{
    IBoardGameRepository BoardGame { get; }
    IUserRepository User { get; }
    Task SaveAsync();
}