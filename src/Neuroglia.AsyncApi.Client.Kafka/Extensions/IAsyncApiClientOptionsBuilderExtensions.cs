using Neuroglia.AsyncApi.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi
{

    public static class IAsyncApiClientOptionsBuilderExtensions
    {

        public static IAsyncApiClientOptionsBuilder AddKafka(this IAsyncApiClientOptionsBuilder builder)
        {

            return builder;
        }

    }

}
