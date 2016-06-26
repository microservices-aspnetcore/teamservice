using System;

namespace StatlerWaldorfCorp.TeamService.Models
{
    public class Member {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Member(Guid id) {
            this.ID = id;
        }

        public Member(string firstName, string lastName, Guid id) : this(id) {
            this.FirstName = firstName;
            this.LastName = lastName;
        }        
    }
}