namespace DiceParadiceApi.Repository;

public interface IRepositoryManager
{
    IBoardGameRepository BoardGame { get; }
    Task SaveAsync();
}