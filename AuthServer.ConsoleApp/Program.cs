using System;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace AuthServer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true).Build();

            Console.WriteLine("Choose: 1: ClientCredentials, 2: ResourceOwnerPassword");
            var flow = Console.ReadKey();
            if (flow.KeyChar == '1')
            {
                var discovery = DiscoveryClient.GetAsync(configuration["Authority"]).Result;
                if (discovery.IsError)
                {
                    Console.WriteLine(discovery.Error);
                    return;
                }

                var tokenClient = new TokenClient(discovery.TokenEndpoint, configuration["ClientId"], configuration["ClientSecret"]);
                var tokenResponse = tokenClient.RequestClientCredentialsAsync(String.Empty).Result;

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);
                Console.ReadKey();
            }
            else if (flow.KeyChar == '2')
            {
                var discovery = DiscoveryClient.GetAsync(configuration["Authority"]).Result;
                if (discovery.IsError)
                {
                    Console.WriteLine(discovery.Error);
                    return;
                }

                var tokenClient = new TokenClient(discovery.TokenEndpoint, configuration["ClientId"],
                    configuration["ClientSecret"]);
                var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("a@a.com", "Test123!").Result;

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);
                Console.ReadKey();
            }
        }
    }
}
