using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Redis;

/// <summary>
/// Represents the object used to configure a <see href="https://redis.io/">Redis</see> operation binding
/// </summary>
[DataContract]
public record RedisOperationBindingDefinition
    : RedisBindingDefinition, IOperationBindingDefinition
{



}
