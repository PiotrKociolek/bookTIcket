using Template.Modules.Integrations.Tauron.Core.Responses;
using RestEase;

namespace Template.Modules.Integrations.Tauron.Application.Clients
{
    public interface ITauronClient
    {
        [Get("enum/geo/cities")]
        Task<ICollection<GetCityGAIDResponse>> GetCityInfoAsync([Query("partName")]string cityName, [Query("_")]ulong timestamp, CancellationToken cancellationToken); 

        [Get("enum/geo/onlyonestreet")]
        Task OnlyStreetsAsync([Query("ownerGAID")]int ownerId, [Query("_")]ulong timestamp, CancellationToken cancellationToken); 

        [Get("enum/geo/streets")]
        Task<ICollection<GetStreetGAIDResponse>> GetStreetInfoAsync([Query("partName")]string streetName, [Query("ownerGAID")]int ownerId, [Query("_")]ulong timestamp, CancellationToken cancellationToken); 

        [Get("enum/geo/housenumbers")]
        Task HouseNumbersAsync([Query("partName")]int houseNumber, [Query("cityGAID")]int cityId, [Query("streetGAID")]int streetId, [Query("_")]ulong timestamp, CancellationToken cancellationToken);

        [Get("outages/address")]
        Task<OutagesResponse> GetOutagesAsync([Query("cityGAID")] int cityId,
                                              [Query("streetGAID")] int streetId,
                                              [Query("houseNo")] int houseNumber,
                                              [Query("fromDate")] string from,
                                              [Query("toDate")] string to,
                                              [Query] ulong timestamp,
                                              CancellationToken cancellation);
    }
}
