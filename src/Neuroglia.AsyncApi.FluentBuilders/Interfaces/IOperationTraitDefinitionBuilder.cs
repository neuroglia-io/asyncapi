namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="OperationTraitDefinition"/>s
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="OperationTraitDefinition"/> to build</typeparam>
public interface IOperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TBuilder : IOperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : OperationTraitDefinition
{

    /// <summary>
    /// Configures the <see cref="OperationTraitDefinition"/> to use the specified id
    /// </summary>
    /// <param name="operationId">The id of the Async Api document to build</param>
    /// <returns>The configured <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithOperationId(string operationId);

    /// <summary>
    /// Configures the <see cref="OperationTraitDefinition"/> to use the specified API description
    /// </summary>
    /// <param name="summary">The summary of the Async Api document to build</param>
    /// <returns>The configured <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithSummary(string summary);

    /// <summary>
    /// Configures the <see cref="OperationTraitDefinition"/> to use the specified API description
    /// </summary>
    /// <param name="description">The description of the Async Api document to build</param>
    /// <returns>The configured <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithDescription(string description);

    /// <summary>
    /// Marks the <see cref="OperationTraitDefinition"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithTag(Action<ITagDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified external documentation to the <see cref="OperationTraitDefinition"/> to build
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
    /// <param name="description">The description of the documentation to add</param>
    /// <returns>The configured <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithExternalDocumentation(Uri uri, string? description = null);

    /// <summary>
    /// Adds the specified <see cref="IOperationBindingDefinition"/> to the <see cref="OperationTraitDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IOperationBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithBinding(IOperationBindingDefinition binding);

    /// <summary>
    /// Builds a new <see cref="OperationTraitDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="OperationTraitDefinition"/></returns>
    TTrait Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="OperationTraitDefinition"/>s
/// </summary>
public interface IOperationTraitBuilder
    : IOperationTraitDefinitionBuilder<IOperationTraitBuilder, OperationTraitDefinition>
{



}
