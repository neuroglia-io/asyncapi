namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record SecuritySchemeDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public SecuritySchemeDefinitionViewModel(AsyncApiDocument document, string parentRef, SecuritySchemeDefinition scheme) : base(document) { this.ParentRef = parentRef; this.Scheme = scheme; }

    public string ParentRef { get; }

    public SecuritySchemeDefinition Scheme { get; }

}