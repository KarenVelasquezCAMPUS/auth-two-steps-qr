namespace Domain.Interfaces;
public interface IUnitOfWork
{
    IUserRepository Users { get; }
    Task<int> SaveChanges();
}