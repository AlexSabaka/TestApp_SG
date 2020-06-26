using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppApi.Client;
using AppApi.Data.Entities;

using Refit;

namespace AppClient
{
    public partial class App
    {
        public class TestItemsService : IItemsService
        {
            List<ProductItem> _items = new List<ProductItem>
                {
                    new ProductItem { Description = "1", Available = true, Name = "XX", Price = 100 },
                    new ProductItem { Description = "2", Available = true, Name = "XY", Price = 470 },
                    new ProductItem { Description = "3", Available = true, Name = "YX", Price = 330 },
                    new ProductItem { Description = "4", Available = true, Name = "YY", Price = 250 },
                    new ProductItem { Description = "5", Available = true, Name = "XZ", Price = 111 },
                    new ProductItem { Description = "6", Available = true, Name = "ZX", Price = 999 },
                    new ProductItem { Description = "7", Available = true, Name = "ZZ", Price = 752 },
                };

            public Task<IEnumerable<ProductItem>> Get()
            {
                return Task.FromResult((IEnumerable<ProductItem>)_items);
            }

            public Task<ProductItem> Get([AliasAs("id")] int id)
            {
                return Task.FromResult(_items.SingleOrDefault(x => x.Id == id));
            }
        }
    }
}
