using FluentValidation;
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="Channel"/>s
    /// </summary>
    public class ChannelValidator
        : AbstractValidator<Channel>
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelValidator"/>
        /// </summary>
        public ChannelValidator()
        {

        }

    }

}
