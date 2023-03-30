using System.Linq.Expressions;
using JwtAuthentication.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication.Data.Repositories.GenericRepositories;

public class EfEntityRepository<TEntity,TContext> : IGenericRepository<TEntity>
where TEntity : class
where TContext : DbContext
{
    public TContext Context { get; }
    
    public EfEntityRepository(TContext context)
    {
        Context = context;
    }
    
    
    public async Task<TEntity> GetByIdAsync(int id)
    {
      var entity= await Context.Set<TEntity>().FindAsync(id);

      if (entity is not null)
          Context.Entry(entity).State = EntityState.Detached;

      return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public  IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return  Context.Set<TEntity>().Where(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
      await Context.Set<TEntity>().AddAsync(entity);
      
    }

    public void Remove(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
    
    }

    public TEntity Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}