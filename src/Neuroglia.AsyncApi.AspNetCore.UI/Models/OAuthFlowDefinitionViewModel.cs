namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record OAuthFlowDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public OAuthFlowDefinitionViewModel(AsyncApiDocument document, string parentRef, string flowType, OAuthFlowDefinition flow) : base(document) { this.ParentRef = parentRef; this.FlowType = flowType; this.Flow = flow; }

    public string ParentRef { get; }

    public string FlowType { get; }

    public OAuthFlowDefinition Flow { get; }

}