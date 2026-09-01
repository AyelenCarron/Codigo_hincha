using CodigoHincha.DAL.interfaces;

namespace CodigoHincha.DAL.EntityFramework;

public class EFUnitOfWork(CodigoHinchaDbContext context) : IUnitOfWork
{
    private readonly CodigoHinchaDbContext _context = context;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
