using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;
public abstract class GenericRepository<T> where T : class
{    
    protected readonly DbSet<T> _entity;
    public GenericRepository(DbContext context)=>_entity = context.Set<T>();

    #region CRUD
    
        protected virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression is not null){
                return await _entity.Where(expression).ToListAsync();
            }
            return await _entity.ToListAsync();
        } 
        public async virtual Task<T> FindFirst(Expression<Func<T, bool>> expression)
        {
            if (expression != null){
                var rst = await _entity.Where(expression).ToListAsync();
                return rst.First();
            }
            return await _entity.FirstAsync();
        }

        public async virtual void Add(T entity) => await _entity.AddAsync(entity);
        public async virtual void AddRange(IEnumerable<T> entities) => await _entity.AddRangeAsync(entities);
        public virtual void Remove(T entity) => _entity.Remove(entity);
        public virtual void RemoveRange(IEnumerable<T> entities) => _entity.RemoveRange(entities);
        public virtual void Update(T entity) => _entity.Update(entity);

    #endregion


    #region GetAllAsync

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await GetAll();
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression) => await GetAll(expression);
        public virtual async Task<IEnumerable<T>> GetAllAsync(IParam param) => await GetAllPaginated(param);
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, IParam param) => await GetAllPaginated(param, expression);

    #endregion


    #region pagination

        protected virtual bool PaginateExpression(T entity, string Search)=> true;
        private async Task<IEnumerable<T>> GetAllPaginated(IParam param, Expression<Func<T, bool>> expression = null)
        {
            return (await GetAll(expression))
                    .Where(x => PaginateExpression(x,param.Search))
                    .Skip((param.PageIndex - 1) * param.PageSize)
                    .Take(param.PageSize)
                    .ToList();
        }

    #endregion
    
}