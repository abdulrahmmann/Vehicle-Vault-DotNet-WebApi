using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface IFuelTypeRepository: IGenericRepository<FuelType>
{
    #region GET 
    Task<FuelType> GetFuelTypeByName(string name);
    
    Task<FuelType> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateFuelType(int id, FuelType fuelType);
    #endregion
    
    #region DELETE 
    Task DeleteFuelTypeById(int id);
    
    Task RestoreFuelTypeById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}