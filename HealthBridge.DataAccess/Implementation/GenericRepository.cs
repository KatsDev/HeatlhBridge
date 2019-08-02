using HealthBridge.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public HealthBridgeEntities _context = null;
        public DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new HealthBridgeEntities();
            table = _context.Set<T>();
        }
        public GenericRepository(HealthBridgeEntities _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public async Task<IList<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> Search(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }

        public async Task<T> SearchIndividual(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
