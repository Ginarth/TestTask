using Microsoft.EntityFrameworkCore;
using SupportOperatorsSalaryAPI.Data.Database.Entities;

namespace SupportOperatorsSalaryAPI.Data.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<SupportOperator> SupportOperators { get; set; } = null!;
        public DbSet<Parameter> Parameters { get; set; } = null!;
        public DbSet<BaseRate> BaseRates { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SupportOperator>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasMaxLength(50);

                entity.Property(e => e.IsWorking)
                    .HasColumnName("is_working")
                    .HasColumnType("boolean")
                    .IsRequired();

                entity.Property(e => e.FirstResponseTime)
                    .HasColumnName("first_response_time")
                    .HasColumnType("smallint")
                    .IsRequired();

                entity.Property(e => e.ResponseTime)
                    .HasColumnName("response_time")
                    .HasColumnType("smallint")
                    .IsRequired();

                entity.Property(e => e.CompetencyAssessment)
                    .HasColumnName("competency_assessment")
                    .HasColumnType("smallint")
                    .IsRequired();

                entity.Property(e => e.PolitenessAssessment)
                    .HasColumnName("politeness_assessment")
                    .HasColumnType("smallint")
                    .IsRequired();

                entity.ToTable("support_operators", e =>
                {
                    e.HasCheckConstraint("valid_first_response_time",
                        "first_response_time > -1");

                    e.HasCheckConstraint("valid_response_time",
                        "response_time > -1");

                    e.HasCheckConstraint("valid_competency_assessment",
                        "competency_assessment > -1");

                    e.HasCheckConstraint("valid_politeness_assessment",
                        "politeness_assessment > -1");
                });
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("name");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasColumnType("decimal")
                    .HasPrecision(3, 2)
                    .IsRequired();

                entity.Property(e => e.BaseValue)
                    .HasColumnName("base_value")
                    .HasColumnType("smallint")
                    .IsRequired();

                entity.Property(e => e.NormalValue)
                    .HasColumnName("normal_value")
                    .HasColumnType("smallint")
                    .IsRequired();

                entity.ToTable("parameters", e =>
                {
                    e.HasCheckConstraint("valid_weight",
                        "weight >= 0.00 AND weight <= 1.00");

                    e.HasCheckConstraint("valid_base_value",
                        "base_value > -1");

                    e.HasCheckConstraint("valid_normal_value",
                        "normal_value > -1");

                    e.HasCheckConstraint("valid_values",
                        "base_value != normal_value");
                });
            });

            modelBuilder.Entity<BaseRate>(entity =>
            {
                entity.HasKey(e => e.Position)
                    .HasName("position");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("varchar(50)")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("integer")
                    .IsRequired();

                entity.ToTable("base_rates", e =>
                {
                    e.HasCheckConstraint("valid_amount",
                        "amount > -1");
                });
            });

            foreach (var name in Parameter.Names)
            {
                short baseValue = 0;
                short normalValue = 0;

                if (name.Split('_').Contains("assessment"))
                {
                    baseValue = 4;
                    normalValue = 5;
                }
                else if (name.Split('_').Contains("time"))
                {
                    baseValue = 120;
                    normalValue = 60;
                }

                modelBuilder.Entity<Parameter>().HasData(new Parameter 
                { 
                    Name = name, 
                    Weight = 0.25M, 
                    BaseValue = baseValue, 
                    NormalValue = normalValue 
                });
            }

            modelBuilder.Entity<BaseRate>().HasData(new BaseRate
            {
                Position = "support_operator",
                Amount = 40000
            });
        }
    }
}
