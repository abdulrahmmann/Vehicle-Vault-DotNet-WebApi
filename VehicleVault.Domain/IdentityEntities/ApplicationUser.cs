using Microsoft.AspNetCore.Identity;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Domain.IdentityEntities;

public class ApplicationUser: IdentityUser<int>
{
    public ICollection<Vehicle> VehiclesCollection { get; init; } = new List<Vehicle>();
}