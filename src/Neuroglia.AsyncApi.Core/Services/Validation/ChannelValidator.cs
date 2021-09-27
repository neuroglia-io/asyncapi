using FluentValidation;
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="ChannelDefinition"/>s
    /// </summary>
    public class ChannelValidator
        : AbstractValidator<ChannelDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelValidator"/>
        /// </summary>
        public ChannelValidator()
        {

        }

    }

}
