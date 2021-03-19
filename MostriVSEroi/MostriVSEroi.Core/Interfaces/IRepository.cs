using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi.Core.Interfaces
{
    /// <summary>
    /// Interfaccia <c>IRepository</c> contenente metodi Crud per tutte le altre interfacce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {

        /// Metodi CRUD

        ///Create
        bool Create(T obj);

        ///Read
        T GetByID(int ID);

        IEnumerable<T> GetAll();

        ///Update
        bool Update(T obj);

        ///Delete
        bool Delete(T obj);

    }
}
