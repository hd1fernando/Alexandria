using Alexandria.Bussiness.Entitties;
using FluentNHibernate.Mapping;

namespace Alexandria.Infra.Mappins;
public class BookEntityMap : ClassMap<BookEntity>
{
    public BookEntityMap()
    {
        Id(x => x.Id);

        Map(x => x.Title).Not.Nullable();
        Map(x => x.Price).Not.Nullable();
        Map(x => x.ISBN).Unique().Not.Nullable();

        Table("BOOK");
    }
}
