using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="OperationDefinition"/>s
/// </summary>
public interface IOperationDefinitionBuilder
    : IOperationTraitDefinitionBuilder<IOperationDefinitionBuilder, OperationDefinition>
{

    /// <summary>
    /// Configures the <see cref="OperationDefinition"/> to build to use the specified <see cref="OperationTraitDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OperationTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IOperationDefinitionBuilder"/></returns>
    IOperationDefinitionBuilder WithTrait(Action<IOperationTraitBuilder> setup);

    /// <summary>
    /// Configures the <see cref="OperationDefinition"/> to build to use the specified <see cref="MessageDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IOperationDefinitionBuilder"/></returns>
    IOperationDefinitionBuilder WithMessage(Action<IMessageDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="OperationDefinition"/> to build to use the specified <see cref="MessageDefinition"/>s
    /// </summary>
    /// <param name="setups">An array containing the <see cref="Action{T}"/> used to setup the <see cref="MessageDefinition"/>s to use</param>
    /// <returns>The configured <see cref="IOperationDefinitionBuilder"/></returns>
    IOperationDefinitionBuilder WithMessages(params Action<IMessageDefinitionBuilder>[] setups);

}
