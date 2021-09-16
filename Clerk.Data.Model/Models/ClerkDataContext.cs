using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Clerk.Data.Model.Models
{
    public partial class ClerkDataContext : DbContext
    {
        public ClerkDataContext()
        {
        }

        public ClerkDataContext(DbContextOptions<ClerkDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Committee> Committees { get; set; }
        public virtual DbSet<CommitteeAssignment> CommitteeAssignments { get; set; }
        public virtual DbSet<CommitteeRatio> CommitteeRatios { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<State> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string connString = Configuration.ConnectionString;
                //optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Committee>(entity =>
            {
                entity.ToTable("Committee");

                entity.Property(e => e.CommitteeId).HasColumnName("CommitteeID");

                entity.Property(e => e.BuildingCode).HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HeaderText).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ParentCommitteeId).HasColumnName("ParentCommitteeID");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Room).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.ZipCode).HasMaxLength(50);

                entity.Property(e => e.ZipSuffix).HasMaxLength(50);

                entity.HasOne(d => d.ParentCommittee)
                    .WithMany(p => p.InverseParentCommittee)
                    .HasForeignKey(d => d.ParentCommitteeId)
                    .HasConstraintName("FK_Committee_ParentCommittee");
            });

            modelBuilder.Entity<CommitteeAssignment>(entity =>
            {
                entity.HasKey(e => e.AssignmentId);

                entity.ToTable("CommitteeAssignment");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.CommitteeId).HasColumnName("CommitteeID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Committee)
                    .WithMany(p => p.CommitteeAssignments)
                    .HasForeignKey(d => d.CommitteeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommitteeAssignment_Committee");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CommitteeAssignments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommitteeAssignment_Member");
            });

            modelBuilder.Entity<CommitteeRatio>(entity =>
            {
                entity.ToTable("CommitteeRatio");

                entity.Property(e => e.CommitteeRatioId).HasColumnName("CommitteeRatioID");

                entity.Property(e => e.CommitteeId).HasColumnName("CommitteeID");

                entity.Property(e => e.Majority).HasMaxLength(50);

                entity.HasOne(d => d.Committee)
                    .WithMany(p => p.CommitteeRatios)
                    .HasForeignKey(d => d.CommitteeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommitteeRatio_Committee");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.BioguideId)
                    .HasMaxLength(50)
                    .HasColumnName("BioguideID");

                entity.Property(e => e.Caucus).HasMaxLength(10);

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.ElectedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.Party).HasMaxLength(10);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.Suffix).HasMaxLength(50);

                entity.Property(e => e.SwornDate).HasColumnType("datetime");

                entity.Property(e => e.TownName).HasMaxLength(50);

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_Member_Office");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Member_State");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("Office");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.Building).HasMaxLength(50);

                entity.Property(e => e.Room).HasMaxLength(50);

                entity.Property(e => e.ZipCode).HasMaxLength(50);

                entity.Property(e => e.ZipSuffix).HasMaxLength(50);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
