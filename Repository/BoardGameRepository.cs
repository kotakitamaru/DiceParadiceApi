using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;
using DiceParadiceApi.Models.RequestFeatures;
using DiceParadiceApi.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DiceParadiceApi.Repository;

public class BoardGameRepository : RepositoryBase<BoardGame>, IBoardGameRepository
{
    public BoardGameRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<BoardGame>> GetAllAsync(BoardGamesParameters boardGamesParameters ,bool trackChanges = false)
    {
        var boardGames = await FindAll(trackChanges)
            .FilterByPrice(boardGamesParameters.MinPrice,boardGamesParameters.MaxPrice)
            .Search(boardGamesParameters.SearchTerm)
            .Sort(boardGamesParameters.OrderBy)
            .ToListAsync();
        
        return PagedList<BoardGame>
            .ToPagedList(boardGames, boardGamesParameters.PageNumber, boardGamesParameters.PageSize);
    }
    public async Task<BoardGame> GetByIdAsync(Guid id, bool trackChanges = false) =>
        await FindByCondition(x => x.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<BoardGame>> GetByIdsAsync(IEnumerable<Guid> ids,bool  trackChanges = false) => 
        await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

    public void Add(BoardGame boardGame)
    {
        Create(boardGame);
    }

    public new void Delete(BoardGame boardGame) => base.Delete(boardGame);
    
    
}