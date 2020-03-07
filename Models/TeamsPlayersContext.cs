using Microsoft.EntityFrameworkCore;

namespace TeamsApi.Models
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base (options)
        {

        }
        public DbSet<Team> Teams { 
            get; 
            set;
        }
    }

    public class PlayerContext: DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
    }
}