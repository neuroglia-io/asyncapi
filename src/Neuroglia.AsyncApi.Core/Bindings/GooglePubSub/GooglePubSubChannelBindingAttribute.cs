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

namespace Neuroglia.AsyncApi.Bindings.GooglePubSub;

/// <summary>
/// Represents the attribute used to define an <see cref="GooglePubSubChannelBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class GooglePubSubChannelBindingAttribute(string name, string version = "latest")
    : ChannelBindingAttribute<GooglePubSubChannelBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets a list of labels used to categorize Cloud Resources like Cloud Pub/Sub Topics. Values must be structured using the '{key}:{value}' format
    /// </summary>
    public virtual string[]? Labels { get; init; }

    /// <summary>
    /// Gets/sets the minimum duration to retain a message after it is published to the topic (Must be a valid Duration.)
    /// </summary>
    public virtual string? MessageRetentionDuration { get; init; }

    /// <summary>
    /// Gets/sets a list of IDs of GCP regions where messages that are published to the topic may be persisted in storage
    /// </summary>
    public virtual string[]? AllowedPersistenceRegions { get; init; }

    /// <summary>
    /// Gets/sets the encoding of the message
    /// </summary>
    public virtual GooglePubSubMessageEncoding? SchemaEncoding { get; init; }

    /// <summary>
    /// Gets/sets the minimum (inclusive) revision allowed for validating messages
    /// </summary>
    public virtual string? SchemaFirstRevisionId { get; init; }

    /// <summary>
    /// Gets/sets the maximum (inclusive) revision allowed for validating messages
    /// </summary>
    public virtual string? SchemaLastRevisionId { get; init; }

    /// <summary>
    /// Gets/sets the name of the schema that messages published should be validated against (The format is projects/{project}/schemas/{schema}.)
    /// </summary>
    public virtual string? SchemaName { get; init; }

    /// <inheritdoc/>
    public override GooglePubSubChannelBindingDefinition Build() 
    {
        var binding = new GooglePubSubChannelBindingDefinition()
        {
            BindingVersion = Version,
            Labels = this.Labels == null ? null : new(this.Labels.Select(l => 
            {
                var components = l.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (components.Length != 2) return null;
                return new { Key = components[0], Value = components[1] };
            })
            .Where(v => v != null)
            .ToDictionary(kvp => kvp!.Key, kvp => kvp!.Value)),
            MessageRetentionDuration = this.MessageRetentionDuration
        };
        if (this.AllowedPersistenceRegions != null) binding.MessageStoragePolicy = new() 
        { 
            AllowedPersistenceRegions = new(this.AllowedPersistenceRegions) 
        };
        if (this.SchemaEncoding.HasValue || !string.IsNullOrWhiteSpace(this.SchemaFirstRevisionId) || !string.IsNullOrWhiteSpace(this.SchemaFirstRevisionId) || !string.IsNullOrWhiteSpace(this.SchemaName)) binding.SchemaSettings = new()
        {
            Encoding = this.SchemaEncoding ?? GooglePubSubMessageEncoding.Unspecified,
            FirstRevisionId = this.SchemaFirstRevisionId,
            LastRevisionId = this.SchemaLastRevisionId,
            Name = this.SchemaName
        };
        return binding;
    }

}
