using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAuthTwoStepCreateQrU.Extensions;
public static class ApplicationExtensions
{
    public static void AddAplicacionServices(this IServiceCollection services)
    {
        /* services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService,AuthService>(); */
    }
}