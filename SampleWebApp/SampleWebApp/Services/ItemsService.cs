using SampleWebApp.Models;
using SampleWebApp.Services.Contracts;

namespace SampleWebApp.Services
{
    public class ItemService : IItemService
    {
        private List<Item> _items = new List<Item> { 
            new Item{ Id = 1, Name = "Item1", Description = "This is Item 1", Quantity = 10, Price = 20 },
            new Item{ Id = 2, Name = "Item2", Description = "This is Item 2", Quantity = 5, Price = 30 },
            new Item{ Id = 3, Name = "Item3", Description = "This is Item 3", Quantity = 15, Price = 45 },
            new Item{ Id = 4, Name = "Item4", Description = "This is Item 4", Quantity = 20, Price = 15 }
        };
        public async Task<Item> Get(int id)
        {
            return await Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await Task.FromResult(_items);
        }
    }
}
