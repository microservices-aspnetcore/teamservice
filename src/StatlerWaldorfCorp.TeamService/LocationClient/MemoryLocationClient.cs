using System;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
    public class MemoryLocationClient : ILocationClient
    {
        public Dictionary<Guid, SortedList<long, LocationRecord>> MemberLocationHistory { get; set; }

        public async Task<LocationRecord> AddLocation(Guid memberId, LocationRecord locationRecord)
        {
            return await Task.Run(() =>
           {
               if (!MemberLocationHistory.ContainsKey(memberId))
               {
                   MemberLocationHistory.Add(memberId, new SortedList<long, LocationRecord>());
               }

               MemberLocationHistory[memberId].Add(locationRecord.Timestamp, locationRecord);

               return locationRecord;
           });
        }

        public MemoryLocationClient()
        {
            this.MemberLocationHistory = new Dictionary<Guid, SortedList<long, LocationRecord>>();
        }

        public async Task<LocationRecord> GetLatestForMember(Guid memberId)
        {
            return await Task.Run(() =>
            {
                if (MemberLocationHistory.ContainsKey(memberId))
                {
                    return MemberLocationHistory[memberId].Values.LastOrDefault();
                }

                return null;
            });
        }
    }
}