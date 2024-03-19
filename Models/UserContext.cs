using Microsoft.EntityFrameworkCore;

namespace user_service_app.Models
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions options):
            base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
    }
}
