using System.Collections.Generic;

namespace OMS.DAO
{
    public interface ICRUDDao<T, ID>
    {
        int Count();

        IEnumerable<T> FindAll();

        int Save(T entity);

        int SaveAll(IEnumerable<T> entities);

        bool ExistById(ID id);

        T FindById(ID id);
    }
}
