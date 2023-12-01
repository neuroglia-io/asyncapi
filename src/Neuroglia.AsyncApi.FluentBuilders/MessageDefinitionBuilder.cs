using Neuroglia.AsyncApi.v2;
using Neuroglia.Json.Schema.Generation;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IMessageDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="MessageDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="MessageTraitDefinition"/>s</param>
public class MessageDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<MessageDefinition>> validators)
    : MessageTraitDefinitionBuilder<IMessageDefinitionBuilder, MessageDefinition>(serviceProvider, validators), IMessageDefinitionBuilder
{

    /// <inheritdoc/>
    public virtual IMessageDefinitionBuilder WithPayloadOfType<TPayload>() => this.WithPayloadOfType(typeof(TPayload));

    /// <inheritdoc/>
    public virtual IMessageDefinitionBuilder WithPayloadOfType(Type payloadType)
    {
        ArgumentNullException.ThrowIfNull(payloadType);
        return this.WithPayloadSchema(new JsonSchemaBuilder().FromType(payloadType, JsonSchemaGeneratorConfiguration.Default));
    }

    /// <inheritdoc/>
    public virtual IMessageDefinitionBuilder WithPayloadSchema(JsonSchema payloadSchema)
    {
        ArgumentNullException.ThrowIfNull(payloadSchema);
        this.Trait.Payload = payloadSchema;
        return this;
    }

    /// <inheritdoc/>
    public virtual IMessageDefinitionBuilder WithTrait(Action<IMessageTraitBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);

        this.Trait.Traits ??= [];
        var builder = ActivatorUtilities.CreateInstance<MessageTraitBuilder>(this.ServiceProvider);
        setup(builder);
        this.Trait.Traits.Add(builder.Build());

        return this;
    }

    /// <inheritdoc/>
    public virtual IMessageDefinitionBuilder WithTrait(MessageTraitDefinition trait)
    {
        ArgumentNullException.ThrowIfNull(trait);

        this.Trait.Traits ??= [];
        this.Trait.Traits.Add(trait);

        return this;
    }

    /// <inheritdoc/>
    public virtual IMessageDefinitionBuilder WithTraitReference(string reference)
    {
        if (string.IsNullOrWhiteSpace(reference)) throw new ArgumentNullException(nameof(reference));
        return this.WithTrait(new MessageTraitDefinition() { Reference = reference });
    }

}
