using Neuroglia.AsyncApi.Specification.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an <see cref="Attribute"/> used to mark a method as an <see cref="OperationDefinition"/>
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public abstract class OperationAttribute
    : Attribute
{

    /// <summary>
    /// Initializes a new <see cref="OperationAttribute"/>
    /// </summary>
    /// <param name="operationType">The <see cref="OperationDefinition"/>'s type</param>
    /// <param name="messageType">The <see cref="OperationDefinition"/>'s message type</param>
    protected OperationAttribute(OperationType operationType, Type? messageType)
    {
        this.OperationType = operationType;
        this.MessageType = messageType;
    }

    /// <summary>
    /// Initializes a new <see cref="OperationAttribute"/>
    /// </summary>
    /// <param name="operationType">The <see cref="OperationDefinition"/>'s type</param>
    protected OperationAttribute(OperationType operationType) : this(operationType, null) { }

    /// <summary>
    /// Gets the <see cref="OperationDefinition"/>'s type
    /// </summary>
    public virtual OperationType OperationType { get; }

    /// <summary>
    /// Gets the <see cref="OperationDefinition"/>'s message type
    /// </summary>
    public virtual Type? MessageType { get; }

    /// <summary>
    /// Gets/sets the <see cref="OperationDefinition"/>'s operation id
    /// </summary>
    public virtual string? OperationId { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="OperationDefinition"/>'s summary
    /// </summary>
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="OperationDefinition"/>'s summary
    /// </summary>
    public virtual string? Description { get; set; }

}
