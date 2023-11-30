namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the base class for all <see cref="IOperationTraitBuilder"/> implementations
/// </summary>
/// <remarks>
/// Initializes a new <see cref="OperationTraitBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="OperationTraitDefinition"/>s</param>
public class OperationTraitBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<OperationTraitDefinition>> validators)
    : OperationTraitDefinitionBuilder<IOperationTraitBuilder, OperationTraitDefinition>(serviceProvider, validators), IOperationTraitBuilder
{
}
