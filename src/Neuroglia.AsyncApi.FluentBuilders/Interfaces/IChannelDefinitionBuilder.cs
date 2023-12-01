using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ChannelDefinition"/>s
/// </summary>
public interface IChannelDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="ChannelDefinition"/> to build to use the specified description
    /// </summary>
    /// <param name="description">The <see cref="ChannelDefinition"/>'s description</param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithDescription(string description);

    /// <summary>
    /// Adds a new <see cref="ParameterDefinition"/> to the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="ParameterDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ParameterDefinition"/> to add</param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithParameter(string name, Action<IParameterDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="IChannelBindingDefinition"/> to the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IChannelBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithBinding(IChannelBindingDefinition binding);

    /// <summary>
    /// Defines and configures an operation of the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="type">The <see cref="OperationDefinition"/>'s type</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the Subscribe <see cref="OperationDefinition"/></param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithOperation(OperationType type, Action<IOperationDefinitionBuilder> setup);

    /// <summary>
    /// Defines and configures the Subscribe operation of the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the Subscribe <see cref="OperationDefinition"/></param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithSubscribeOperation(Action<IOperationDefinitionBuilder> setup);

    /// <summary>
    /// Defines and configures the Publish operation of the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the Publish <see cref="OperationDefinition"/></param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithPublishOperation(Action<IOperationDefinitionBuilder> setup);

    /// <summary>
    /// Builds a new <see cref="ChannelDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ChannelDefinition"/></returns>
    ChannelDefinition Build();

}
