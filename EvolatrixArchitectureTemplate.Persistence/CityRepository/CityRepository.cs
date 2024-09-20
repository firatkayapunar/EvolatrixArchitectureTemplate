using EvolatrixArchitectureTemplate.Domain.Entities;
using EvolatrixArchitectureTemplate.Persistence.Context;
using EvolatrixArchitectureTemplate.PersistenceContract.CityContracts;
using Microsoft.EntityFrameworkCore;

namespace EvolatrixArchitectureTemplate.Persistence.Repositories.CityRepository
{
    public class CityRepository<T> : ICityRepository<T>
    where T : BaseEntity
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public CityRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<T>();
        }
    }
}
