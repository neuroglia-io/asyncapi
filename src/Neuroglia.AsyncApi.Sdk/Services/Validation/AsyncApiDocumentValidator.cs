using FluentValidation;
using Neuroglia.AsyncApi.Sdk.Models;

namespace Neuroglia.AsyncApi.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the service used to validate <see cref="AsyncApiDocument"/>s
    /// </summary>
    public class AsyncApiDocumentValidator
        : AbstractValidator<AsyncApiDocument>
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiDocumentValidator"/>
        /// </summary>
        public AsyncApiDocumentValidator()
        {
            this.RuleFor(d => d.AsyncApi)
                .NotEmpty();
            this.RuleFor(d => d.Info)
                .NotNull();
            this.RuleFor(d => d.Channels)
                .NotEmpty();
            this.RuleForEach(d => d.Channels.Values)
                .SetValidator(new ChannelValidator())
                .When(d => d.Channels != null);
            this.RuleForEach(d => d.Servers.Values)
                .SetValidator(new ServerValidator())
                .When(d => d.Servers != null);
        }

    }

}
