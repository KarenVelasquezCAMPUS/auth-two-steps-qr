using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable{
    private readonly ApiAuthTwoStepCreateQrUContext _context;
    private IUserRepository _user;
    public UnitOfWork(ApiAuthTwoStepCreateQrUContext ctx) => _context = ctx;

    public IUserRepository Users => _user ??= new UserRepository(_context);

    public void Dispose()
    { 
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChanges()=> await _context.SaveChangesAsync();
}