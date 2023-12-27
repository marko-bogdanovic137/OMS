using System;
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

        int Delete(T entity);

        int DeleteById(ID id);
    }
    public interface ICRUDDao2<T, ID>
    {
        int Save(T entity);

		IEnumerable<T> FindAll();


	}
	public interface ICRUDDao3<T, DateTime>
	{
		IEnumerable<T> FindAll(DateTime startDate, DateTime endDate);

        void FindKvar(string id);
	}
	public interface ICRUDDao4<T, ID>
	{
		int Save(T entity);

		IEnumerable<T> FindAll();

		int CountRowsWithId(string id);
	}
}
