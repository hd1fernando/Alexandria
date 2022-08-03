namespace Alexandria.Bussiness.Interfaces.Services;

public interface IBookInstanceService
{
    public Task CreateBookInstanceAsync(string isbn, string circulationType, CancellationToken cancellationToken);
}
