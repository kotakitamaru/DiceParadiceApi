using DiceParadiceApi.Models;

namespace DiceParadiceApi.Repository;

public class RepositoryManager : IRepositoryManager
{
    private RepositoryContext _repositoryContext;
    private IBoardGameRepository _boardGameRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
    public IBoardGameRepository BoardGame
    {
        get
        {
            if(_boardGameRepository == null)
                _boardGameRepository = new BoardGameRepository(_repositoryContext);
            return _boardGameRepository;
        }
    }

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
