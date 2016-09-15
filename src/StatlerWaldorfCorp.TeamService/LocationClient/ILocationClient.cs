using System;
using System.Threading.Tasks;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public interface ILocationClient
    {
        Task<LocationRecord> GetLatestForMember(Guid memberId);
        Task<LocationRecord> AddLocation(Guid memberId, LocationRecord locationRecord);
    }
}