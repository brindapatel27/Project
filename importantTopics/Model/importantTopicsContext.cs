using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace importantTopics.Model
{
    public partial class importantTopicsContext : DbContext
    {
        public importantTopicsContext()
        {
        }

        public importantTopicsContext(DbContextOptions<importantTopicsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contain> Contain { get; set; }
        public virtual DbSet<Keywords> Keywords { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:important-topics.database.windows.net,1433;Initial Catalog=importantTopics;Persist Security Info=False;User ID=brinda123;Password=Bansi@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Contain>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.KeywordId })
                    .HasName("PK__Contain__01521C2118B8EDFC");

                entity.HasOne(d => d.Keyword)
                    .WithMany(p => p.Contain)
                    .HasForeignKey(d => d.KeywordId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Contain__Keyword__4E88ABD4");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Contain)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Contain__TopicID__4D94879B");
            });

            modelBuilder.Entity<Keywords>(entity =>
            {
                entity.HasKey(e => e.KeywordId)
                    .HasName("PK__Keywords__37C135C18737F651");

                entity.Property(e => e.KeywordId).ValueGeneratedNever();

                entity.Property(e => e.KeywordName).IsUnicode(false);
            });

            modelBuilder.Entity<Topics>(entity =>
            {
                entity.HasKey(e => e.TopicId)
                    .HasName("PK__Topics__022E0F7DD8BFE677");

                entity.Property(e => e.TopicId).ValueGeneratedNever();

                entity.Property(e => e.TopicName).IsUnicode(false);
            });
        }
    }
}
