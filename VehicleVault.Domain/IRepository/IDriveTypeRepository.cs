using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Domain.IRepository;

public interface IDriveTypeRepository: IGenericRepository<DriveType>
{
    #region GET 
    Task<DriveType> GetDriveTypeByName(string name);
    
    Task<DriveType> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateDriveType(int id, DriveType driveType);
    #endregion
    
    #region DELETE 
    Task DeleteDriveTypeById(int id);
    
    Task RestoreDriveTypeById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}