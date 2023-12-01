// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using JsonCons.Utilities;
using System.Text.Json;
using System.Text.Json.Nodes;
using JsonPointer = Json.Pointer.JsonPointer;

namespace Neuroglia.Data.Schemas;

/// <summary>
/// Defines the fundamentals of a service used to resolve <see cref="JsonSchema"/>s
/// </summary>
public interface IJsonSchemaResolver
{

    /// <summary>
    /// Resolves the $refs of the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> to resolve</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The resolved <see cref="JsonSchema"/></returns>
    Task<JsonSchema> ResolveSchemaAsync(JsonSchema schema, CancellationToken cancellationToken = default);

}

/// <summary>
/// Represents the default implementation of the <see cref="IJsonSchemaResolver"/> interface
/// </summary>
/// <param name="httpClient">The service used to perform HTTP requests</param>
public class JsonSchemaResolver(HttpClient httpClient)
    : IJsonSchemaResolver
{

    /// <summary>
    /// Gets the service used to perform HTTP requests
    /// </summary>
    protected HttpClient HttpClient { get; } = httpClient;

    /// <inheritdoc/>
    public virtual async Task<JsonSchema> ResolveSchemaAsync(JsonSchema schema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(schema);

        var jsonElement = Serialization.Json.JsonSerializer.Default.SerializeToElement(schema)!;
        jsonElement = await this.ResolveSchemaAsync(jsonElement.Value, jsonElement, cancellationToken).ConfigureAwait(false);
        var json = Serialization.Json.JsonSerializer.Default.SerializeToText(jsonElement)!;
        return JsonSchema.FromText(json);
    }

    /// <summary>
    /// Resolves the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonElement"/> representation of the <see cref="JsonSchema"/> to resolve</param>
    /// <param name="rootSchema">The <see cref="JsonElement"/> representation of the root <see cref="JsonSchema"/>, if any, of the <see cref="JsonSchema"/> to resolve</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="JsonElement"/> representation of the <see cref="JsonSchema"/> to resolve</returns>
    protected virtual async Task<JsonElement> ResolveSchemaAsync(JsonElement schema, JsonElement? rootSchema = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(schema);
        rootSchema ??= schema;

        switch (schema.ValueKind)
        {
            case JsonValueKind.Array:
                var schemaArray = new JsonArray();
                foreach (var item in schema.EnumerateArray())
                {
                    schemaArray.Add(await this.ResolveSchemaAsync(item, rootSchema, cancellationToken).ConfigureAwait(false));
                }
                return Serialization.Json.JsonSerializer.Default.SerializeToElement(schemaArray)!.Value;
            case JsonValueKind.Object:
                var refSchemas = await this.ResolveReferencedSchemasAsync(schema, rootSchema.Value, cancellationToken).ConfigureAwait(false);
                var mergedSchema = this.RemoveReferenceProperties(schema);
                foreach (var refSchema in refSchemas)
                {
                    mergedSchema = JsonMergePatch.ApplyMergePatch(mergedSchema, refSchema).RootElement;
                }
                var mergedSchemaNode = Serialization.Json.JsonSerializer.Default.SerializeToNode(mergedSchema)!.AsObject()!;
                foreach (var property in mergedSchema.EnumerateObject())
                {
                    mergedSchemaNode[property.Name] = Serialization.Json.JsonSerializer.Default.SerializeToNode(await this.ResolveSchemaAsync(property.Value, rootSchema, cancellationToken).ConfigureAwait(false));
                }
                return Serialization.Json.JsonSerializer.Default.SerializeToElement(mergedSchemaNode)!.Value;
            default: return schema;
        }
    }

    /// <summary>
    /// Resolves the references specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonElement"/> representation of the <see cref="JsonSchema"/> to resolve</param>
    /// <param name="rootSchema">The <see cref="JsonElement"/> representation of the root <see cref="JsonSchema"/>, if any, of the <see cref="JsonSchema"/> to resolve</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="JsonElement"/> representation of the <see cref="JsonSchema"/> to resolve</returns>
    protected virtual async Task<IEnumerable<JsonElement>> ResolveReferencedSchemasAsync(JsonElement schema, JsonElement rootSchema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(schema);
        ArgumentNullException.ThrowIfNull(rootSchema);

        if (schema.ValueKind != JsonValueKind.Object) return Array.Empty<JsonElement>();

        var refSchemas = new List<JsonElement>();
        var keywords = schema.EnumerateObject();
        var refKeyword = keywords.FirstOrDefault(k => k.NameEquals(RefKeyword.Name));
        var allOfKeyword = keywords.FirstOrDefault(k => k.NameEquals(AllOfKeyword.Name));

        if (refKeyword.Value.ValueKind == JsonValueKind.String)
        {
            var reference = refKeyword.Value.Deserialize<string>()!;
            var refSchema = await this.ResolveReferencedSchemaAsync(new Uri(reference, UriKind.RelativeOrAbsolute), rootSchema, cancellationToken).ConfigureAwait(false);
            if (refSchema.HasValue) refSchemas.Add(refSchema);
        }

        if (allOfKeyword.Value.ValueKind == JsonValueKind.Array)
        {
            foreach (var refSchema in allOfKeyword.Value.EnumerateArray())
            {
                refSchemas.Add(await this.ResolveSchemaAsync(refSchema, null, cancellationToken).ConfigureAwait(false));
            }
        }

        return refSchemas;
    }

    /// <summary>
    /// Resolves the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the <see cref="JsonSchema"/> to resolve</param>
    /// <param name="rootSchema">The <see cref="JsonElement"/> representation of the root <see cref="JsonSchema"/>, if any, of the <see cref="JsonSchema"/> to resolve</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="JsonElement"/> representation of the <see cref="JsonSchema"/> to resolve</returns>
    protected virtual async Task<JsonElement?> ResolveReferencedSchemaAsync(Uri uri, JsonElement rootSchema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(uri);
        ArgumentNullException.ThrowIfNull(rootSchema);
        var useRootSchema = true;

        JsonElement? schema;
        if (uri.IsAbsoluteUri)
        {
            var schemaJson = await this.HttpClient.GetStringAsync(uri, cancellationToken).ConfigureAwait(false);
            schema = Serialization.Json.JsonSerializer.Default.SerializeToElement(schemaJson);
            useRootSchema = false;
        }
        else
        {
            if (!JsonPointer.TryParse(uri.OriginalString, out var jsonPointer) || jsonPointer == null) throw new NullReferenceException($"Failed to find the JSON schema at '{uri}'");
            schema = jsonPointer.Evaluate(rootSchema);
        }

        if (schema == null) return null;
        else return await this.ResolveSchemaAsync(schema.Value, useRootSchema ? rootSchema : null, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Removes references from the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonElement"/> representation of the <see cref="JsonSchema"/> to remove references from</param>
    /// <returns>The <see cref="JsonElement"/> purged out of references</returns>
    protected virtual JsonElement RemoveReferenceProperties(JsonElement schema)
    {
        ArgumentNullException.ThrowIfNull(schema);

        var node = Serialization.Json.JsonSerializer.Default.SerializeToNode(schema)?.AsObject();
        if (node == null) return schema;

        node.Remove(RefKeyword.Name);
        node.Remove(AllOfKeyword.Name);

        return Serialization.Json.JsonSerializer.Default.SerializeToElement(node)!.Value;
    }

}