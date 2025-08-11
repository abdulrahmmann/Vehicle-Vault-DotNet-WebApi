using VehicleVault.Domain.Entities;
using VehicleVault.Domain.Views;

namespace VehicleVault.Domain.IRepository;

public interface IVehicleRepository: IGenericRepository<Vehicle>
{
    IQueryable<Vehicle> AsNoTrackingVehicles();
    
    IQueryable<Vehicle> GetAllVehicles();
    
    // IQueryable<VehiclesSummaryView> GetVehiclesById(int id);
    // IQueryable<VehiclesSummaryView> GetVehiclesByName(string vehicleName);
    //
    // IQueryable<VehiclesSummaryView> GetVehiclesByModelId(int id);
    // IQueryable<VehiclesSummaryView> GetVehiclesByModelName(string modelName);
    //
    // IQueryable<VehiclesSummaryView> GetVehiclesBySubModelId(int id);
    // IQueryable<VehiclesSummaryView> GetVehiclesBySubModelName(string subModelName);
    
    
}