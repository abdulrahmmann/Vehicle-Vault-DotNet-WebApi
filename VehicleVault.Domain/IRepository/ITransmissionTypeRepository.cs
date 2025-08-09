using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface ITransmissionTypeRepository: IGenericRepository<TransmissionType>
{
    #region GET 
    Task<TransmissionType> GetTransmissionTypeByName(string name);
    
    Task<TransmissionType> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateTransmissionType(int id, TransmissionType transmissionType);
    #endregion
    
    #region DELETE 
    Task DeleteTransmissionTypeById(int id);
    
    Task RestoreTransmissionTypeById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}