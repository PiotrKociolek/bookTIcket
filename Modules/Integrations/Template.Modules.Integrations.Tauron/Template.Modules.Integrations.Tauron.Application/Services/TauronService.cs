using AutoMapper;
using Template.Modules.Integrations.Tauron.Application.Clients;
using Template.Modules.Integrations.Tauron.Application.Dto;
using Template.Modules.Integrations.Tauron.Infrastructure.Helpers;
using Template.Modules.Notifications.Application.Dto.EmailModels;
using Template.Modules.Notifications.Application.Services;
using Template.Modules.Shared.Application.Settings;
using Template.Modules.Shared.Application.Extensions;
using Microsoft.Extensions.Logging;
using RestEase;

namespace Template.Modules.Integrations.Tauron.Application.Services
{
    public class TauronService : ITauronService
    {
        private readonly ITauronClient _tauronClient;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly ILogger<TauronService> _logger;
        private readonly ExternalServiceSettings _externalServiceSettings;
        private const string CITY_NAME = "Nowy Sącz";
        private const string PROVIDER_NAME = "TAURON";

        public TauronService(
            ITauronClient tauronClient,
            IMapper mapper,
            INotificationService notificationService,
            ILogger<TauronService> logger,
            ExternalServiceSettings externalServiceSettings)
        {
            _tauronClient = tauronClient;
            _mapper = mapper;
            _notificationService = notificationService;
            _logger = logger;
            _externalServiceSettings = externalServiceSettings;
        }

        public async Task<ICollection<OutageDto>> GetOutageItemsAsync(string streetName, int houseNumber, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            var timestamp = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();
            try
            {
                _logger.LogTrace(
                    "Sending request to external provider {PROVIDER_NAME}, with street name: {StreetName}, house number: {HouseNumber}, for date range: {From} to {To}",
                    PROVIDER_NAME,
                    streetName,
                    houseNumber,
                    from,
                    to);

                var cityResponse = (await _tauronClient.GetCityInfoAsync(CITY_NAME, timestamp, cancellationToken))
                    .FirstOrDefault(x => x.Name == CITY_NAME);
                if (cityResponse is null)
                {
                    return Array.Empty<OutageDto>();
                }

                // Following request flow from tauron website
                timestamp = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();
                await _tauronClient.OnlyStreetsAsync(cityResponse.GAID, timestamp, cancellationToken);

                timestamp = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var streetResponse = (await _tauronClient.GetStreetInfoAsync(streetName, cityResponse.GAID, timestamp, cancellationToken)).FirstOrDefault();

                if (streetResponse is null)
                {
                    return Array.Empty<OutageDto>();
                }

                timestamp = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();
                await _tauronClient.HouseNumbersAsync(houseNumber, cityResponse.GAID, streetResponse.GAID, timestamp, cancellationToken);

                timestamp = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var result = await _tauronClient.GetOutagesAsync(
                    cityResponse.GAID,
                    streetResponse.GAID,
                    houseNumber,
                    from.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"),
                    to.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"),
                    timestamp,
                    cancellationToken);
                var mappedResult = _mapper.Map<List<OutageDto>>(result.OutageItems);
                TauronResponseDataTransformation.TrimCityPrefix(mappedResult);
                return mappedResult;
            }
            catch (ApiException e)
            {
                if (e.StatusCode is not System.Net.HttpStatusCode.OK)
                {
                    var emailAdress = _externalServiceSettings.ErrorMail;
                    _logger.LogError(
                        "Got invalid response from external provider: {PROVIDER_NAME}, with following trace: {ErrorTrace}",
                        PROVIDER_NAME,
                        e.Content);
                    var exceptionMessages = e.GetInnerMessages();

                    await _notificationService.ExternalServiceFailureAsync(emailAdress, new ExternalServiceFailureModelDto
                    {
                        Provider = PROVIDER_NAME,
                        Content = e.Content,
                        StatusCode = e.StatusCode.ToString(),
                        ErrorMessages = exceptionMessages
                    });
                }
                throw;
            }
        }
    }
}
