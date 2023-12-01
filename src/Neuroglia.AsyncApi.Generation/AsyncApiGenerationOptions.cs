using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents the options used to configure code-first AsyncAPI document generation
/// </summary>
public class AsyncApiGenerationOptions
{

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> containing the types used to markup assemblies to scan for Async Api declarations
    /// </summary>
    public virtual List<Type> MarkupTypes { get; set; } = [];

    /// <summary>
    /// Gets/sets the <see cref="Action{T}"/> used to apply a default configuration to generated <see cref="AsyncApiDocument"/>s
    /// </summary>
    public virtual Action<IAsyncApiDocumentBuilder>? DefaultDocumentConfiguration { get; set; }

}