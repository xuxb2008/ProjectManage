using DomainDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDLL
{
    public interface IRepository<T> where T : PersistenceEntity
    {
        T Load(string id);
        T Get(string id);
        IList<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);

        T FindSingle(List<QueryField> queryList);

    }
}
