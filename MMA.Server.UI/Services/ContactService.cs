using AutoMapper;
using Blazored.LocalStorage;
using MMA.Server.UI.Services.Base;

namespace MMA.Server.UI.Services
{
  public class ContactService : BaseHttpService, IContactService
  {
    private readonly IClient client;
    private readonly IMapper mapper;

    public ContactService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
    {
      this.client = client;
      this.mapper = mapper;
    }

    public async Task<Response<int>> Create(DtoContactCreate cli)
    {
      Response<int> response = new();

      try
      {
        await GetBearerToken();
        await client.ContactsPOSTAsync(cli);
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
        await client.ContactsDELETEAsync(id);
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<int>(exception);
      }

      return response;
    }

    public async Task<Response<int>> Edit(int id, DtoContactUpdate dtoclient)
    {
      Response<int> response = new();

      try
      {
        await GetBearerToken();
        await client.ContactsPUTAsync(id, dtoclient);
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<int>(exception);
      }

      return response;
    }

    public async Task<Response<DtoContactOnly>> Get(int id)
    {
      Response<DtoContactOnly> response;

      try
      {
        await GetBearerToken();
        var data = await client.ContactsGETAsync(id);
        response = new Response<DtoContactOnly>
        {
          Data = data,
          Success = true
        };
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<DtoContactOnly>(exception);
      }

      return response;
    }

    public async Task<Response<List<DtoContactOnly>>> Get()
    {
      Response<List<DtoContactOnly>> response;

      try
      {
        await GetBearerToken();
        var data = await client.ContactsAllAsync();
        response = new Response<List<DtoContactOnly>>
        {
          Data = data.ToList(),
          Success = true
        };
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<List<DtoContactOnly>>(exception);
      }

      return response;
    }

    public async Task<Response<DtoContactUpdate>> GetForUpdate(int id)
    {
      Response<DtoContactUpdate> response;

      try
      {
        await GetBearerToken();

        var data = await client.ContactsGETAsync(id);

        DtoContactUpdate dtoUpdate = new DtoContactUpdate();
        dtoUpdate.ContactType = data.ContactType;
        dtoUpdate.Title = data.Title;
        dtoUpdate.FullName = data.FullName;
        dtoUpdate.Phone = data.Phone;
        dtoUpdate.EmailAddress = data.EmailAddress;


        response = new Response<DtoContactUpdate>
        {
          Data = dtoUpdate, //mapper.Map<DtoContactUpdate>(data), 
          Success = true
        };
      }
      catch (ApiException exception)
      {
        response = ConvertApiExceptions<DtoContactUpdate>(exception);
      }

      return response;
    }
  }


}
