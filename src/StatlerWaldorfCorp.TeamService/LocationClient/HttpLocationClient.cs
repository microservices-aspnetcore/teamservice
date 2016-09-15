using System;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public class LocationClient : ILocationClient
    {
        public LocationRecord GetLatestForMember(Guid memberId) 
        {
            return new LocationRecord();
        }

        public LocationRecord AddLocation(Guid memberId, LocationRecord locationRecord) 
        {
            return locationRecord;
        }
    }
}