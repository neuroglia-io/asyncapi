using FluentValidation;
using Neuroglia.AsyncApi.Sdk.Models;

namespace Neuroglia.AsyncApi.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the service used to validate <see cref="MessageTrait"/>s
    /// </summary>
    public class MessageTraitValidator
        : AbstractValidator<MessageTrait>
    {

        /// <summary>
        /// Initializes a new <see cref="MessageTraitValidator"/>
        /// </summary>
        public MessageTraitValidator()
        {
            
        }

    }

}
