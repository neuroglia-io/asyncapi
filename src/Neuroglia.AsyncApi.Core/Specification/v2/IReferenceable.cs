namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Defines the fundamentals of a referenceable component
/// </summary>
public interface IReferenceable
{

    /// <summary>
    /// Gets the <see cref="IReferenceable"/>'s reference, if any
    /// </summary>
    string? Reference { get; }

}