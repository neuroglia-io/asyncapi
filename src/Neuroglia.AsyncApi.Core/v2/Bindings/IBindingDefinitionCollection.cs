using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.v2.Bindings;

/// <summary>
/// Defines the fundamentals of a collection of <see cref="IBindingDefinition"/>s
/// </summary>
/// <typeparam name="TBinding">The type of <see cref="IBindingDefinition"/> contained by the <see cref="IBindingDefinitionCollection{TBinding}"/></typeparam>
public interface IBindingDefinitionCollection<TBinding>
    : IReferenceable
    where TBinding : IBindingDefinition
{



}
