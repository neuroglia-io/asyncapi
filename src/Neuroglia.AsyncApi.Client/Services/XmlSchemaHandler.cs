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

using Neuroglia.Serialization;
using System.Net;
using System.Xml.Schema;
using System.Xml;

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Represents the <see cref="ISchemaHandler"/> implementation used to handle XML schemas
/// </summary>
/// <param name="serializer">The service used to serialize/deserialize data to/from XML</param>
public class XmlSchemaHandler(IXmlSerializer serializer)
    : ISchemaHandler
{

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from XML
    /// </summary>
    protected IXmlSerializer Serializer { get; } = serializer;

    /// <inheritdoc/>
    public virtual bool Supports(string format) => format.Equals(SchemaFormat.Xml, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IOperationResult> ValidateAsync(object graph, object schema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(schema);
        XmlSchema xmlSchema;
        using var reader = new StringReader(Serializer.SerializeToText(schema));
        xmlSchema = XmlSchema.Read(reader, OnValidationError)!;
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(xmlSchema);
        settings.ValidationType = ValidationType.Schema;
        var xml = Serializer.SerializeToText(graph);
        using var stringReader = new StringReader(xml);
        using var xmlReader = XmlReader.Create(stringReader, settings);
        var validationErrors = new List<string>();
        settings.ValidationEventHandler += (sender, args) => validationErrors.Add(args.Message);
        try
        {
            while (xmlReader.Read()) { }
        }
        catch (XmlException ex)
        {
            validationErrors.Add(ex.Message);
        }
        if (validationErrors.Count != 0) return new OperationResult((int)HttpStatusCode.BadRequest, null, new Error()
        {
            Type = ErrorType.Validation,
            Title = ErrorTitle.Validation,
            Status = ErrorStatus.Validation,
            Errors = new(validationErrors.GroupBy(e => e).Select(e => new KeyValuePair<string, string[]>(e.Key, [e.Key])))
        });
        else return await Task.FromResult(new OperationResult((int)HttpStatusCode.OK));
    }

    /// <summary>
    /// Handles schema validation errors
    /// </summary>
    protected virtual void OnValidationError(object? sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error) throw new XmlSchemaValidationException(e.Message, e.Exception, e.Exception.LineNumber, e.Exception.LinePosition);
    }

}

