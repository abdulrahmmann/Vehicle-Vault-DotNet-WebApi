using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;

namespace VehicleVault.Application.Features.FeaturesVFeature.Commands.Requests;

public record CreateFeatureRequest(CreateFeatureDto FeatureDto): IRequest<BaseResponse<Unit>>;