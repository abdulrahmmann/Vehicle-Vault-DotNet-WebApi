using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class SubModelRepository: GenericRepository<SubModel>, ISubModelRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public SubModelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion


    public async Task<SubModel> GetSubModelByName(string name)
    {
        return await _dbContext.SubModels.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task<SubModel> GetByIdIgnoreQueryFilterAsync(int id)
    {
        return await _dbContext.SubModels
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task UpdateSubModel(int id, int modelId, string subModelName)
    {
        var subModelToUpdate = await _dbContext.SubModels.FirstOrDefaultAsync(c => c.Id == id);

        if (subModelToUpdate == null) return;

        subModelToUpdate.UpdateSubModel(modelId, subModelName);
    }

    public async Task DeleteSubModelById(int id)
    {
        var subModelToDelete = await _dbContext.SubModels.FirstOrDefaultAsync(c => c.Id == id);
        
        if (subModelToDelete == null) return;
        
        subModelToDelete.SoftDeleteSubModel();
    }
    public async Task RestoreSubModelById(int id)
    {
        var subModelToRestore = await _dbContext.SubModels.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (subModelToRestore == null) return;
        
        subModelToRestore.RestoreSubModel();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.SubModels.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}