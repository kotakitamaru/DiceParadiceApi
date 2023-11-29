using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using DiceParadiceApi.Models;
using DiceParadiceApi.Repository.Extensions.Utility;

namespace DiceParadiceApi.Repository.Extensions;

public static class RepositoryBoardGameExtensions
{
    public static IQueryable<BoardGame> FilterByPrice(this IQueryable<BoardGame> boardGames, double minPrice, double maxPrice) =>
        boardGames.Where(e => e.Price >= minPrice && e.Price <= maxPrice);
    
    public static IQueryable<BoardGame> Search(this IQueryable<BoardGame> boardGames, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return boardGames;
        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return boardGames.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<BoardGame> Sort(this IQueryable<BoardGame> boardGames, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return boardGames.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<BoardGame>(orderByQueryString);
            
        if (string.IsNullOrWhiteSpace(orderQuery))
            return boardGames.OrderBy(b => b.Name);
        return boardGames.OrderBy(orderQuery);
    }
}