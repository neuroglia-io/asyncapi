using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record AsyncApiDocumentViewModel
{

    public AsyncApiDocumentViewModel(AsyncApiDocument document) => this.Document = document;

    public AsyncApiDocument Document { get; }

}