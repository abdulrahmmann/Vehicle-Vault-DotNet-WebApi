namespace VehicleVault.Domain.IRepository;

public interface IGenericRepository<T> where T : class
{
    #region GET
    IEnumerable<T> GetAll();
    
    Task<IEnumerable<T>> GetAllAsync();
    
    T GetById(int id);
    
    Task<T> GetByIdAsync(int id);
    #endregion
    
    #region POST 
    void Add(T entity);
    
    Task AddAsync(T entity);
    
    Task AddRangeAsync(IEnumerable<T> entities);
    #endregion 
    
    
    void SaveChanges();
    
    Task SaveChangesAsync();
}