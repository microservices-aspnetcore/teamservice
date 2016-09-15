using System;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public class MemoryLocationClient : ILocationClient
    {
        public Dictionary<Guid, SortedList<long, LocationRecord>> MemberLocationHistory {get; set;}            
            
        public LocationRecord AddLocation(Guid memberId, LocationRecord locationRecord) 
        {
            if(!MemberLocationHistory.ContainsKey(memberId))
            {
                MemberLocationHistory.Add(memberId, new SortedList<long, LocationRecord>());
            }

            MemberLocationHistory[memberId].Add(locationRecord.Timestamp, locationRecord);

            return locationRecord;
        }

        public MemoryLocationClient() 
        {
            this.MemberLocationHistory = new Dictionary<Guid, SortedList<long, LocationRecord>>();
        }

        public LocationRecord GetLatestForMember(Guid memberId) 
        {
            if(MemberLocationHistory.ContainsKey(memberId)) 
            {
                return MemberLocationHistory[memberId].Values.LastOrDefault();
            } 

            return null;
        }
    }
}