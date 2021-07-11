using FluentValidation;
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="OperationTrait"/>s
    /// </summary>
    public class OperationTraitValidator
        : AbstractValidator<OperationTrait>
    {

        /// <summary>
        /// Initializes a new <see cref="OperationTraitValidator"/>
        /// </summary>
        public OperationTraitValidator()
        {

        }

    }

}
