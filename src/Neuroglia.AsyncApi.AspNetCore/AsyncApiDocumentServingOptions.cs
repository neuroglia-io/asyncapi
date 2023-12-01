using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents the options used to configure how to serve <see cref="AsyncApiDocument"/>s
/// </summary>
public class AsyncApiDocumentServingOptions
{

    /// <summary>
    /// Gets/sets the path prefix of <see cref="AsyncApiDocument"/>s
    /// </summary>
    public virtual string PathTemplate { get; set; } = "/asyncapi/{title}.{version}.[json|yaml]";

}
