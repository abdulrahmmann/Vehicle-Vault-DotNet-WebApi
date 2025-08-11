using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;

namespace VehicleVault.Application.Features.FeaturesVFeature.Queries.Requests;

public record GetAllFeaturesRequest(): IRequest<BaseResponse<IEnumerable<FeatureDto>>>;