using VehicleVault.Domain.IRepository;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.UOF;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : class;
    
    ApplicationDbContext dbContext { get; }
    
    IVehicleRepository  GetVehicleRepository { get; }
    
    ICategoryRepository GetCategoryRepository { get; }
    
    IMakesRepository  GetMakesRepository { get; }
    
    IBodyRepository GetBodyRepository { get; }
    
    IFuelTypeRepository GetFuelTypeRepository { get; }
    
    IModelsRepository  GetModelsRepository { get; }
    
    IDriveTypeRepository GetDriveTypeRepository { get; }
    
    ISubModelRepository GetSubModelRepository { get; }
    
    ITransmissionTypeRepository  GetTransmissionTypeRepository { get; }
    
    void SaveChanges();
    
    Task SaveChangesAsync();
}