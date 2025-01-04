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
/// Represents the base class for all <see cref="IAsyncApiOperationResult"/> implementations
/// </summary>
public abstract class AsyncApiOperationResult
    : IAsyncApiOperationResult
{

    bool _disposed;

    /// <inheritdoc/>
    public abstract bool IsSuccessful { get; }

    /// <inheritdoc/>
    public abstract Stream? PayloadStream { get; }

    /// <summary>
    /// Disposes of the <see cref="AsyncApiOperationResult"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="AsyncApiOperationResult"/> is beings disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (PayloadStream != null) PayloadStream.Dispose();
            }
            _disposed = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the <see cref="AsyncApiOperationResult"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="AsyncApiOperationResult"/> is beings disposed of</param>
    public virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (PayloadStream != null) await PayloadStream.DisposeAsync().ConfigureAwait(false);
            }
            _disposed = true;
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true);
        GC.SuppressFinalize(this);
    }

}
