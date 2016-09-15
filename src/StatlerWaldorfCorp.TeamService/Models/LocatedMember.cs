using System;

namespace StatlerWaldorfCorp.TeamService.Models
{    
    public class LocatedMember : Member 
    {
        public LocationRecord LastLocation {get; set;}
    }        
}
