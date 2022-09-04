using AutoMapper;
using Blazored.LocalStorage;
using MMA.Server.UI.Services.Base;

namespace MMA.Server.UI.Services
{
  public class ClientService : BaseHttpService, IClientService
  {
    private readonly IClient client;
    private readonly IMapper mapper;

    public ClientService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
    {
      this.client = client;
      this.mapper = mapper;
    }

    public async Task<Response<int>> Create(DtoClientCreate cli)
    {
      Response<int> response = new();

      try
      {
        await GetBearerToken();
        await client.ClientsPOSTAsync(cli);
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<int>(exception);
      }

      return response;
    }

    public async Task<Response<int>> Delete(int id)
    {
      Response<int> response = new();

      try
      {
        await GetBearerToken();
        await client.ClientsDELETEAsync(id);
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<int>(exception);
      }

      return response;
    }

    public async Task<Response<int>> Edit(int id, DtoClientUpdate dtoclient)
    {
      Response<int> response = new();

      try
      {
        await GetBearerToken();
        await client.ClientsPUTAsync(id, dtoclient);
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<int>(exception);
      }

      return response;
    }

    public async Task<Response<DtoClient>> Get(int id)
    {
      Response<DtoClient> response;

      try
      {
        await GetBearerToken();
        var data = await client.ClientsGETAsync(id);
        response = new Response<DtoClient>
        {
          Data = data,
          Success = true
        };
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<DtoClient>(exception);
      }

      return response;
    }

    public async Task<Response<List<DtoClientOnly>>> Get()
    {
      Response<List<DtoClientOnly>> response;

      try
      {
        await GetBearerToken();
        var data = await client.ClientsAllAsync();
        response = new Response<List<DtoClientOnly>>
        {
          Data = data.ToList(),
          Success = true
        };
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<List<DtoClientOnly>>(exception);
      }

      return response;
    }

    public async Task<Response<DtoClientUpdate>> GetForUpdate(int id)
    {
      Response<DtoClientUpdate> response;

      try
      {
        await GetBearerToken();

        var data = await client.ClientsGETAsync(id);

        DtoClientUpdate dtoUpdate = new DtoClientUpdate();
        dtoUpdate.AccountNo = data.AccountNo;
        dtoUpdate.Address1 = data.Address1;
        dtoUpdate.Address2 = data.Address2;
        dtoUpdate.Address3 = data.Address3;
        dtoUpdate.ApplyDiscount = data.ApplyDiscount;
        dtoUpdate.City = data.City;
        dtoUpdate.ClientName = data.ClientName;
        dtoUpdate.Country = data.Country;
        dtoUpdate.Currency = data.Currency;
        dtoUpdate.DiscountFee = data.DiscountFee;
        dtoUpdate.EmailAddress = data.EmailAddress;
        dtoUpdate.Id = data.Id;
        dtoUpdate.PaymentTerms = data.PaymentTerms;
        dtoUpdate.Phone = data.Phone;
        dtoUpdate.Postcode = data.Postcode;
        dtoUpdate.Region = data.Region;
        dtoUpdate.RegNo = data.RegNo;
        dtoUpdate.State = data.State;
        dtoUpdate.Type = data.Type;
        dtoUpdate.VatNo = data.VatNo;

        response = new Response<DtoClientUpdate>
        {
          Data = dtoUpdate, //mapper.Map<DtoClientUpdate>(data),
          Success = true
        };
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<DtoClientUpdate>(exception);
      }

      return response;
    }
  }


}
