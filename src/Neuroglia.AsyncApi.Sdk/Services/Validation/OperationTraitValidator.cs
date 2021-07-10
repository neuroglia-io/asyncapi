using FluentValidation;
using Neuroglia.AsyncApi.Sdk.Models;

namespace Neuroglia.AsyncApi.Sdk.Services.Validation
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
