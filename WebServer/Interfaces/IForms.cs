using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IForms
    {
        public Task<List<ApprovedForm>> GetForms();
        public Task<ApprovedForm> Add(ApprovedForm aForm);
        public Task<ApprovedForm> Update(ApprovedForm aForm);
        public Task<ApprovedForm> Delete(Guid id);
        //public Task<FormDto> AddForm(FormsAddDto row);

        //#region водоснабжение
        //public Task<List<SupplyCityForm1TableDto>> SupplyCityGetForm1(Guid id);
        //public Task<List<SupplyCityForm1TableDto>> SupplyCityUpdateForm1(List<SupplyCityForm1TableDto> list, Guid id);
        //public Task<List<SupplyCityForm2TableDto>> SupplyCityGetForm2(Guid id);
        //public Task<List<SupplyCityForm2TableDto>> SupplyCityUpdateForm2(List<SupplyCityForm2TableDto> list, Guid id);
        //public Task<List<SupplyCityForm3TableDto>> SupplyCityGetForm3(Guid id);
        //public Task<List<SupplyCityForm3TableDto>> SupplyCityUpdateForm3(List<SupplyCityForm3TableDto> list, Guid id);
        //public Task<List<SupplyCityForm4TableDto>> SupplyCityGetForm4(Guid id);
        //public Task<List<SupplyCityForm4TableDto>> SupplyCityUpdateForm4(List<SupplyCityForm4TableDto> list, Guid id);
        //public Task<List<SupplyCityForm5TableDto>> SupplyCityGetForm5(Guid id);
        //public Task<List<SupplyCityForm5TableDto>> SupplyCityUpdateForm5(List<SupplyCityForm5TableDto> list, Guid id);
        //#endregion

        //#region Водоотведение
        //public Task<List<WasteCityForm1TableDto>> WasteCityGetForm1(Guid id);
        //public Task<List<WasteCityForm1TableDto>> WasteCityUpdateForm1(List<WasteCityForm1TableDto> list, Guid id);
        //public Task<List<WasteCityForm2TableDto>> WasteCityGetForm2(Guid id);
        //public Task<List<WasteCityForm2TableDto>> WasteCityUpdateForm2(List<WasteCityForm2TableDto> list, Guid id);
        //public Task<List<WasteCityForm3TableDto>> WasteCityGetForm3(Guid id);
        //public Task<List<WasteCityForm3TableDto>> WasteCityUpdateForm3(List<WasteCityForm3TableDto> list, Guid id);
        //#endregion
    }
}
