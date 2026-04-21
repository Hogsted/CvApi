using CvApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CvApi.Data
{
    public class CvDbContext : DbContext
    {
        public CvDbContext(DbContextOptions<CvDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Experience> Experiences { get; set; }
    }
}