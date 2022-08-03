namespace Alexandria.Bussiness.Entitties;

public class BookInstanceEntity : Entity<int>
{
    public virtual string? CirculationType { get; }
    public virtual BookEntity? Book { get; }

    [Obsolete("Apenas para uso do ORM.")]
    public BookInstanceEntity() { }

    public BookInstanceEntity(string? circulationType, BookEntity? book)
    {
        CirculationType = circulationType;
        Book = book;
    }
}
