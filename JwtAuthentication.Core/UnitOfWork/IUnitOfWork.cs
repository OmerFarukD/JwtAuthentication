namespace JwtAuthentication.Core.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    void Save();
}