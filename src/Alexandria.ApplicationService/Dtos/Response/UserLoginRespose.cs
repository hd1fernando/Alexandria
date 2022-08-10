using System.Text.Json.Serialization;

namespace Alexandria.ApplicationService.Dtos.Response;

public class UserLoginRespose
{
    public bool Success { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ExpirationTime { get; private set; }

    public List<string>? Errors { get; private set; }

    public UserLoginRespose() => Errors = new List<string>();

    public UserLoginRespose(bool success = true) : this() => Success = success;

    public UserLoginRespose(bool success, string? token, DateTime? expirationTime) : this(success)
    {
        Token = token;
        ExpirationTime = expirationTime; 
    }

    public void AddError(string error) => Errors.Add(error);
}