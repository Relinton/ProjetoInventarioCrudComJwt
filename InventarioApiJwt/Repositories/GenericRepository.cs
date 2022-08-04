
using InventarioApiJwt.Conexao;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioApiJwt.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly InventarioContext _inventarioContext;

        public GenericRepository(InventarioContext inventarioContext)
        {
            _inventarioContext = inventarioContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _inventarioContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _inventarioContext.Set<T>().FindAsync(id);
        }
        public async Task Insert(T obj)
        {
            await _inventarioContext.Set<T>().AddAsync(obj);
            await _inventarioContext.SaveChangesAsync();
        }

        public async Task Update(int id, T obj)
        {
            _inventarioContext.Set<T>().Update(obj);
            await _inventarioContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _inventarioContext.Set<T>().Remove(entity);
            await _inventarioContext.SaveChangesAsync();
        }
    }
}
