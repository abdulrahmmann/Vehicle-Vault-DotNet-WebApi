using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class GenericRepository<T>: IGenericRepository<T> where T : class
{
    #region Instance Fields

    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    #endregion
    
    #region Constructor

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    #endregion
    
    #region GET
    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public T GetById(int id)
    {
        return _dbSet.Find(id)!;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return (await _dbSet.FindAsync(id))!;
    }
    #endregion
    

    #region POST
    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }
    #endregion
    
    
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}