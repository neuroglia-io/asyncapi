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
/// Represents the settings for validating messages published against a schema
/// </summary>
[DataContract]
public record GooglePubSubSchemaSettings
{

    /// <summary>
    /// Gets/sets the encoding of the message
    /// </summary>
    [DataMember(Order = 1, Name = "encoding"), JsonPropertyOrder(1), JsonPropertyName("encoding"), YamlMember(Order = 1, Alias = "encoding")]
    public virtual GooglePubSubMessageEncoding Encoding { get; set; }

    /// <summary>
    /// Gets/sets the minimum (inclusive) revision allowed for validating messages
    /// </summary>
    [DataMember(Order = 2, Name = "firstRevisionId"), JsonPropertyOrder(2), JsonPropertyName("firstRevisionId"), YamlMember(Order = 2, Alias = "firstRevisionId")]
    public virtual string? FirstRevisionId { get; set; }

    /// <summary>
    /// Gets/sets the maximum (inclusive) revision allowed for validating messages
    /// </summary>
    [DataMember(Order = 3, Name = "lastRevisionId"), JsonPropertyOrder(3), JsonPropertyName("lastRevisionId"), YamlMember(Order = 3, Alias = "lastRevisionId")]
    public virtual string? LastRevisionId { get; set; }

    /// <summary>
    /// Gets/sets the name of the schema that messages published should be validated against (The format is projects/{project}/schemas/{schema}.)
    /// </summary>
    [DataMember(Order = 4, Name = "name"), JsonPropertyOrder(4), JsonPropertyName("name"), YamlMember(Order = 4, Alias = "name")]
    public virtual string? Name { get; set; }

}
