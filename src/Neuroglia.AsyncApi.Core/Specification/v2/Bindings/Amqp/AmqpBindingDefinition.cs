namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Amqp;

/// <summary>
/// Represents the base record for all AMQP implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record AmqpBindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.Amqp;
        }
    }

}
