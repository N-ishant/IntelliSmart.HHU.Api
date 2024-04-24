using IntelliSmart.HHU.Api.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace IntelliSmart.HHU.Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Register> Users { get; set; }
    }
}