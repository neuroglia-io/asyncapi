namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record OperationDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public OperationDefinitionViewModel(AsyncApiDocument document, string channelKey, OperationType operationType, OperationDefinition operation) : base(document) { this.ChannelKey = channelKey; this.OperationType = operationType; this.Operation = operation; }

    public string ChannelKey { get; }

    public OperationType OperationType { get; }

    public OperationDefinition Operation { get; }

}
