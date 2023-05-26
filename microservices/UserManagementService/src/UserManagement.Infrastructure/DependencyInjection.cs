using Configuration.Infrastructure.Config;
using Configuration.Infrastructure.Repository;
using Configuration.Infrastructure.Repository.Command.Base;
using Configuration.Infrastructure.Repository.Query.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UserManagement.Infrastructure.Persistence;

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
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<Func<ApplicationDbContext>>((provider) => provider.GetService<ApplicationDbContext>);
            services.AddScoped<DbFactory>();


            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISchemeQueryRepository, SchemeQueryRepository>()
                .AddScoped<ISchemeCommandRepository, SchemeCommandRepository>()
                .AddScoped<ILookupTypeCommandRepository, LookupTypeCommandRepository>()
                .AddScoped<ILookupTypeQueryRepository, LookupTypeQueryRepository>()
                .AddScoped<ILookupDetailCommandRepository, LookupDetailCommandRepository>()
                .AddScoped<ILookupDetailQueryRepository, LookupDetailQueryRepository>()
                .AddScoped<IGatewayConfigurationCommandRepository, GatewayConfigurationCommandRepository>()
                .AddScoped<IGatewayConfigurationQueryRepository, GatewayConfigurationQueryRepository>()
                .AddScoped<IBureauCommandRepository, BureauCommandRepository>()
                .AddScoped<IBureauQueryRepository, BureauQueryRepository>()
                .AddScoped<IBureauConfigCommandRepository, BureauConfigCommandRepository>()
                .AddScoped<IBureauConfigQueryRepository, BureauConfigQueryRepository>()
                .AddScoped<IDaysInYearConfigCommandRepository, DaysInYearConfigCommandRepository>()
                .AddScoped<IDaysInYearConfigQueryRepository, DaysInYearConfigQueryRepository>()
                .AddScoped<IAppSettingCommandRepository, AppSettingCommandRepository>()
                .AddScoped<IAppSettingQueryRepository, AppSettingQueryRepository>()
                .AddScoped<IModuleCommandRepository, ModuleCommandRepository>()
                .AddScoped<IModuleQueryRepository, ModuleQueryRepository>()
                .AddScoped<IReportTypeCommandRepository, ReportTypeCommandRepository>()
                .AddScoped<IReportTypeQueryRepository, ReportTypeQueryRepository>()
                .AddScoped<IReportConfigQueryRepository, ReportConfigQueryRepository>()
                .AddScoped<IReportConfigCommandRepository, ReportConfigCommandRepository>()
                .AddScoped<IReportParamsConfigQueryRepository, ReportParamsConfigQueryRepository>()
                .AddScoped<IReportParamsConfigCommandRepository, ReportParamsConfigCommandRepository>()
                .AddScoped<IAppSettingDataTypeQueryRepository, AppSettingDataTypeQueryRepository>();


            return services;
        }
    }
}
