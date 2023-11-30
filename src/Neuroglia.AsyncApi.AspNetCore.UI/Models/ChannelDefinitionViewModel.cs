namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

public record ChannelDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    public ChannelDefinitionViewModel(AsyncApiDocument document, ChannelDefinition channel) : base(document) => this.Channel = channel;

    public ChannelDefinition Channel { get; }

}
