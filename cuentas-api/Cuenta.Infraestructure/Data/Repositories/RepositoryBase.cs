using Cuenta.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Account.Domain.Entities.Base;

namespace Cuenta.Infraestructure.Data.Repositories
{
   public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
   {
      protected readonly DbSet<T> DbSet;
      protected readonly AccountContext DbContext;
      protected List<Expression<Func<T, object>>> PropertiesToInclude;
      public RepositoryBase(AccountContext dbContext)
      {
         DbSet = dbContext.Set<T>();
         DbContext = dbContext;
         PropertiesToInclude = new List<Expression<Func<T, object>>>();
      }
      public void SetProperties(List<Expression<Func<T, object>>>? propertiesToInclude)
      {
         if (propertiesToInclude != null && propertiesToInclude.Any()) PropertiesToInclude = propertiesToInclude;
      }

      public async Task<List<T>> GetAllAsync()
      {
         return await DbSet.AsNoTrackingWithIdentityResolution().ToListAsync();
      }

      public async Task<bool> Exists(int id)
      {
         var entity = await Get(id);

         return entity != null;
      }

      public async Task<T> AddAsync(T entity)
      {
         await DbSet.AddAsync(entity);
         return entity;
      }

      public async Task AddRangeAsync(IList<T> value)
      {
         await DbSet.AddRangeAsync(value);
      }

      public Task<bool> DeleteAsync(T entity)
      {
         DbSet.Remove(entity);
         return Task.FromResult(true);
      }

      public async Task<T?> Get(int id)
      {
         return await DbSet.FindAsync(id);
      }

      public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
      {
         return await DbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(expression);
      }

      public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, int? init = null, int? limit = null)
      {
         var query = PropertiesToInclude.Any() ? AllIncluding(PropertiesToInclude).Where(expression) : DbSet.Where(expression);

         if (init != null) query = query.Skip((int)init);
         if (limit != null) query = query.Take((int)limit);

         return await query.AsNoTrackingWithIdentityResolution().ToListAsync();
      }

      public Task<T> UpdateAsync(T entity)
      {
         DbSet.Update(entity);

         return Task.FromResult(entity);
      }

      public Task<int> SaveChangesAsync()
      {
         return DbContext.SaveChangesAsync();
      }

      private IQueryable<T> AllIncluding(List<Expression<Func<T, object>>> includeProperties)
      {
         var query = DbSet.AsQueryable();

         includeProperties.ForEach(includeProperty => query = query.Include(includeProperty));

         return query;
      }

   }
}
