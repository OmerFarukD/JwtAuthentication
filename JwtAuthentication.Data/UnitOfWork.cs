using JwtAuthentication.Core.UnitOfWork;

namespace JwtAuthentication.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public void Save()
    {
        _appDbContext.SaveChanges();
    }
}