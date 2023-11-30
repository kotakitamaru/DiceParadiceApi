using System.Security.Cryptography;
using AutoMapper;
using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;
using DiceParadiceApi.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DiceParadiceApi.Repository;

public class UserRepositry:RepositoryBase<User>, IUserRepository
{
    private IMapper _mapper = new Mapper(new MapperConfiguration(x =>
        x.CreateMap<UserForRegistrationDto, User>()
        ));
    
    public UserRepositry(RepositoryContext repositoryContext):base(repositoryContext)
    {
    }


    public void CreateUser(UserForRegistrationDto userForRegistration)
    {
        CreatePasswordHash(userForRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = _mapper.Map<User>(userForRegistration);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        Create(user);
    }

    public async Task<User> FindByEmailAsync(string email, bool trackChanges = false)
    {
        return await FindByCondition(x => x.Email == email,trackChanges)
            .SingleOrDefaultAsync();
    }

    public bool CheckPassword(User user, string password)
    {
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(user.PasswordHash);
    }

    public async Task UpdateAsync(User user)
    {
        var userToUpdate = await FindByCondition(x => x.Id == user.Id, true)
            .SingleOrDefaultAsync();
        userToUpdate = user;
    }
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
    
}