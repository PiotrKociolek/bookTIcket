using Template.Modules.Shared.Application.Settings;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace Template.Modules.Shared.Infrastructure.RestEase
{
    public class RestEaseClientBuilder
    {
        private readonly IServiceCollection _services;
        private readonly RestEaseSettings _settings;

        public RestEaseClientBuilder(IServiceCollection services, RestEaseSettings settings)
        {
            _services = services;
            _settings = settings;
        }

        public RestEaseClientBuilder AddClient<T>(string clientName) where T : class
        {
            var service = _settings
                .Services
                .FirstOrDefault(x => x.Name == clientName);

            if (service is null)
            {
                throw new Exception($"Configuration for service: '{typeof(T).Name}' not found.");
            }
            _services.AddRestEaseClient<T>(service.Url);

            return this;
        }

        public RestEaseClientBuilder AddClientWithRetries<T>(string clientName, TimeSpan retryDelay, int retryCount) where T : class
        {
            var service = _settings
                .Services
                .FirstOrDefault(x => x.Name == clientName);

            if (service is null)
            {
                throw new Exception($"Configuration for service: '{typeof(T).Name}' not found.");
            }
            _services.AddRestEaseClient<T>(service.Url)
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(retryDelay, retryCount)));

            return this;
        }
    }
}
