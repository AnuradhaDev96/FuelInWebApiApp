using Microsoft.EntityFrameworkCore;

namespace FuelInApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
