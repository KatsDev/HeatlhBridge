using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        Task<int> Save();
        Task<IList<T>> Search(Expression<Func<T, bool>> predicate);
        Task<T> SearchIndividual(Expression<Func<T, bool>> predicate);
    }
}
