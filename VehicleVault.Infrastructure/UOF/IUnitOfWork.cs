using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.UOF;

public interface IUnitOfWork: IDisposable
{
    ApplicationDbContext DbContext { get; }
    
    ICategoryRepository GetCategoryRepository { get; }
    
    IGenericRepository<T> GetRepository<T>() where T : class;
    
    void SaveChanges();

    Task SaveChangesAsync();
}