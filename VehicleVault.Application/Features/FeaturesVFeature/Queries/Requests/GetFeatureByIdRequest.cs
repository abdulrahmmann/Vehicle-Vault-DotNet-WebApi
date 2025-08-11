using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;

namespace VehicleVault.Application.Features.FeaturesVFeature.Queries.Requests;

public record GetFeatureByIdRequest(int Id): IRequest<BaseResponse<FeatureDto>>;