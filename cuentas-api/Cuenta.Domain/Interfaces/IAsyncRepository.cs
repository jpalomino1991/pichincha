using System.Linq.Expressions;
using Account.Domain.Entities.Base;

namespace Cuenta.Domain.Interfaces
{
   public interface IAsyncRepository<T> where T : BaseEntity
   {
      void SetProperties(List<Expression<Func<T, object>>>? propertiesToInclude);

      Task<T?> Get(int id);

      Task<T?> GetAsync(Expression<Func<T, bool>> expression);

      Task<List<T>> GetAllAsync();

      Task<bool> Exists(int id);

      Task<T> AddAsync(T entity);

      Task AddRangeAsync(IList<T> value);

      Task<T> UpdateAsync(T entity);

      Task<bool> DeleteAsync(T entity);

      Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, int? init = null, int? limit = null);

      Task<int> SaveChangesAsync();
   }
}
