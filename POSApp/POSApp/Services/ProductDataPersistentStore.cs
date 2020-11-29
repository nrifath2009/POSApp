using POSApp.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSApp.Services
{
    public class ProductDataPersistentStore : IDataStore<Product>
    {
        private readonly Realm _context;
        public ProductDataPersistentStore()
        {
            _context = Realm.GetInstance();
        }
        public async Task<bool> AddItemAsync(Product item)
        {
            await Task.Yield();
            _context.Write(() =>
            {
                _context.Add(item);
            });
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetItemAsync(string id)
        {
            await Task.Yield();
            var obj =  _context.All<Product>().FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(obj);
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            await Task.Yield();
            return await Task.FromResult(_context.All<Product>().ToList());
        }

        public async Task<bool> UpdateItemAsync(Product item)
        {
            await Task.Yield();
            _context.Write(() => _context.Add(item, update: true));
            return await Task.FromResult(true);
        }
    }
}
