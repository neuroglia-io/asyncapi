namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Exposes constants about API key locations
/// </summary>
public static class ApiKeyLocation
{

    /// <summary>
    /// Stores the API key in the 'user' field
    /// </summary>
    public const string User = "user";
    /// <summary>
    /// Stores the API key in the 'password' field
    /// </summary>
    public const string Password = "password";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> that contains all supported API key locations
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> that contains all supported API key locations</returns>
    public static IEnumerable<string> GetLocations()
    {
        yield return User;
        yield return Password;
    }

}
