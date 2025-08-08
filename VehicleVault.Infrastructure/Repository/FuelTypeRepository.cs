using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class FuelTypeRepository: GenericRepository<FuelType>, IFuelTypeRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public FuelTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    public async Task<FuelType> GetFuelTypeByName(string name)
    {
        return await _dbContext.FuelTypes.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task<FuelType> GetByIdIgnoreQueryFilterAsync(int id)
    {
        return await _dbContext.FuelTypes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task UpdateFuelType(int id, FuelType fuelType)
    {
        var fuelTypeToUpdate = await _dbContext.FuelTypes.FirstOrDefaultAsync(c => c.Id == id);

        fuelTypeToUpdate?.UpdateFuelType(fuelType.Name);
    }

    public async Task DeleteFuelTypeById(int id)
    {
        var fuelTypeToDelete = await _dbContext.FuelTypes.FirstOrDefaultAsync(c => c.Id == id);

        fuelTypeToDelete?.SoftDeleteFuelType();
    }

    public async Task RestoreFuelTypeById(int id)
    {
        var fuelTypeToRestore = await _dbContext.FuelTypes.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);

        fuelTypeToRestore?.RestoreFuelType();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.FuelTypes.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}