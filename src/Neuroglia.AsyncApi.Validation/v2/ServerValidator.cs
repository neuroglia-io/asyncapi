namespace Neuroglia.AsyncApi.Validation;

/// <summary>
/// Represents the service used to validate <see cref="ServerDefinition"/>s
/// </summary>
public class ServerValidator
    : AbstractValidator<ServerDefinition>
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
