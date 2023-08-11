namespace SpeciesDatabaseApi;

/// <summary>
/// Defines where a API token will be placed within the request
/// </summary>
public enum ApiTokenPlacement
{
    /// <summary>
    /// Is up to developer to insert the token manually
    /// </summary>
    Manual,

    /// <summary>
    /// The token goes into Header<br/>
    /// Inserted by the base client
    /// </summary>
    Header,

    /// <summary>
    /// The token goes into Header.Authorization<br/>
    /// Inserted by the base client
    /// </summary>
    HeaderAuthorization,

    /// <summary>
    /// The tokes goes as a GET parameter<br/>
    /// Inserted by the base client
    /// </summary>
    Get,
}