using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an <see cref="Attribute"/> used to mark a method as an <see cref="OperationDefinition"/> of type <see cref="OperationType.Subscribe"/>
/// </summary>
public class SubscribeOperationAttribute
    : OperationAttribute
{
    
    /// <summary>
    /// Initializes a new <see cref="SubscribeOperationAttribute"/>
    /// </summary>
    /// <param name="messageType">The <see cref="OperationDefinition"/>'s message type</param>
    public SubscribeOperationAttribute(Type messageType) : base(OperationType.Subscribe, messageType) { }

    /// <summary>
    /// Initializes a new <see cref="OperationAttribute"/>
    /// </summary>
    public SubscribeOperationAttribute() : base(OperationType.Subscribe) { }

}
