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
/// Represents an object that holds a set of reusable objects for different aspects of the AsyncAPI specification. All objects defined within the components object will have no effect on the API unless they are explicitly referenced from properties outside the components object.
/// </summary>
[DataContract]
public record V3ComponentDefinitionCollection
{

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Schema Objects. 
    /// If this is a Schema Object, then the schemaFormat will be assumed to be "application/vnd.aai.asyncapi+json;version=asyncapi" where the version is equal to the AsyncAPI Version String.
    /// </summary>
    [DataMember(Order = 1, Name = "schemas"), JsonPropertyOrder(1), JsonPropertyName("schemas"), YamlMember(Order = 1, Alias = "schemas")]
    public virtual EquatableDictionary<string, V3SchemaDefinition>? Schemas { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Server Objects. 
    /// </summary>
    [DataMember(Order = 2, Name = "servers"), JsonPropertyOrder(2), JsonPropertyName("servers"), YamlMember(Order = 2, Alias = "servers")]
    public virtual EquatableDictionary<string, V3ServerDefinition>? Servers { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Channel Objects. 
    /// </summary>
    [DataMember(Order = 3, Name = "channels"), JsonPropertyOrder(3), JsonPropertyName("channels"), YamlMember(Order = 3, Alias = "channels")]
    public virtual EquatableDictionary<string, V3ChannelDefinition>? Channels { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Operation Objects. 
    /// </summary>
    [DataMember(Order = 4, Name = "operations"), JsonPropertyOrder(4), JsonPropertyName("operations"), YamlMember(Order = 4, Alias = "operations")]
    public virtual EquatableDictionary<string, V3OperationDefinition>? Operations { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Message Objects. 
    /// </summary>
    [DataMember(Order = 5, Name = "messages"), JsonPropertyOrder(5), JsonPropertyName("messages"), YamlMember(Order = 5, Alias = "messages")]
    public virtual EquatableDictionary<string, V3MessageDefinition>? Messages { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Security Scheme Objects. 
    /// </summary>
    [DataMember(Order = 6, Name = "securitySchemes"), JsonPropertyOrder(6), JsonPropertyName("securitySchemes"), YamlMember(Order = 6, Alias = "securitySchemes")]
    public virtual EquatableDictionary<string, V3SecuritySchemeDefinition>? SecuritySchemes { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Server Variable Objects. 
    /// </summary>
    [DataMember(Order = 7, Name = "serverVariables"), JsonPropertyOrder(7), JsonPropertyName("serverVariables"), YamlMember(Order = 7, Alias = "serverVariables")]
    public virtual EquatableDictionary<string, ServerVariableDefinition>? ServerVariables { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Parameter Objects. 
    /// </summary>
    [DataMember(Order = 8, Name = "parameters"), JsonPropertyOrder(8), JsonPropertyName("parameters"), YamlMember(Order = 8, Alias = "parameters")]
    public virtual EquatableDictionary<string, V3ParameterDefinition>? Parameters { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Correlation Id Objects. 
    /// </summary>
    [DataMember(Order = 9, Name = "correlationIds"), JsonPropertyOrder(9), JsonPropertyName("correlationIds"), YamlMember(Order = 9, Alias = "correlationIds")]
    public virtual EquatableDictionary<string, CorrelationIdDefinition>? CorrelationIds { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Reply Objects. 
    /// </summary>
    [DataMember(Order = 10, Name = "replies"), JsonPropertyOrder(10), JsonPropertyName("replies"), YamlMember(Order = 10, Alias = "replies")]
    public virtual EquatableDictionary<string, V3OperationReplyDefinition>? Replies { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Reply Address Objects. 
    /// </summary>
    [DataMember(Order = 11, Name = "replyAddresses"), JsonPropertyOrder(11), JsonPropertyName("replyAddresses"), YamlMember(Order = 11, Alias = "replyAddresses")]
    public virtual EquatableDictionary<string, V3OperationReplyAddressDefinition>? ReplyAddresses { get; set; }
    
    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable External Documentation Objects. 
    /// </summary>
    [DataMember(Order = 12, Name = "externalDocs"), JsonPropertyOrder(12), JsonPropertyName("externalDocs"), YamlMember(Order = 12, Alias = "externalDocs")]
    public virtual EquatableDictionary<string, ExternalDocumentationDefinition>? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Tag Objects. 
    /// </summary>
    [DataMember(Order = 13, Name = "tags"), JsonPropertyOrder(13), JsonPropertyName("tags"), YamlMember(Order = 13, Alias = "tags")]
    public virtual EquatableDictionary<string, TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Operation Trait Objects. 
    /// </summary>
    [DataMember(Order = 14, Name = "operationTraits"), JsonPropertyOrder(14), JsonPropertyName("operationTraits"), YamlMember(Order = 14, Alias = "operationTraits")]
    public virtual EquatableDictionary<string, V3OperationTraitDefinition>? OperationTraits { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Message Trait Objects. 
    /// </summary>
    [DataMember(Order = 15, Name = "messageTraits"), JsonPropertyOrder(15), JsonPropertyName("messageTraits"), YamlMember(Order = 15, Alias = "messageTraits")]
    public virtual EquatableDictionary<string, V3MessageTraitDefinition>? MessageTraits { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Server Binding Objects. 
    /// </summary>
    [DataMember(Order = 16, Name = "serverBindings"), JsonPropertyOrder(16), JsonPropertyName("serverBindings"), YamlMember(Order = 16, Alias = "serverBindings")]
    public virtual EquatableDictionary<string, ServerBindingDefinitionCollection>? ServerBindings { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Channel Binding Objects. 
    /// </summary>
    [DataMember(Order = 17, Name = "channelBindings"), JsonPropertyOrder(17), JsonPropertyName("channelBindings"), YamlMember(Order = 17, Alias = "channelBindings")]
    public virtual EquatableDictionary<string, ChannelBindingDefinitionCollection>? ChannelBindings { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Operation Binding Objects. 
    /// </summary>
    [DataMember(Order = 18, Name = "operationBindings"), JsonPropertyOrder(18), JsonPropertyName("operationBindings"), YamlMember(Order = 18, Alias = "operationBindings")]
    public virtual EquatableDictionary<string, OperationBindingDefinitionCollection>? OperationBindings { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping that contains reusable Message Binding Objects. 
    /// </summary>
    [DataMember(Order = 19, Name = "messageBindings"), JsonPropertyOrder(19), JsonPropertyName("messageBindings"), YamlMember(Order = 19, Alias = "messageBindings")]
    public virtual EquatableDictionary<string, MessageBindingDefinitionCollection>? MessageBindings { get; set; }

}
