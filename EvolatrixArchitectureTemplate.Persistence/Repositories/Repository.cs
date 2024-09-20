using EvolatrixArchitectureTemplate.Persistence.Context;
using EvolatrixArchitectureTemplate.PersistenceContract;
using Microsoft.EntityFrameworkCore;

namespace EvolatrixArchitectureTemplate.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;
        public Repository(DatabaseContext driveNowContext)
        {
            _databaseContext = driveNowContext;
            _dbSet = _databaseContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
