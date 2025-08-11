using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IRepository;
using VehicleVault.Domain.Views;
using VehicleVault.Infrastructure.Context;

namespace VehicleVault.Infrastructure.Repository;

public class VehicleRepository: GenericRepository<Vehicle>, IVehicleRepository
{
    #region Instance Fields
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    #endregion
    
    #region Constructor
    public VehicleRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    #endregion
    
    public IQueryable<Vehicle> AsNoTrackingVehicles() => _dbContext.Vehicles.AsNoTracking();
    
    public IQueryable<Vehicle>GetAllVehicles()
    {
        var vehicles = AsNoTrackingVehicles()
            .Include(v => v.User)
            .Include(v => v.Make)
            .Include(v => v.Body)
            .Include(v => v.Model)
            .Include(v => v.SubModel)
            .Include(v => v.FuelType)
            .Include(v => v.TransmissionType)
            .Include(v => v.Category)
            .Include(v => v.Color);

        return vehicles;
        // var vehicles = await AsNoTrackingVehicles()
        //     .Select(v => new VehiclesSummaryView
        //     {
        //         Id = v.Id,
        //         Name = v.Name,
        //         Year = v.Year,
        //         Engine = v.Engine,
        //         EngineCc = v.EngineCc,
        //         EngineCylinders = v.EngineCylinders,
        //         EngineLiterDisplay = v.EngineLiterDisplay,
        //         NumDoors = v.NumDoors,
        //         
        //         BodyId = v.BodyId,
        //         BodyName = v.Body.Name,
        //         
        //         DriveTypeId = v.DriveTypeId,
        //         DriveTypeName = v.DriveType.Name,
        //
        //         FuelTypeId = v.FuelTypeId,
        //         FuelTypeName = v.FuelType.Name,
        //
        //         MakeId = v.MakeId,
        //         MakeName = v.Make.Name,
        //
        //         ModelId = v.ModelId,
        //         ModelName = v.Model.Name,
        //
        //         SubModelId = v.SubModelId,
        //         SubModelName = v.SubModel.Name,
        //
        //         TransmissionTypeId = v.TransmissionTypeId,
        //         TransmissionTypeName = v.TransmissionType.Name,
        //
        //         ColorId = v.ColorId,
        //         ColorCode = v.Color.Name,
        //
        //         CategoryId = v.CategoryId,
        //         CategoryName = v.Category.Name,
        //
        //         UserId = v.UserId,
        //         UserName = v.User.UserName!
        //     }).ToListAsync();
        //
        // return vehicles;
    }

    // public IQueryable<VehiclesSummaryView> GetVehiclesById(int id)
    // {
    //     return _dbContext.Vehicles.AsNoTracking().Where(v => v.Id == id)
    //         .Select(v => new VehiclesSummaryView
    //         {
    //             Id = v.Id,
    //             Name = v.Name,
    //             Year = v.Year,
    //             Engine = v.Engine,
    //             EngineCc = v.EngineCc,
    //             EngineCylinders = v.EngineCylinders,
    //             EngineLiterDisplay = v.EngineLiterDisplay,
    //             NumDoors = v.NumDoors,
    //             
    //             BodyId = v.BodyId,
    //             BodyName = v.Body.Name,
    //             
    //             DriveTypeId = v.DriveTypeId,
    //             DriveTypeName = v.DriveType.Name,
    //
    //             FuelTypeId = v.FuelTypeId,
    //             FuelTypeName = v.FuelType.Name,
    //
    //             MakeId = v.MakeId,
    //             MakeName = v.Make.Name,
    //
    //             ModelId = v.ModelId,
    //             ModelName = v.Model.Name,
    //
    //             SubModelId = v.SubModelId,
    //             SubModelName = v.SubModel.Name,
    //
    //             TransmissionTypeId = v.TransmissionTypeId,
    //             TransmissionTypeName = v.TransmissionType.Name,
    //
    //             ColorId = v.ColorId,
    //             ColorCode = v.Color.Name,
    //
    //             CategoryId = v.CategoryId,
    //             CategoryName = v.Category.Name,
    //
    //             UserId = v.UserId,
    //             UserName = v.User.UserName!
    //         });
    // }
}