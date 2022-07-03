using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace stock_quote_alert.Configuration
{
    public sealed class SecretConfiguration
    {
        private static IConfiguration configuration { get; set; }
        private static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();
            configuration = builder.Build();
        }
        public static IConfiguration GetInstance()
        {
            if (configuration == null)
                BuildConfiguration();
            return configuration;
        }
    }
}
