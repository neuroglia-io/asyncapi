namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record SecurityRequirementViewModel
    : AsyncApiDocumentViewModel
{

    public SecurityRequirementViewModel(AsyncApiDocument document, string parentRef, string key, object? requirement) : base(document) { this.ParentRef = parentRef; this.Key = key; this.Requirement = requirement; }

    public string ParentRef { get; }

    public string Key { get; }

    public object? Requirement { get; }

}
