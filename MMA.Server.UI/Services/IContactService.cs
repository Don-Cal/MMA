using MMA.Server.UI.Services.Base;

namespace MMA.Server.UI.Services
{
  public interface IContactService
  {
    Task<Response<List<DtoContactOnly>>> Get();
    Task<Response<DtoContactOnly>> Get(int id);
    Task<Response<DtoContactUpdate>> GetForUpdate(int id);
    Task<Response<int>> Create(DtoContactCreate contact);
    Task<Response<int>> Edit(int id, DtoContactUpdate contact);
    Task<Response<int>> Delete(int id);
  }

}