using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class ModelsRepository: GenericRepository<Model>, IModelsRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public ModelsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    public async Task<Model> GetModelByName(string name)
    {
        return await _dbContext.Models.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task<Model> GetByIdIgnoreQueryFilterAsync(int id)
    {
        return await _dbContext.Models
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task UpdateModel(int id, Model model)
    {
        var modelToUpdate = await _dbContext.Models.FirstOrDefaultAsync(c => c.Id == id);

        if (modelToUpdate == null) return;

        modelToUpdate.UpdateModel(model.Name);
    }

    public async Task DeleteModelById(int id)
    {
        var modelToDelete = await _dbContext.Models.FirstOrDefaultAsync(c => c.Id == id);
        
        if (modelToDelete == null) return;
        
        modelToDelete.SoftDeleteModel();
    }

    public async Task RestoreModelById(int id)
    {
        var modelToRestore = await _dbContext.Models.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (modelToRestore == null) return;
        
        modelToRestore.RestoreModel();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.Models.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}