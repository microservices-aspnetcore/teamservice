using System;

namespace StatlerWaldorfCorp.TeamService.Models
{
    public class Member {
        public Guid ID { get; set; }

        public Member(Guid id) {
            this.ID = id;
        }
    }
}