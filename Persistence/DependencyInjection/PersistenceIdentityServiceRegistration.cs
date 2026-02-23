using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Identity;

namespace Persistence.DependencyInjection;

public static class PersistenceIdentityServiceRegistration
{
    public static IServiceCollection AddPersistenceIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CmsIdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}