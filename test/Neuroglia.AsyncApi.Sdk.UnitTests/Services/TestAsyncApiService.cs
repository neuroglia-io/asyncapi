using Neuroglia.AsyncApi.UnitTests.Data;
using System;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Services
{

    [AsyncApi("test", "1.0.0")]
    public class TestAsyncApiService
    {

        /// <summary>
        /// A test MQTT subscribe operation
        /// </summary>
        /// <param name="message">The message to</param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        [SubscribeOperation, Channel("test-channel")]
        public async Task SubscribeAsync(TestMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

        }

        /// <summary>
        /// A test MQTT publish operation
        /// </summary>
        /// <param name="message">The message to</param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        [PublishOperation, Channel("test-channel")]
        public async Task PublishAsync(TestMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

        }

    }

}
