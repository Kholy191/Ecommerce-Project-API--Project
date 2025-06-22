using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<T,TK> where T : BaseEntity<TK>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TK id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(TK id);
    }
}
