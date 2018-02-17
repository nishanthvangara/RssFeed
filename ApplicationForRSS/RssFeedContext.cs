using ApplicationForRSS.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationForRSS
{
    public class RssFeedContext : DbContext
    {
        public RssFeedContext(DbContextOptions<RssFeedContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<RssFeed> RssFeed { get; set; }
    }
}
