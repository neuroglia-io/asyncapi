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

namespace Neuroglia.AsyncApi.v3;

/// <summary>
/// Represents an object that provides metadata about the API. The metadata can be used by the clients if needed
/// </summary>
[DataContract]
public record V3ApiInfo
{

    /// <summary>
    /// Gets/sets the title of the application
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "title"), JsonPropertyOrder(1), JsonPropertyName("title"), YamlMember(Order = 1, Alias = "title")]
    public virtual string Title { get; set; } = null!;

    /// <summary>
    /// Gets/sets the version of the application API (not to be confused with the specification version)
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "version"), JsonPropertyOrder(2), JsonPropertyName("version"), YamlMember(Order = 2, Alias = "version")]
    public virtual string Version { get; set; } = null!;

    /// <summary>
    /// Gets/sets a short description of the application. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 3, Name = "description"), JsonPropertyOrder(3), JsonPropertyName("description"), YamlMember(Order = 3, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="Uri"/> to the Terms of Service for the API.
    /// </summary>
    [DataMember(Order = 4, Name = "termsOfService"), JsonPropertyOrder(4), JsonPropertyName("termsOfService"), YamlMember(Order = 4, Alias = "termsOfService")]
    public virtual Uri? TermsOfService { get; set; }

    /// <summary>
    /// Gets/sets the contact information for the exposed API.
    /// </summary>
    [DataMember(Order = 5, Name = "contact"), JsonPropertyOrder(5), JsonPropertyName("contact"), YamlMember(Order = 5, Alias = "contact")]
    public virtual V3ContactDefinition? Contact { get; set; }

    /// <summary>
    /// Gets/sets the license information for the exposed API.
    /// </summary>
    [DataMember(Order = 6, Name = "license"), JsonPropertyOrder(6), JsonPropertyName("license"), YamlMember(Order = 6, Alias = "license")]
    public virtual V3LicenseDefinition? License { get; set; }

    /// <summary>
    /// Gets/sets a list of tags for application API documentation control. Tags can be used for logical grouping of applications.
    /// </summary>
    [DataMember(Order = 7, Name = "tags"), JsonPropertyOrder(7), JsonPropertyName("tags"), YamlMember(Order = 7, Alias = "tags")]
    public virtual EquatableList<V3TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets additional external documentation of the exposed API.
    /// </summary>
    [DataMember(Order = 8, Name = "externalDocs"), JsonPropertyOrder(8), JsonPropertyName("externalDocs"), YamlMember(Order = 8, Alias = "externalDocs")]
    public virtual V3ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.Title;

}
