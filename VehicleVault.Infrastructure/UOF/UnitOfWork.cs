using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;
using VehicleVault.Infrastructure.Repository;

namespace VehicleVault.Infrastructure.UOF;

public class UnitOfWork: IUnitOfWork
{
    #region Instance Fields
    private readonly ApplicationDbContext  _dbContext;
    private readonly Dictionary<Type, object> _repositories;
    #endregion
    
    public ICategoryRepository GetCategoryRepository { get; }

    
    #region Constructor
    public UnitOfWork(ApplicationDbContext dbContext, ICategoryRepository getCategoryRepository)
    {
        _dbContext = dbContext; 
        GetCategoryRepository = getCategoryRepository;
        _repositories = new Dictionary<Type, object>();
    }
    #endregion
    
    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T);
        
        if (!_repositories.ContainsKey(type))
        {
            var repoInstance = new GenericRepository<T>(_dbContext);
            _repositories[type] = repoInstance;
        }

        return (IGenericRepository<T>)_repositories[type];
    }
    
    public void SaveChanges() => _dbContext.SaveChanges();

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public void Dispose() => _dbContext.Dispose();

}