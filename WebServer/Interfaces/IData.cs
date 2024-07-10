using WebServer.Dtos;

namespace WebServer.Interfaces
{
    public interface IData
    {
        /// <summary>
        /// id:ИД Формы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<DataTableDto>> Get(Guid id);
        public Task<string> Update(List<DataTableDto> data);
    }
}
