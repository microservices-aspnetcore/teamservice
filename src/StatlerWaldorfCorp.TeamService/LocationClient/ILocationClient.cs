using System;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public interface ILocationClient
    {
        LocationRecord GetLatestForMember(Guid memberId);
        LocationRecord AddLocation(Guid memberId, LocationRecord locationRecord);
    }
}