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

using Microsoft.Extensions.Hosting;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Configuration;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.FluentBuilders;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Client;

public abstract class BindingHandlerTestsBase
    : IAsyncLifetime
{

    public BindingHandlerTestsBase(Action<IAsyncApiClientOptionsBuilder> setup, Action<IServiceCollection>? serviceConfiguration = null)
    {
        var services = new ServiceCollection();
        services.AddAsyncApi();
        services.AddAsyncApiClient(setup);
        serviceConfiguration?.Invoke(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    protected ServiceProvider ServiceProvider;

    protected IAsyncApiDocumentBuilder DocumentBuilder => ServiceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();

    protected IAsyncApiClientFactory ClientFactory => ServiceProvider.GetRequiredService<IAsyncApiClientFactory>();

    public async Task InitializeAsync()
    {
        foreach (var hostedService in ServiceProvider.GetServices<IHostedService>()) await hostedService.StartAsync(CancellationToken.None);
    }

    public async Task DisposeAsync()
    {
        await ServiceProvider.DisposeAsync();
    }

}
