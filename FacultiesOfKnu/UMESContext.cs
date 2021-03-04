using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lab1
{
    public partial class UMESContext : DbContext
    {
        public UMESContext()
        {
        }

        public UMESContext(DbContextOptions<UMESContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FacultyOfKnu> FacultyOfKnu { get; set; }
        public virtual DbSet<SpecializationOfFaculties> Knu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UMES;Trusted_Connection=True;");
            }
        }

        
    }
}
