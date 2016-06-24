using System;

namespace StatlerWaldorfCorp.TeamService.Models
{
    public class Team {

        public string Name { get; set; }
        public Guid ID { get; set; }

        public Team()
        {
        }

        public Team(string name)
        {
            this.Name = name;
        }

        public Team(string name, Guid id) 
        {
            this.Name = name;
            this.ID = ID;
        }

        public override string ToString() {
            return this.Name;
        }
    }
}