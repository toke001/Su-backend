using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IRefs
    {        
        public Task<List<RefRoleDto>> GetRefRolesList();
        public Dictionary<int, string> GetDataTypes();
        #region Universal_Refferences
        Task<List<RefUniverRefDto>> GetRefUniverList();
        Task<RefUniverRefDto> GetRefUniverById(Guid Id);
        Task<Universal_Refference> AddRefUniver(Universal_Refference model);
        Task<RefUniverRefDto> UpdateRefUniver(Universal_Refference model);
        Task<RefUniverRefDto> DeleteRefUniver(Guid id);

        #endregion Universal_Refferences

        Task<List<RefBusinesDictDto>> GetBusinesDictList(Guid? parentId, string? type);
        Task<RefBusinesDictDto> GetBusinesDictById(Guid Id);
        Task<Business_Dictionary> AddBusinesDict(Business_Dictionary model);
        Task<RefBusinesDictDto> UpdateBusinesDict(Business_Dictionary model);
        Task<RefBusinesDictDto> DeleteBusinesDict(Guid id);
    }
}
