using Alexandria.Infra.Mappins;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Alexandria.Api.Extensions;
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
