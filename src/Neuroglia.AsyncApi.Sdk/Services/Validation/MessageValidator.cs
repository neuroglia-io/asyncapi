using FluentValidation;
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="Message"/>s
    /// </summary>
    public class MessageValidator
        : AbstractValidator<Message>
    {

        /// <summary>
        /// Initializes a new <see cref="MessageValidator"/>
        /// </summary>
        public MessageValidator()
        {

        }

    }

}
