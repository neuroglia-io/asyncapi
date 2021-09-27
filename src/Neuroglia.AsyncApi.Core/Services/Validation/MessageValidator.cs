using FluentValidation;
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="MessageDefinition"/>s
    /// </summary>
    public class MessageValidator
        : AbstractValidator<MessageDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="MessageValidator"/>
        /// </summary>
        public MessageValidator()
        {

        }

    }

}
