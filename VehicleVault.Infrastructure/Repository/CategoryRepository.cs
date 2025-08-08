using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    public async Task<Category> GetByIdIgnoreQueryFilterAsync(int id)
    { 
        return await _dbContext.Categories
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<Category> GetCategoryByName(string name)
    { 
        return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task UpdateCategory(int id, Category category) 
    {
        var categoryToUpdate = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (categoryToUpdate == null) return;

        categoryToUpdate.UpdateCategory(category.Name, category.Description);
    }


    public async Task DeleteCategoryById(int id)
    {
        var categoryToDelete = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        
        if (categoryToDelete == null) return;
        
        categoryToDelete.SoftDeleteCategory();
    }

    public async Task RestoreCategoryById(int id)
    {
        var categoryToRestore = await _dbContext.Categories.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (categoryToRestore == null) return;
        
        categoryToRestore.RestoreCategory();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.Categories.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}