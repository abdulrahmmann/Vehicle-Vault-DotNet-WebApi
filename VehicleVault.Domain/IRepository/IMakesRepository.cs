using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface IMakesRepository: IGenericRepository<Make>
{
    #region GET 
    Task<Make> GetMakeByName(string name);
    
    Task<Make> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    
    #endregion
    
    #region PUT 
    Task UpdateMake(int id, Make make);
    #endregion
    
    #region DELETE 
    Task DeleteMakeById(int id);
    
    Task RestoreMakeById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}