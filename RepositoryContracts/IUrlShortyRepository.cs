using Entities;

namespace RepositoryContracts;

/// <summary>
/// Repository for managing shortened URLs.
/// </summary>
public interface IUrlShortyRepository
{
    /// <summary>
    /// Retrieves the original URL associated with the specified short code.
    /// </summary>
    /// <param name="urlShort">The unique short code of the URL.</param>
    /// <returns>Returns the original URL if a matching record is found;
    /// otherwise <c>null</c>.</returns>
    Task<string?> GetOriginalUrlAsync(string urlShort);

    /// <summary>
    /// Retrieves the complete URL record associated with the specified short code.
    /// </summary>
    /// <param name="urlShort">The unique short code of the URL.</param>
    /// <returns>Returns a <see cref="UrlShorty"/> instance if found;
    /// otherwise <c>null</c>.</returns>
    Task<UrlShorty?> GetByShortCodeFullAsync(string urlShort);

    /// <summary>
    /// Retrieves all the URL records stored in the database.
    /// </summary>
    /// <returns>Returns the <see cref="List{T}"/> of all the <see cref="UrlShorty"/></returns>
    Task<List<UrlShorty>> GetAllUrlShorties();

    /// <summary>
    /// Creates and stores a new shortened URL record that needs to be updated
    /// with the newly created short URL using <see cref="UpdateShortUrl"/>.
    /// </summary>
    /// <param name="urlShorty">The URL entity to add.</param>
    /// <returns>The created <see cref="UrlShorty"/> entity's Id.</returns>
    Task<long> AddInitialAsync(UrlShorty urlShorty);

    /// <summary>
    /// Updates the newly created URL record with the short code
    /// </summary>
    /// <param name="urlShort">The short URL to update.</param>
    /// <param name="urlLong">The original URL to search on.</param>
    /// <returns>The created <see cref="UrlShorty"/> entity.</returns>
    Task<UrlShorty?> UpdateShortUrl(string urlShort, string urlLong);

    /// <summary>
    /// Determines whether a short code already exists.
    /// </summary>
    /// <param name="urlShort">The short code to check.</param>
    /// <returns><c>true</c> if the short code exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(string urlShort);
}