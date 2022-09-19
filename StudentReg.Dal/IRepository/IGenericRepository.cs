using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.Dal.IRepository
{
    public interface IGenericRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetByID(object id);

        TEntity Insert(TEntity entity);

        void InsertRange(IList<TEntity> entityList);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void DeleteRange(IEnumerable<TEntity> lstEntity);

        void Update(TEntity entityToUpdate);

        void TruncateTable(TEntity entity);
    }
}
