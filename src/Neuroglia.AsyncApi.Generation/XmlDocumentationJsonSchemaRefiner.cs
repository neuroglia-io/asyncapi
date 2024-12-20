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

using Json.Schema.Generation.Intents;

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Represents an <see cref="ISchemaRefiner"/> implementation used to refine schemas by adding xml-based descriptions
/// </summary>
public class XmlDocumentationJsonSchemaRefiner
    : ISchemaRefiner
{

    /// <inheritdoc/>
    public virtual bool ShouldRun(SchemaGenerationContextBase context) => context.Intents.OfType<PropertiesIntent>().Any();

    /// <inheritdoc/>
    public virtual void Run(SchemaGenerationContextBase context)
    {
        var propertiesIntent = context.Intents.OfType<PropertiesIntent>().First();
        foreach (var (propertyName, propertyContext) in propertiesIntent.Properties)
        {
            var property = context.Type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property == null) continue;
            try
            {
                var description = XmlDocumentationHelper.SummaryOf(property);
                if (string.IsNullOrEmpty(description)) continue;
                var clonedContext = CloneSchemaContext(propertyContext);
                clonedContext.Intents.Add(new DescriptionIntent(description));
                propertiesIntent.Properties[propertyName] = clonedContext;
            }
            catch { }
        }
    }

    SchemaGenerationContextBase CloneSchemaContext(SchemaGenerationContextBase originalContext)
    {
        var clonedContext = (TypeGenerationContext)Activator.CreateInstance(typeof(TypeGenerationContext), BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly, null, [originalContext.Type], null)!;
        foreach (var intent in originalContext.Intents) clonedContext.Intents.Add(intent);
        return clonedContext;
    }

}
