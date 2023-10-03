using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class ApiAuthTwoStepCreateQrUContext : DbContext
{
    public ApiAuthTwoStepCreateQrUContext(DbContextOptions<ApiAuthTwoStepCreateQrUContext> options) : base(options)
    {
    }


    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}