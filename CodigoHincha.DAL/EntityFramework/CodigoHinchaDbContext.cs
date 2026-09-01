using Microsoft.EntityFrameworkCore;

namespace CodigoHincha.DAL.EntityFramework;

public class CodigoHinchaDbContext(DbContextOptions<CodigoHinchaDbContext> options)
    : DbContext(options)
{
    // Agregar DbSet<T> aquí a medida que se creen las entidades.
    // Ejemplo:
    // public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de entidades y relaciones aquí.
    }
}
