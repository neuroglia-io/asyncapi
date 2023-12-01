using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an <see cref="Attribute"/> used to mark a class as an Async Api to generate a new <see cref="AsyncApiDocument"/> for
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AsyncApiAttribute
    : Attribute
{

    /// <summary>
    /// Initializes a new <see cref="AsyncApiAttribute"/>
    /// </summary>
    /// <param name="title">The <see cref="AsyncApiAttribute"/>'s title</param>
    /// <param name="version">The <see cref="AsyncApiAttribute"/>'s version</param>
    public AsyncApiAttribute(string title, string version)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        this.Title = title;
        this.Version = version;
    }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s id
    /// </summary>
    public virtual string? Id { get; set; }

    /// <summary>
    /// Gets the generated <see cref="AsyncApiDocument"/>'s title
    /// </summary>
    public virtual string Title { get; }

    /// <summary>
    /// Gets the generated <see cref="AsyncApiDocument"/>'s version
    /// </summary>
    public virtual string Version { get; }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s description
    /// </summary>
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s terms of service <see cref="Uri"/>
    /// </summary>
    public virtual string? TermsOfServiceUrl { get; set; }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s contact name
    /// </summary>
    public virtual string? ContactName { get; set; }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s contact url
    /// </summary>
    public virtual string? ContactUrl { get; set; }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s contact email
    /// </summary>
    public virtual string? ContactEmail { get; set; }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s license name
    /// </summary>
    public virtual string? LicenseName { get; set; }

    /// <summary>
    /// Gets/sets the generated <see cref="AsyncApiDocument"/>'s license <see cref="Uri"/>
    /// </summary>
    public virtual string? LicenseUrl { get; set; }

}
