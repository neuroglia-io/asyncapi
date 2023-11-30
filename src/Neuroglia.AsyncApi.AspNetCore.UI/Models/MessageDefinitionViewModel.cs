namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record MessageDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public MessageDefinitionViewModel(AsyncApiDocument document, int index, string operationRef, MessageDefinition definition)
        : base(document)
    {
        this.Index = index;
        this.OperationRef = operationRef;
        this.Definition = definition;
    }

    public int Index { get; }

    public string OperationRef { get; }

    public MessageDefinition Definition { get; }

}
