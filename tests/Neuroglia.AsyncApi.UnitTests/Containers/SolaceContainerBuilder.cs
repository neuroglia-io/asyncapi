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

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Neuroglia.AsyncApi.UnitTests.Containers;

public static class SolaceContainerBuilder
{

    public const int PublicPort = 6650;

    public static IContainer Build()
    {
        return new ContainerBuilder()
            .WithName($"solace-{Guid.NewGuid():N}")
            .WithImage("solace/solace-pubsub-standard")
            .WithPortBinding(PublicPort, true)
            .WithEnvironment("username_admin_globalaccesslevel", "admin")
            .WithEnvironment("username_admin_password", "admin")
            .WithEnvironment("system_scaling_maxconnectioncount", "100")
            .WithWaitStrategy(Wait
                .ForUnixContainer()
                .UntilMessageIsLogged("")) //todo
            .Build();
    }

}
