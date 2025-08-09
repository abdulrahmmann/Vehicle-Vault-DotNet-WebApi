using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Infrastructure.Repository;

public class DriveTypeRepository: GenericRepository<DriveType>, IDriveTypeRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public DriveTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion
    
    public async Task<DriveType> GetDriveTypeByName(string name)
    {
        return await _dbContext.DriveTypes.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task<DriveType> GetByIdIgnoreQueryFilterAsync(int id)
    {
        return await _dbContext.DriveTypes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task UpdateDriveType(int id, DriveType driveType)
    {
        var driveTypeToUpdate = await _dbContext.DriveTypes.FirstOrDefaultAsync(c => c.Id == id);

        if (driveTypeToUpdate == null) return;

        driveTypeToUpdate.UpdateDriveType(driveType.Name);
    }

    public async Task DeleteDriveTypeById(int id)
    {
        var driveTypeToDelete = await _dbContext.DriveTypes.FirstOrDefaultAsync(c => c.Id == id);
        
        if (driveTypeToDelete == null) return;
        
        driveTypeToDelete.SoftDeleteDriveType();
    }

    public async Task RestoreDriveTypeById(int id)
    {
        var driveTypeToRestore = await _dbContext.DriveTypes.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (driveTypeToRestore == null) return;
        
        driveTypeToRestore.RestoreDriveType();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.DriveTypes.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}