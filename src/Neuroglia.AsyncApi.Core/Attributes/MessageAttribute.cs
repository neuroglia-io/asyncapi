using Neuroglia.AsyncApi.Specification.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an <see cref="Attribute"/> used to configure an <see cref="OperationDefinition"/>'s <see cref="MessageDefinition"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class MessageAttribute
    : Attribute
{
    
    /// <summary>
    /// Gets/sets the <see cref="MessageDefinition"/>'s name
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="MessageDefinition"/>'s title
    /// </summary>
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="MessageDefinition"/>'s description
    /// </summary>
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="MessageDefinition"/>'s summary
    /// </summary>
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="MessageDefinition"/>'s content type
    /// </summary>
    public virtual string? ContentType { get; set; }

}
