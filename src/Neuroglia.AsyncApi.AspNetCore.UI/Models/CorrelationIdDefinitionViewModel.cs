using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record CorrelationIdDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public CorrelationIdDefinitionViewModel(AsyncApiDocument document, CorrelationIdDefinition correlationId) : base(document) { this.CorrelationId = correlationId; }

    public CorrelationIdDefinition CorrelationId { get; }


}
