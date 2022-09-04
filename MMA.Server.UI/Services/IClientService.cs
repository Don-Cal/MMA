using MMA.Server.UI.Services.Base;

namespace MMA.Server.UI.Services
{
  public interface IClientService
    {
        Task<Response<List<DtoClientOnly>>> Get();
        Task<Response<DtoClient>> Get(int id);
        Task<Response<DtoClientUpdate>> GetForUpdate(int id);
        Task<Response<int>> Create(DtoClientCreate client);
        Task<Response<int>> Edit(int id, DtoClientUpdate client);
        Task<Response<int>> Delete(int id);
    }

}