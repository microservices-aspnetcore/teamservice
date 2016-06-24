namespace StatlerWaldorfCorp.TeamService.Models
{
    public class Team {
        public string Name { get; set; }

        public Team()
        {
        }

        public Team(string name)
        {
            this.Name = name;
        }

        public override string ToString() {
            return this.Name;
        }
    }
}