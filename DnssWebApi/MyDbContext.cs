using Microsoft.EntityFrameworkCore;

namespace DnssWebApi
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}