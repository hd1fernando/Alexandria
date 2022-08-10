namespace Alexandria.ApplicationService.Dtos.Response;

public class CreatedUserResponse
{
    public bool Sucess { get; private set; }
    public List<string> Errors { get; private set; }

    public CreatedUserResponse()
        => Errors = new List<string>();

    public CreatedUserResponse(bool sucess) : this() 
        => Sucess = sucess;

    public void AddErrors(IEnumerable<string> strings) 
        => Errors.AddRange(Errors);
}
