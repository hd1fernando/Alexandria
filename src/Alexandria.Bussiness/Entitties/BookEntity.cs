namespace Alexandria.Bussiness.Entitties;

public class BookEntity : Entity<int>
{

    public virtual string? Title { get; }
    public virtual decimal Price { get; }
    public virtual string? ISBN { get; }

    public BookEntity(string? title, decimal price, string? iSBN)
    {
        Title = title;
        Price = price;
        ISBN = iSBN;
    }
}
