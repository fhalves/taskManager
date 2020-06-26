using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T Get(object id);

        IEnumerable<T> GetAll();

        T Post(T obj);

        T Put(T obj);

        T Delete(T obj);
    }
}
