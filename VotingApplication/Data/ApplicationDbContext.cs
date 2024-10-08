﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<VoterAddressDataModel> Address { get; set; }
        public DbSet<VoterDemographicsDataModel> Demographics { get; set; }
        
        public DbSet<ZipDataModel> Zip { get; set; }
        public DbSet<DistrictDataModel> District { get; set; }
        public DbSet<RegionDataModel> Region { get; set; }
        
        public DbSet<CandidateDataModel> Candidate { get; set; }
        public DbSet<OfficeDataModel> Office { get; set; }
        public DbSet<BallotDataModel> Ballot { get; set; }

        public DbSet<VoterVotesBallot> Votes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            // Sets up the many to many relationship of Zipcodes and districts
            modelBuilder.Entity<ZipFillsDistrict>()
                .HasKey(zd => new { zd.ZipCode, zd.DistrictName });

            modelBuilder.Entity<ZipFillsDistrict>()
                .HasOne(zd => zd.Zip)
                .WithMany(z => z.District)
                .HasForeignKey(zd => zd.ZipCode);

            modelBuilder.Entity<ZipFillsDistrict>()
                .HasOne(zd => zd.District)
                .WithMany(d => d.Zip)
                .HasForeignKey(zd => zd.DistrictName);
            
            // Sets up the many to many relationship of districts and regions
            modelBuilder.Entity<DistrictFillsRegion>()
                .HasKey(dr => new { dr.DistrictName, dr.RegionName });

            modelBuilder.Entity<DistrictFillsRegion>()
                .HasOne(zd => zd.District)
                .WithMany(z => z.Region)
                .HasForeignKey(zd => zd.DistrictName);

            modelBuilder.Entity<DistrictFillsRegion>()
                .HasOne(zd => zd.Region)
                .WithMany(d => d.District)
                .HasForeignKey(zd => zd.RegionName);

            //sets up the many to many relationship of ballots and Voters
            modelBuilder.Entity<VoterVotesBallot>()
                .HasKey(vb => new { vb.VoterName, vb.BallotName });

            modelBuilder.Entity<VoterVotesBallot>()
                .HasOne(vb => vb.Voter)
                .WithMany(u => u.VoteGiven)
                .HasForeignKey(vb => vb.VoterName);

            modelBuilder.Entity<VoterVotesBallot>()
                .HasOne(vb => vb.Ballot)
                .WithMany(b => b.Voter)
                .HasForeignKey(vb => vb.BallotName);

            modelBuilder.Entity<VoterVotesBallot>()
                .HasOne(vb => vb.Candidate)
                .WithMany(c => c.VoteReceived)
                .HasForeignKey(vb => vb.CandidateName);
        }
    }
}
