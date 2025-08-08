using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class MakesRepository: GenericRepository<Make>, IMakesRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public MakesRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    public async Task<Make> GetMakeByName(string name)
    {
        return await _dbContext.Makes.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task<Make> GetByIdIgnoreQueryFilterAsync(int id)
    {
        return await _dbContext.Makes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task UpdateMake(int id, Make make)
    {
        var makeToUpdate = await _dbContext.Makes.FirstOrDefaultAsync(m => m.Id == id);
        
        if (makeToUpdate == null) return;
        
        makeToUpdate.UpdateMake(make.Name);
    }

    public async Task DeleteMakeById(int id)
    {
        var makeToDelete = await _dbContext.Makes.FirstOrDefaultAsync(m => m.Id == id);
        
        if (makeToDelete == null) return;
        
        makeToDelete.SoftDeleteMake();
    }

    public async Task RestoreMakeById(int id)
    {
        var makeToRestore = await _dbContext.Makes.IgnoreQueryFilters()
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (makeToRestore == null) return;
        
        makeToRestore.RestoreMake();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.Makes.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}