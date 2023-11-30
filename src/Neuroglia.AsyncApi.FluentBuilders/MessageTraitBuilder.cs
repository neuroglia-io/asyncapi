namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IMessageTraitBuilder"/> interface
/// </summary>
public class MessageTraitBuilder
    : MessageTraitDefinitionBuilder<IMessageTraitBuilder, MessageTraitDefinition>, IMessageTraitBuilder
{

    /// <summary>
    /// Initializes a new <see cref="MessageTraitBuilder"/>
    /// </summary>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="MessageTraitDefinition"/>s</param>
    public MessageTraitBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<MessageTraitDefinition>> validators) : base(serviceProvider, validators) { }

}
