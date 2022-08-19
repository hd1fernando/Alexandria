using Alexandria.Infra.Mappins;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Identity;

namespace Alexandria.Api.Extensions;
public static class WebApplicationExtensions
{
    public static async Task<WebApplication> CreateRolesAsync(this WebApplication app, IConfiguration configuration)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
        //var roleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = configuration.GetSection("Roles").Get<List<string>>();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        return app;
    }
}
public static class NHibernateExtensions
{
    public static IServiceCollection AddNHibernate(this IServiceCollection services, IConfiguration configuration)
    {
        var sessionFactory = Fluently.Configure()
             .Database(MsSqlConfiguration.MsSql2012.ConnectionString(configuration.GetConnectionString("DefaultConnection"))
                 .ShowSql()
                 .FormatSql())
             .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(BookEntityMap).Assembly))
             .BuildSessionFactory();

        services.AddScoped(factory =>
        {
            return sessionFactory.OpenSession();
        });

        return services;
    }
}
