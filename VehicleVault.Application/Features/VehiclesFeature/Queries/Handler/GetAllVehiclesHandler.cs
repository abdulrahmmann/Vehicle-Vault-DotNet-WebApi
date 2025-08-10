using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.VehiclesFeature.DTOs;
using VehicleVault.Application.Features.VehiclesFeature.Queries.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.VehiclesFeature.Queries.Handler;

public class GetAllVehiclesHandler: IRequestHandler<GetAllVehiclesRequest, BaseResponse<IEnumerable<VehicleSummaryDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    #endregion
    
    #region Constructor
    public GetAllVehiclesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<VehicleSummaryDto>>> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var vehicles = _unitOfWork.GetVehicleRepository.GetAllVehicles().ToList();

            var vehiclesMapped = vehicles.Select(v => new VehicleSummaryDto(
                v.Id, v.Name, v.Year, v.Engine, v.EngineCc, v.EngineCylinders, v.EngineLiterDisplay, v.NumDoors,
                v.BodyId, v.Body.Name, // Body
                v.DriveTypeId, v.DriveType.Name, // DriveType
                v.FuelTypeId, v.FuelType.Name, // FuelType
                v.MakeId, v.Make.Name, // Make
                v.ModelId, v.Model.Name, // Model
                v.SubModelId, v.SubModel.Name, // SubModel
                v.TransmissionTypeId, v.TransmissionType.Name, // TransmissionType
                v.ColorId, v.Color.Name, // Color
                v.CategoryId, v.Category.Name, // Category
                v.UserId, v.User.UserName // User
            ));
            
            return BaseResponse<IEnumerable<VehicleSummaryDto>>.Success(vehiclesMapped, "Vehicles Retrieved success", vehicles.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}