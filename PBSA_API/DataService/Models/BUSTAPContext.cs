using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataService.Models
{
    public partial class BUSTAPContext : DbContext
    {
        public BUSTAPContext()
        {
        }

        public BUSTAPContext(DbContextOptions<BUSTAPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplyPriceType> ApplyPriceType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Idpaper> Idpaper { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketType> TicketType { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Wallet> Wallet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UH7HU37\\TIENTPSQL;Database=BUSTAP;User ID=sa;Password=1234;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<ApplyPriceType>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.DebitCount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.WorkingDate).HasColumnType("date");
            });

            modelBuilder.Entity<Idpaper>(entity =>
            {
                entity.ToTable("IDPaper");

                entity.Property(e => e.AppliedPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IsUsing).HasColumnName("isUsing");

                entity.Property(e => e.IsValid).HasColumnName("isValid");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.SoldTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TicketTypeId)
                    .HasConstraintName("FK_Ticket_TicketType");

                entity.HasOne(d => d.Verhicle)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.VerhicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Vehicle");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PasswordHash).HasMaxLength(200);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Department).HasMaxLength(10);

                entity.Property(e => e.Idnumber)
                    .IsRequired()
                    .HasColumnName("IDNumber")
                    .HasMaxLength(20);

                entity.Property(e => e.IsTraveling).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastTravelTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });
        }
    }
}
