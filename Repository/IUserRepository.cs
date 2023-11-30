using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;

namespace DiceParadiceApi.Repository.Extensions;

public interface IUserRepository
{
    void CreateUser(UserForRegistrationDto user);
    bool CheckPassword(User user, string password);
    Task<User> FindByEmailAsync(string email, bool trackChanges = false);
    Task UpdateAsync(User user);
}