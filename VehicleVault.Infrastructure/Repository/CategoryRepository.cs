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


    public async Task<Category> GetCategoryByName(string name)
    { 
        return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task UpdateCategory(int id, Category category) 
    {
        var categoryToUpdate = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (categoryToUpdate == null) return;

        categoryToUpdate.UpdateCategory(category.Name, category.Description);
        await _dbContext.SaveChangesAsync(); 
    }


    public async Task DeleteCategoryById(int id)
    {
        var categoryToDelete = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        
        if (categoryToDelete == null) return;
        
        categoryToDelete.SoftDeleteCategory();
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategoryByName(string name)
    {
        var categoryToDelete = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);

        if (categoryToDelete == null) return;

        categoryToDelete.SoftDeleteCategory();
        await _dbContext.SaveChangesAsync();
    }
}