using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record BindingDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public BindingDefinitionViewModel(AsyncApiDocument document, IBindingDefinition binding, string parentRef) : base(document) { this.Binding = binding; this.ParentRef = parentRef; }

    public IBindingDefinition Binding { get; }

    public string ParentRef { get; }

}
