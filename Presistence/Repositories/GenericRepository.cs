using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class GenericRepository<T, TK> : IGenericRepository<T, TK> where T : BaseEntity<TK>
    {
        readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(TK id)
        {
            var element = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
            if (element != null)
            {
                _context.Set<T>().Remove(element);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TK id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
            if (entity == null)
            {
                return null!;
            }
            return entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
