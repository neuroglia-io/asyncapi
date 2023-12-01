using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an <see cref="Attribute"/> used to mark a method as an <see cref="OperationDefinition"/> of type <see cref="OperationType.Publish"/>
/// </summary>
public class PublishOperationAttribute
    : OperationAttribute
{

    /// <summary>
    /// Initializes a new <see cref="PublishOperationAttribute"/>
    /// </summary>
    /// <param name="messageType">The <see cref="OperationDefinition"/>'s message type</param>
    public PublishOperationAttribute(Type messageType) : base(OperationType.Publish, messageType) { }

    /// <summary>
    /// Initializes a new <see cref="PublishOperationAttribute"/>
    /// </summary>
    public PublishOperationAttribute() : base(OperationType.Publish) { }

}
