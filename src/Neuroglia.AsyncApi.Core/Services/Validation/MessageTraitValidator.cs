using FluentValidation;
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Services.Validation
{

    /// <summary>
    /// Represents the service used to validate <see cref="MessageTraitDefinition"/>s
    /// </summary>
    public class MessageTraitValidator
        : AbstractValidator<MessageTraitDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="MessageTraitValidator"/>
        /// </summary>
        public MessageTraitValidator()
        {
            
        }

    }

}
