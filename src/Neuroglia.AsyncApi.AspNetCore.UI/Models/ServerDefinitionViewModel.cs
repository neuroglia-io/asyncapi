using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record ServerDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public ServerDefinitionViewModel(AsyncApiDocument document, string key, ServerDefinition server) : base(document) { this.Key = key; this.Server = server; }

    public string Key { get; }

    public ServerDefinition Server { get; }

}
