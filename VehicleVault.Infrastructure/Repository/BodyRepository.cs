using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class BodyRepository: GenericRepository<Body>, IBodyRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    #endregion
    
    #region Constructor
    public BodyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    public async Task<Body> GetBodyByName(string name)
    {
        return await _dbContext.Bodies.FirstOrDefaultAsync(c => c.Name == name) ?? throw new InvalidOperationException();
    }

    public async Task<Body> GetByIdIgnoreQueryFilterAsync(int id)
    {
        return await _dbContext.Bodies
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task UpdateBody(int id, Body body)
    {
        var bodyToUpdate = await _dbContext.Bodies.FirstOrDefaultAsync(c => c.Id == id);

        if (bodyToUpdate == null) return;

        bodyToUpdate.UpdateBody(body.Name);
    }

    public async Task DeleteBodyById(int id)
    {
        var bodyToDelete = await _dbContext.Bodies.FirstOrDefaultAsync(c => c.Id == id);
        
        if (bodyToDelete == null) return;
        
        bodyToDelete.SoftDeleteBody();
    }

    public async Task RestoreBodyById(int id)
    {
        var bodyToRestore = await _dbContext.Bodies.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (bodyToRestore == null) return;
        
        bodyToRestore.RestoreBody();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.Bodies.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }
}