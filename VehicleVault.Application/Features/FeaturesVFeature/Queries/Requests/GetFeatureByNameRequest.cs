using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;

namespace VehicleVault.Application.Features.FeaturesVFeature.Queries.Requests;

public record GetFeatureByNameRequest(string Name): IRequest<BaseResponse<FeatureDto>>;