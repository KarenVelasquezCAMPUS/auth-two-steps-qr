using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    /* // Implementacion de la interfaz
    public Task<IEnumerable<User>> GetAllAsync(IParam param)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> predicate, IParam param)
    {
        throw new NotImplementedException();
    } */

    public async Task<User> GetByIdAsync(long id) => await _entity.FindAsync(id);
}