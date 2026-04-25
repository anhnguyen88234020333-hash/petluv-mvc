using Microsoft.EntityFrameworkCore;
using PetLuv.Models; 

namespace PetLuv.Data
{
    public class PetLuvDbContext : DbContext
    {
        public PetLuvDbContext(DbContextOptions<PetLuvDbContext> options) : base(options)
        {
        }

        
        public DbSet<User> Users { get; set; }
    }
}