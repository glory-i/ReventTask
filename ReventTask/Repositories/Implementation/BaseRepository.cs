using Microsoft.EntityFrameworkCore;
using ReventTask.Data;
using ReventTask.Repositories.Interfaces;

namespace ReventTask.Repositories.Implementation
{
    public class BaseRepository<T> :IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return await Task.FromResult(entity);
        }
    }
}
