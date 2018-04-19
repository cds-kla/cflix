using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CFlix.ActiveDirectory.NETStandard
{
    public static class ActiveDirectoryExtension
    {
        public static void AddActiveDirectory(this IServiceCollection provider, string connectionString)
        {
            provider.AddScoped<ActiveDirectory>(p => new ActiveDirectory(connectionString));
        }
    }
}
