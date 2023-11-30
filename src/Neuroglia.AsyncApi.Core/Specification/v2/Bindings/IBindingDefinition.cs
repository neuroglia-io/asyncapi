namespace Neuroglia.AsyncApi.Specification.v2.Bindings;

/// <summary>
/// Defines the fundamentals of an Async API binding
/// </summary>
public interface IBindingDefinition
{

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the protocols supported by the <see cref="IBindingDefinition"/>
    /// </summary>
    IEnumerable<string> Protocols { get; }

}