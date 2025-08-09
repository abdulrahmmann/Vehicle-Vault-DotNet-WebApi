using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface ISubModelRepository: IGenericRepository<SubModel>
{
    #region GET 
    Task<SubModel> GetSubModelByName(string name);
    
    Task<SubModel> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateSubModel(int id, int modelId, string subModelName);
    #endregion
    
    #region DELETE 
    Task DeleteSubModelById(int id);
    
    Task RestoreSubModelById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}