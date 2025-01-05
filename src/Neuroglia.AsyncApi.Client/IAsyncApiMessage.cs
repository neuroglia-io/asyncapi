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

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Defines the fundamentals of an inbound message produced by an AsyncAPI application
/// </summary>
public interface IAsyncApiMessage
{

    /// <summary>
    /// Gets the message's content type
    /// </summary>
    string ContentType { get; }

    /// <summary>
    /// Gets the message's payload, if any
    /// </summary>
    object? Payload { get; }

    /// <summary>
    /// Gets the message's headers, if any
    /// </summary>
    object? Headers { get; }

    /// <summary>
    /// Gets the message's correlation id, if any
    /// </summary>
    string? CorrelationId { get; }

}
