using Microsoft.EntityFrameworkCore;
using RecruiterApi.Models;

namespace RecruiterApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<Recruiter> Recruiters => Set<Recruiter>();
    }
}