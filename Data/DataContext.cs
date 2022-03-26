using Microsoft.EntityFrameworkCore;
using ytRESTfulAPI.Models;

namespace ytRESTfulAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    
        public DbSet<ST_USER> ST_USER { get; set; }
    
    
    
    }
}
