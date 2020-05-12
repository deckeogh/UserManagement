using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(long id);
        void Add(TEntity entity);
        void Update(TEntity user, TEntity entity);
        void Delete(TEntity entity);
    }
}