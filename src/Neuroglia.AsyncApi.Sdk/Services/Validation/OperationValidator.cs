using FluentValidation;
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="Operation"/>s
    /// </summary>
    public class OperationValidator
        : AbstractValidator<Operation>
    {

        /// <summary>
        /// Initializes a new <see cref="OperationValidator"/>
        /// </summary>
        public OperationValidator()
        {

        }

    }

}
