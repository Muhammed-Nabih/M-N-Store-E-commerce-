using N_Store.Domain.Interfaces;
using N_Store.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace N_Store.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;


        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(T id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(T id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach(var item in includes)
            {
                query = query.Include(item);
            }
            return await ((DbSet<T>)query).FindAsync(id);
        }

        public async Task UpdateAsync(T id, T entity)
        {
            var currentEntity = await _context.Set<T>().FindAsync(id);
            if (currentEntity != null)
            {
                _context.Update(currentEntity);
                await _context.SaveChangesAsync();
            }

        }
    }
}
