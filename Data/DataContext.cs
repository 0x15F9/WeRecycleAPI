using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { set; get; }
        public DbSet<Admin> Admins { set; get; }
        public DbSet<Driver> Drivers { set; get; }
        public DbSet<Bin> Bins { set; get; }

    }
}