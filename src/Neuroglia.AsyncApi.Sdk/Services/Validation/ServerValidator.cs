﻿using FluentValidation;
using Neuroglia.AsyncApi.Sdk.Models;

namespace Neuroglia.AsyncApi.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="Server"/>s
    /// </summary>
    public class ServerValidator
        : AbstractValidator<Server>
    {

        /// <summary>
        /// Initializes a new <see cref="ServerValidator"/>
        /// </summary>
        public ServerValidator()
        {
            this.RuleFor(s => s.Url)
                .NotNull();
            this.RuleFor(s => s.Protocol)
                .NotEmpty();
        }

    }

}