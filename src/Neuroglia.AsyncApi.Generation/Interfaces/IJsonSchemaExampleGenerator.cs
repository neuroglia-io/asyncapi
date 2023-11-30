namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines the fundamentals of a service used to generate example values based on a JSON Schema
/// </summary>
public interface IJsonSchemaExampleGenerator
{

    /// <summary>
    /// Generates an example value based on the provided JSON Schema.
    /// </summary>
    /// <param name="schema">The JSON Schema for which an example value is to be generated.</param>
    /// <param name="requiredPropertiesOnly">A boolean indicating whether or not only the required the generator should generate values for required properties only</param>
    /// <returns>An object representing an example value conforming to the provided JSON Schema.</returns>
    object? GenerateExample(JsonSchema schema, string? name = null, bool requiredPropertiesOnly = false);

}
