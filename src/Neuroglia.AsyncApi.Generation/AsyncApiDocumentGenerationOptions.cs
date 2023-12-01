using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Represents the options used to configure <see cref="AsyncApiDocument"/> generation
/// </summary>
public class AsyncApiDocumentGenerationOptions
{

    /// <summary>
    /// Gets/sets an <see cref="Action{T}"/> used to configure the <see cref="AsyncApiDocument"/>s to configure
    /// </summary>
    public Action<IAsyncApiDocumentBuilder>? DefaultConfiguration { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the automatically generate examples. Defaults to true.
    /// </summary>
    public bool AutomaticallyGenerateExamples { get; set; } = true;

}