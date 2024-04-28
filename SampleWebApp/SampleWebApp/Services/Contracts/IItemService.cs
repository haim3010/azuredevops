using SampleWebApp.Models;

namespace SampleWebApp.Services.Contracts
{
    public interface IItemService
    {
        public Task<Item> Get(int id);
        public Task<IEnumerable<Item>> GetAll();
    }
}
