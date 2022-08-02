namespace Alexandria.Bussiness.Entitties;

public class BookEntity : Entity<int>
{
    public virtual string? Title { get; }
    public virtual decimal Price { get; }
    public virtual string? ISBN { get; }

    [Obsolete("Apenas para uso do ORM")]
    public BookEntity() { }

    public BookEntity(string? title, decimal price, string? iSBN)
    {
        Title = title;
        Price = price;
        ISBN = iSBN;
    }
}
