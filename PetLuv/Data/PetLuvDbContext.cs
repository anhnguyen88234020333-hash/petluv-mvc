using Microsoft.EntityFrameworkCore;

namespace PetLuv.Data
{
    public class PetLuvDbContext : DbContext
    {
        public PetLuvDbContext(DbContextOptions<PetLuvDbContext> options) : base(options) { }

        // để trống xong Model thêm vào sau
    }

}