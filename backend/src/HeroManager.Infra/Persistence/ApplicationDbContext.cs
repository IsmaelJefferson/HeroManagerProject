using HeroManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeroManager.Infra.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hero> Heros { get; set; }

    public DbSet<SuperPower> SuperPowers { get; set; }

    public DbSet<SuperPowerHero> SuperPowerHeroes { get; set; }

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SuperPower>().HasData(
            new SuperPower(1, "Super Força", "Força sobre-humana"),
            new SuperPower(2, "Voo", "Capacidade de voar"),
            new SuperPower(3, "Visão Laser", "Emissão de raios de laser pelos olhos")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SuperPowerHero>()
            .HasKey(x => new { x.HeroId, x.SuperPowerId });

        modelBuilder.Entity<SuperPowerHero>()
            .HasOne(x => x.Hero)
            .WithMany(h => h.SuperPowers)
            .HasForeignKey(x => x.HeroId);

        modelBuilder.Entity<SuperPowerHero>()
            .HasOne(x => x.SuperPower)
            .WithMany(sp => sp.heroSuperPowers)
            .HasForeignKey(x => x.SuperPowerId);

        Seed(modelBuilder);
    }
}