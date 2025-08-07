using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface ICategoryRepository: IGenericRepository<Category>
{
    #region GET 
    Task<Category> GetCategoryByName(string name);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateCategory(int id, Category category);
    #endregion
    
    #region DELETE 
    Task DeleteCategoryById(int id);
    
    Task DeleteCategoryByName(string name);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}