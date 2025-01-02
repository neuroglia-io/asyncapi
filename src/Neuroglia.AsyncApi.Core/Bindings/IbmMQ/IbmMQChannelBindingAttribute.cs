// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Neuroglia.AsyncApi.Bindings.IbmMQ;

/// <summary>
/// Represents the attribute used to define an <see cref="IbmMQChannelBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class IbmMQChannelBindingAttribute(string name, string version = "latest")
    : ChannelBindingAttribute<IbmMQChannelBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets a logical group of IBM MQ server objects. This is necessary to specify multi-endpoint configurations used in high availability deployments. If omitted, the server object is not part of a group.
    /// </summary>
    public virtual IbmMQDestinationType DestinationType { get; init; }

    /// <summary>
    /// Gets/sets the name of the IBM MQ queue associated with the channel.
    /// </summary>
    public virtual string? QueueObjectName { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the queue is a cluster queue and therefore partitioned. If true, a binding option MAY be specified when accessing the queue. More information on binding options can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    public virtual bool? QueueIsPartitioned { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not it is recommended to open the queue exclusively.
    /// </summary>
    public virtual bool? QueueExclusive { get; init; }

    /// <summary>
    /// Gets/sets the value of the IBM MQ topic string to be used.
    /// </summary>
    public virtual string? TopicObjectName { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the subscription may be durable.
    /// </summary>
    public virtual bool? TopicDurablePermitted { get; init; }

    /// <summary>
    /// Gets/sets the last message published will be made available to new subscriptions.
    /// </summary>
    public virtual bool? TopicLastMsgRetained { get; init; }

    /// <summary>
    /// Gets/sets the maximum length of the physical message (in bytes) accepted by the Topic or Queue. Messages produced that are greater in size than this value may fail to be delivered. More information on the maximum message length can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    public virtual int? MaxMsgLength { get; init; }

    /// <inheritdoc/>
    public override IbmMQChannelBindingDefinition Build()
    {
        var binding = new IbmMQChannelBindingDefinition()
        {
            BindingVersion = this.Version,
            DestinationType = this.DestinationType,
            MaxMsgLength = this.MaxMsgLength
        };
        if (!string.IsNullOrEmpty(this.QueueObjectName)) binding.Queue = new()
        {
            ObjectName = this.QueueObjectName,
            IsPartitioned = this.QueueIsPartitioned ?? false,
            Exclusive = this.QueueExclusive ?? false
        };
        if (!string.IsNullOrWhiteSpace(this.TopicObjectName)) binding.Topic = new() 
        { 
            ObjectName = this.TopicObjectName,
            DurablePermitted = this.TopicDurablePermitted ?? false,
            LastMsgRetained = this.TopicLastMsgRetained ?? false
        };
        return binding;
    }

}
