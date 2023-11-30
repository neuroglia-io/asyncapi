using Json.Schema;
using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.Eventing.CloudEvents;

namespace Neuroglia.AsyncApi.CloudEvents;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="CloudEvent"/> <see cref="MessageDefinition"/>s
/// </summary>
public interface ICloudEventMessageDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> spec version
    /// </summary>
    /// <param name="specVersion">The <see cref="CloudEvent"/> spec version to use</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithSpecVersion(string specVersion);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> source
    /// </summary>
    /// <param name="source">The <see cref="CloudEvent"/>'s source <see cref="Uri"/></param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithSource(Uri source);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> type
    /// </summary>
    /// <param name="type">The <see cref="CloudEvent"/>'s type</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithType(string type);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> subject
    /// </summary>
    /// <param name="type">The <see cref="CloudEvent"/>'s subject</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithSubject(string? subject);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data content type
    /// </summary>
    /// <param name="type">The <see cref="CloudEvent"/>'s data content type</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataContentType(string? contentType);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schemaUri">An <see cref="Uri"/> that references the <see cref="CloudEvent"/>'s data <see cref="JsonSchema"/></param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataSchemaUri(Uri? schemaUri);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="CloudEvent"/>'s data <see cref="JsonSchema"/> to use</param>
    /// <param name="schemaUri">An <see cref="Uri"/> that references the <see cref="CloudEvent"/>'s data <see cref="JsonSchema"/></param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataSchema(JsonSchema schema, Uri? schemaUri = null);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data type
    /// </summary>
    /// <param name="type">The type of data transported by the <see cref="CloudEvent"/> to build a new <see cref="MessageDefinition"/> for</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataOfType(Type type);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data type
    /// </summary>
    /// <typeparam name="TData">The type of data transported by the <see cref="CloudEvent"/> to build a new <see cref="MessageDefinition"/> for</typeparam>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataOfType<TData>();

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> extension attribute
    /// </summary>
    /// <param name="type">The <see cref="CloudEvent"/>'s extension attribute</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithExtensionAttribute(string name, string value);

    /// <summary>
    /// Builds the <see cref="MessageDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="MessageDefinition"/></returns>
    MessageDefinition Build();

}
