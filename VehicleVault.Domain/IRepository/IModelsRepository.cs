using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface IModelsRepository: IGenericRepository<Model>
{
    #region GET 
    Task<Model> GetModelByName(string name);
    
    Task<Model> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateModel(int id, Model model);
    #endregion
    
    #region DELETE 
    Task DeleteModelById(int id);
    
    Task RestoreModelById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}