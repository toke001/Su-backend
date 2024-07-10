using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IRefKato
    {
        public Task<List<RefKatoTreeDto>> GetRefKatoAsync(int parentId, bool? getNested);
        public Task<List<Ref_Street>> GetRefStreetByKatoId(int id);
        public Task<List<Ref_Building>> GetRefBuildingByStreetId(int id);
        public Task<Ref_Street> AddStreet(Ref_Street row);
        public Task<Ref_Street> UpdateStreet(Ref_Street row, int id);
        public Task DeleteStreet(int id);
        public Task<Ref_Building> AddBuilding(Ref_Building row);
        public Task<Ref_Building> UpdateBuilding(Ref_Building row, int id);
        public Task DeleteBuilding(int id);
        public Task<bool> IsReportable(int id);
    }
}
