using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            RegisterDbContexts<ApplicationDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext>(services, configuration, env);
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });

            //services.AddTransient<IAggregateRepository<ApplicationRoleAggregate>, AggregateRepository<ApplicationRoleAggregate>>();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IWebHostEnvironment env)
        {
            X509Certificate2? cert = null;
            using (X509Store certStore = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
            {
                certStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    // Replace below with your cert's thumbprint
                    "AB615AD0A43A86BA5E4ED338E6C62BA3A7344A08",
                    false);
                // Get the first cert with the thumbprint
                if (certCollection.Count > 0)
                {
                    cert = certCollection[0];
                }
            }

            // Fallback to local file for development
            if (cert == null)
            {
                cert = new X509Certificate2(Path.Combine(env.ContentRootPath, "Cert/example.pfx"), "123456789");
            }

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddConfigurationStore<IdentityServerConfigurationDbContext>()
                .AddOperationalStore<IdentityServerPersistedGrantDbContext>()
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();

            if (env.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                builder.AddSigningCredential(cert);
            }


            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIdentityRepository, IdentityRepository>()
                .AddScoped<IClientRepository, ClientRepository>()
                .AddScoped<IIdentityResourceRepository, IdentityResourceRepository>()
                .AddScoped<IApiResourceRepository, ApiResourceRepository>()
                .AddScoped<IApiScopeRepository, ApiScopeRepository>()
                .AddScoped<IPermissionRepository, PermissionRepository>()
                .AddScoped<IRolePermissionsRepository, RolePermissionsRepository>()
                .AddScoped<IMenuConfigurationRepository, MenuConfigurationRepository>()
                .AddScoped<IClientMenuConfigurationRepository, ClientMenuConfigurationRepository>()
                .AddScoped<IUserLevelRepository, UserLevelRepository>()
                .AddScoped<IUserBranchRepository, UserBranchRepository>()
                .AddScoped<IAccessPrivilegeConfigConfigurationRepository, AccessPrivilegeConfigConfigurationRepository>()
                .AddScoped<IClientMenuConfigurationRepository, ClientMenuConfigurationRepository>();

            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IHandleCache<>), typeof(HandleCache<>));


            return services;
        }
        private static IServiceCollection RegisterDbContexts<TApplicationIdentityDbContext, TIdentityServerConfigurationDbContext, TIdentityServerPersistedGrantDbContext>(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        where TApplicationIdentityDbContext : DbContext
        where TIdentityServerConfigurationDbContext : DbContext, IAdminConfigurationDbContext
        where TIdentityServerPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
        {
            if (env.EnvironmentName == "IntegrationTest")
            {
                var dbName = "ApplicationDb";
                services.AddDbContext<ApplicationDbContext>(opt => opt
                    .UseInMemoryDatabase(dbName));

                services.AddConfigurationDbContext<TIdentityServerConfigurationDbContext>(options => options.ConfigureDbContext = b =>
                    b.UseInMemoryDatabase(dbName));

                services.AddOperationalDbContext<TIdentityServerPersistedGrantDbContext>(options => options.ConfigureDbContext = b =>
                    b.UseInMemoryDatabase(dbName));

            }
            else
            {
                var applicationIdentityConnectionString =
                    configuration.GetConnectionString(ConfigurationConstants.ApplicationIdentityConnectionStringKey);
                var adminConfigurationConnectionString =
                    configuration.GetConnectionString(ConfigurationConstants.AdminConfigurationConnectionStringKey);
                var persistedConfigurationConnectionString =
                    configuration.GetConnectionString(ConfigurationConstants.PersistedConfigurationConnectionStringKey);

                services.AddDbContext<TApplicationIdentityDbContext>(options =>
                    options
                        .UseSqlServer(
                            applicationIdentityConnectionString,
                            b => b.MigrationsAssembly(typeof(TApplicationIdentityDbContext).Assembly.FullName))
                );

                services.AddConfigurationDbContext<TIdentityServerConfigurationDbContext>(options => options.ConfigureDbContext = b =>
                    b.UseSqlServer(adminConfigurationConnectionString, sql => sql.MigrationsAssembly(typeof(TApplicationIdentityDbContext).Assembly.FullName)));

                services.AddOperationalDbContext<TIdentityServerPersistedGrantDbContext>(options => options.ConfigureDbContext = b =>
                    b.UseSqlServer(persistedConfigurationConnectionString, sql => sql.MigrationsAssembly(typeof(TIdentityServerPersistedGrantDbContext).Assembly.FullName)));

            }

            services.AddScoped<IAdminConfigurationDbContext, IdentityServerConfigurationDbContext>();
            services.AddScoped<IAdminPersistedGrantDbContext, IdentityServerPersistedGrantDbContext>();

            return services;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            //redis Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:ConnectionString"];
            });

            return services;
        }
    }
}
