using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppApi.Helpers;
using AppApi.Data;
using AppApi.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AppApi.Services
{
    public interface IItemService
    {
        IQueryable<ProductItem> GetAll();

        ProductItem Get(int id);
    }

    public class ItemService : IItemService
    {
        private IRepository<ProductItem> _itemsRepository;

        public ItemService(IUnitOfWork unit)
        {
            _itemsRepository = unit.ItemRepository;
        }

        public ProductItem Get(int id)
        {
            return _itemsRepository.Entities.SingleOrDefault(it => it.Id == id);
        }

        public IQueryable<ProductItem> GetAll()
        {
            return _itemsRepository.Entities;
        }
    }
}
