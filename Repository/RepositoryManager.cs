using DiceParadiceApi.Models;
using DiceParadiceApi.Repository.Extensions;

namespace DiceParadiceApi.Repository;

public class RepositoryManager : IRepositoryManager
{
    private RepositoryContext _repositoryContext;
    private IBoardGameRepository _boardGameRepository;
    private IUserRepository _userRepository;
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
    
    public IUserRepository User
    {
        get
        {
            if(_userRepository == null)
                _userRepository = new UserRepositry(_repositoryContext);
            return _userRepository;
        }
    }

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
