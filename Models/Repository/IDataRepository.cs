using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UserManagement.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(long id);
        bool Add(TEntity entity);
        bool Update(TEntity user, TEntity entity);
        bool Delete(TEntity entity);
        bool Login(TEntity entity);
    }
}