using VehicleVault.Domain.IRepository;

namespace VehicleVault.Infrastructure.UOF;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : class;
    
    ICategoryRepository GetCategoryRepository { get; }
    
    IMakesRepository  GetMakesRepository { get; }
    
    IBodyRepository GetBodyRepository { get; }
    
    IFuelTypeRepository GetFuelTypeRepository { get; }
    
    IModelsRepository  GetModelsRepository { get; }
    
    IDriveTypeRepository GetDriveTypeRepository { get; }
    
    ISubModelRepository GetSubModelRepository { get; }
    
    void SaveChanges();
    
    Task SaveChangesAsync();
}