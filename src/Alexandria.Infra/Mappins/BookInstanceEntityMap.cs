using Alexandria.Bussiness.Entitties;
using FluentNHibernate.Mapping;

namespace Alexandria.Infra.Mappins;

public class BookInstanceEntityMap : ClassMap<BookInstanceEntity>
{
    public BookInstanceEntityMap()
    {
        Id(x => x.Id);
        Map(x => x.CirculationType);

        References(x => x.Book)
            .Column("BookId")
            .ForeignKey("Id");

        Table("BOOK_INSTANCE");
    }
}
