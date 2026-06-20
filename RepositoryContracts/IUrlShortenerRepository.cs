using Entities;

namespace RepositoryContracts;

/// <summary>
/// Repository for managing shortened URLs.
/// </summary>
public interface IUrlShortenerRepository
{
    /// <summary>
    /// Retrieves the complete URL record associated with the specified short code.
    /// </summary>
    /// <param name="urlShort">The unique short code of the URL.</param>
    /// <returns>Returns a <see cref="UrlShortener"/> instance if found;
    /// otherwise <c>null</c>.</returns>
    Task<UrlShortener?> GetByShortUrlAsync(string urlShort);

    /// <summary>
    /// Retrieves all the URL records stored in the database.
    /// </summary>
    /// <returns>Returns the <see cref="List{T}"/> of all the <see cref="UrlShortener"/></returns>
    Task<List<UrlShortener>> GetAllUrlsAsync();

    /// <summary>
    /// Creates and stores a new shortened URL record that needs to be updated
    /// with the newly created short URL using <see cref="UpdateShortUrlAsync"/>.
    /// </summary>
    /// <param name="urlShortener">The URL entity to add.</param>
    /// <returns>The created <see cref="UrlShortener"/> entity's Id.</returns>
    Task<UrlShortener> AddInitialAsync(UrlShortener urlShortener);

    /// <summary>
    /// Updates the newly created URL record with the short code
    /// </summary>
    /// <param name="urlShort">The short URL to update.</param>
    /// <param name="urlLong">The original URL to search on.</param>
    /// <returns>The created <see cref="UrlShortener"/> entity.</returns>
    Task<UrlShortener?> UpdateShortUrlAsync(string urlShort, string urlLong);
}