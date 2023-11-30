using Neuroglia.AsyncApi.FluentBuilders;
using Neuroglia.AsyncApi.Specification.v2;

namespace Neuroglia.AsyncApi.CloudEvents;

/// <summary>
/// Defines extensions for <see cref="IOperationDefinitionBuilder"/>s
/// </summary>
public static class IAsyncApiDocumentBuilderExtensions
{

    /// <summary>
    /// Configures the <see cref="OperationDefinition"/> to build to use the specified <see cref="MessageDefinition"/>
    /// </summary>
    /// <param name="operation">The <see cref="IOperationDefinitionBuilder"/> to configure</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IOperationDefinitionBuilder"/></returns>
    public static IOperationDefinitionBuilder WithCloudEventMessage(this IOperationDefinitionBuilder operation, Action<ICloudEventMessageDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(setup);

        operation.WithMessage(message =>
        {
            var cloudEvent = new CloudEventMessageDefinitionBuilder(message);
            setup(cloudEvent);
            cloudEvent.Build();
        });

        return operation;
    }

}