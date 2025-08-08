using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface IBodyRepository: IGenericRepository<Body>
{
    #region GET 
    Task<Body> GetBodyByName(string name);
    
    Task<Body> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateBody(int id, Body body);
    #endregion
    
    #region DELETE 
    Task DeleteBodyById(int id);
    
    Task RestoreBodyById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}