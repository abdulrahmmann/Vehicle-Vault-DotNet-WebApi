using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IRepository;

public interface ICategoryRepository: IGenericRepository<Category>
{
    #region GET 
    Task<Category> GetCategoryByName(string name);
    
    Task<Category> GetByIdIgnoreQueryFilterAsync(int id);
    #endregion
    
    #region POST 
    #endregion
    
    #region PUT 
    Task UpdateCategory(int id, Category category);
    #endregion
    
    #region DELETE 
    Task DeleteCategoryById(int id);
    
    Task RestoreCategoryById(int id);
    #endregion
    
    Task<bool> ExistsByNameAsync(string name);
}