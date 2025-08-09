using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.UOF;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : class;
    
    ICategoryRepository GetCategoryRepository { get; }
    
    IMakesRepository  GetMakesRepository { get; }
    
    IBodyRepository GetBodyRepository { get; }
    
    IFuelTypeRepository GetFuelTypeRepository { get; }
    
    IDriveTypeRepository GetDriveTypeRepository { get; }
    
    void SaveChanges();

    Task SaveChangesAsync();
}
