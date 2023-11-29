using Azure;
using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;
using DiceParadiceApi.Models.RequestFeatures;

namespace DiceParadiceApi.Repository;

public interface IBoardGameRepository
{
    Task<PagedList<BoardGame>> GetAllAsync(BoardGamesParameters boardGamesParameters,bool trackChanges = false);
    Task<BoardGame> GetByIdAsync(Guid id, bool trackChanges = false);
    Task<IEnumerable<BoardGame>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false);
    void Add(BoardGame boardGame);
    void Delete(BoardGame boardGame);
}