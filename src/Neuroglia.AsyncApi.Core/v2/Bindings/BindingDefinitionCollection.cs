using System.Reflection;
using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.v2.Bindings;

/// <summary>
/// Represents the base record for all <see cref="IBindingDefinitionCollection{TBinding}"/> implementations
/// </summary>
/// <typeparam name="TBinding">The type of <see cref="IBindingDefinition"/> contained by the <see cref="BindingDefinitionCollection{TBinding}"/></typeparam>
[DataContract]
public abstract record BindingDefinitionCollection<TBinding>
    : ReferenceableComponentDefinition, IBindingDefinitionCollection<TBinding>
    where TBinding : IBindingDefinition
{

    private List<PropertyInfo> _bindingProperties = null!;
    private List<PropertyInfo> BindingProperties
    {
        get
        {
            _bindingProperties ??= GetType().GetProperties().Where(p => typeof(TBinding).IsAssignableFrom(p.PropertyType)).ToList();
            return _bindingProperties;
        }
    }

    /// <summary>
    /// Adds the specified <see cref="IBindingDefinition"/> to the <see cref="BindingDefinitionCollection{TBinding}"/>
    /// </summary>
    /// <param name="binding">The <see cref="IBindingDefinition"/> to add</param>
    public virtual void Add(TBinding binding)
    {
        ArgumentNullException.ThrowIfNull(binding);

        var property = BindingProperties.FirstOrDefault(p => p.PropertyType.IsAssignableFrom(binding.GetType())) ?? throw new InvalidOperationException($"Failed to find a binding property of the specified type '{typeof(TBinding).Name}'");

        property.SetValue(this, binding);
    }

    /// <summary>
    /// Converts the <see cref="BindingDefinitionCollection{TBinding}"/> into a new <see cref="IEnumerable{T}"/> containing the <see cref="IBindingDefinition"/>s the <see cref="BindingDefinitionCollection{TBinding}"/> is made out of
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="IBindingDefinition"/>s the <see cref="BindingDefinitionCollection{TBinding}"/> is made out of</returns>
    public abstract IEnumerable<TBinding> AsEnumerable();

}
