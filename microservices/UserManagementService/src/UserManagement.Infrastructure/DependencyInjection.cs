using CleanArchitecture.Base.Infrastructure.Repository.Write.Query;
using Configuration.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using UserManagement.Application.Contracts.Repositories;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Repository.Command;
using Work.Rabbi.Common.Infrastructure.Persistence;
using Work.Rabbi.Common.Infrastructure.Repository;
using Work.Rabbi.Common.Infrastructure.Repository.Command;
using Work.Rabbi.Common.Interfaces;

namespace UserManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationSettings>(configuration);
            var serviceProvider = services.BuildServiceProvider();
            var opt = serviceProvider.GetRequiredService<IOptions<ConfigurationSettings>>().Value;

            // For SQLServer Connection
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    opt.ConnectionStrings.ConfigurationDbConnection,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                    });
            });

            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<,>), typeof(CommandRepository<,>));
            services.AddScoped<Func<string, IDbConnection>>((provider) => (conn) => new SqlConnection(conn));
            services.AddScoped<Func<ApplicationDbContext>>((provider) => provider.GetService<ApplicationDbContext>);
            services.AddScoped<DbFactory<ApplicationDbContext>>();
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork<ApplicationDbContext>));


            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUserCommandRepository), typeof(UserCommandRepository<ApplicationDbContext>));
            // Add all command and query repositories

            return services;
        }
    }
}
