namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the base class for all <see cref="IOperationDefinitionBuilder"/> implementations
/// </summary>
/// <remarks>
/// Initializes a new <see cref="OperationDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="OperationTraitDefinition"/>s</param>
public class OperationDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<OperationTraitDefinition>> validators)
    : OperationTraitDefinitionBuilder<IOperationDefinitionBuilder, OperationDefinition>(serviceProvider, validators), IOperationDefinitionBuilder
{

    /// <inheritdoc/>
    public virtual IOperationDefinitionBuilder WithMessage(Action<IMessageDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);

        var builder = ActivatorUtilities.CreateInstance<MessageDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        this.Trait.Message = OperationMessageDefinition.From(builder.Build());

        return this;
    }

    /// <inheritdoc/>
    public virtual IOperationDefinitionBuilder WithMessages(params Action<IMessageDefinitionBuilder>[] setups)
    {
        if (setups == null || setups.Length == 0) throw new ArgumentNullException(nameof(setups));

        this.Trait.Message = new()
        {
            OneOf = new(setups.Select(setup =>
            {
                var builder = ActivatorUtilities.CreateInstance<MessageDefinitionBuilder>(this.ServiceProvider);
                setup(builder);
                return OperationMessageDefinition.From(builder.Build());
            }))
        };

        return this;
    }

    /// <inheritdoc/>
    public virtual IOperationDefinitionBuilder WithTrait(Action<IOperationTraitBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);

        this.Trait.Traits ??= [];
        var builder = ActivatorUtilities.CreateInstance<OperationTraitBuilder>(this.ServiceProvider);
        setup(builder);
        this.Trait.Traits.Add(builder.Build());

        return this;
    }

}
