using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InterviewProfiler.Models
{
    public partial class profilerContext : DbContext
    {
        public profilerContext()
        {
        }

        public profilerContext(DbContextOptions<profilerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "profuser");

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question", "dcostea");

                entity.Property(e => e.Answer100)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Answer50)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Meta).IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Source).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Skill");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill", "dcostea");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tech)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
