using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class TransmissionTypeRepository: GenericRepository<TransmissionType>, ITransmissionTypeRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public TransmissionTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    public async Task<TransmissionType> GetTransmissionTypeByName(string name)
    {
        return await _dbContext.TransmissionTypes.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task<TransmissionType> GetByIdIgnoreQueryFilterAsync(int id)
    {
        return await _dbContext.TransmissionTypes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task UpdateTransmissionType(int id, TransmissionType transmissionType)
    {
        var transmissionTypeToUpdate = await _dbContext.TransmissionTypes.FirstOrDefaultAsync(c => c.Id == id);

        if (transmissionTypeToUpdate == null) return;

        transmissionTypeToUpdate.UpdateTransmissionType(transmissionType.Name);
    }

    public async Task DeleteTransmissionTypeById(int id)
    {
        var transmissionTypeToDelete = await _dbContext.TransmissionTypes.FirstOrDefaultAsync(c => c.Id == id);
        
        if (transmissionTypeToDelete == null) return;
        
        transmissionTypeToDelete.SoftDeleteTransmissionType();
    }

    public async Task RestoreTransmissionTypeById(int id)
    {
        var transmissionTypeToRestore = await _dbContext.TransmissionTypes.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (transmissionTypeToRestore == null) return;
        
        transmissionTypeToRestore.RestoreTransmissionType();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.TransmissionTypes.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}