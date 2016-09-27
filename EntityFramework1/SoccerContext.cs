using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework1
{
    class SoccerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
    }
}
