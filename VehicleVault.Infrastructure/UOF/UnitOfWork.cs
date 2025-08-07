using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;
using VehicleVault.Infrastructure.Repository;

namespace VehicleVault.Infrastructure.UOF;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext  _dbContext;
    
    private readonly Dictionary<Type, object> _repositories;
    
    public ApplicationDbContext DbContext { get; }
    
    public ICategoryRepository GetCategoryRepository { get; }

    public UnitOfWork(ApplicationDbContext dbContext, Dictionary<Type, object> repositories, ICategoryRepository getCategoryRepository)
    {
        _dbContext = dbContext;
        _repositories = repositories;
        GetCategoryRepository = getCategoryRepository;
    }

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