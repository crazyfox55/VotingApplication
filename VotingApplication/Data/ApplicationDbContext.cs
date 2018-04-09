using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VotingApplication
{
    /// <summary>
    /// The database representational motel for the whole application
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SettingsDataModel> Settings { get; set; }

        public DbSet<VoterRegistrationDataModel> Registration { get; set; }
        public DbSet<VoterDemographicsDataModel> Demographics { get; set; }

        public DbSet<ZipCodeDataModel> ZipCode { get; set; }
        public DbSet<DistrictDataModel> District { get; set; }
        public DbSet<RegionDataModel> Region { get; set; }
        
        public DbSet<CandidateDataModel> Candidate { get; set; }
        public DbSet<OfficeDataModel> Office { get; set; }
        public DbSet<BallotDataModel> Ballot { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            // Sets up the many to many relationship of Zipcodes and districts
            modelBuilder.Entity<ZipCodeFillsDistrict>()
                .HasKey(zd => new { zd.ZipCode, zd.DistrictId });

            modelBuilder.Entity<ZipCodeFillsDistrict>()
                .HasOne(zd => zd.Zip)
                .WithMany(z => z.District)
                .HasForeignKey(zd => zd.ZipCode);

            modelBuilder.Entity<ZipCodeFillsDistrict>()
                .HasOne(zd => zd.District)
                .WithMany(d => d.ZipCode)
                .HasForeignKey(zd => zd.DistrictId);

            // Sets up the many to many relationship of districts and regions
            modelBuilder.Entity<DistrictFillsRegion>()
                .HasKey(dr => new { dr.DistrictId, dr.RegionId });

            modelBuilder.Entity<DistrictFillsRegion>()
                .HasOne(zd => zd.District)
                .WithMany(z => z.Region)
                .HasForeignKey(zd => zd.DistrictId);

            modelBuilder.Entity<DistrictFillsRegion>()
                .HasOne(zd => zd.Region)
                .WithMany(d => d.District)
                .HasForeignKey(zd => zd.RegionId);
        }
    }
}
