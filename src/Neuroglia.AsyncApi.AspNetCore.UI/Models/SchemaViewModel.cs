namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record SchemaViewModel
    : AsyncApiDocumentViewModel
{

    public SchemaViewModel(AsyncApiDocument document, JsonSchema schema) : base(document) => this.Schema = schema;

    public JsonSchema Schema { get; }

}
