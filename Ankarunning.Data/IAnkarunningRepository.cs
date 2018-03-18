using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Data
{
   /// <summary>
   /// generic interface for crud operations
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public interface IAnkarunningRepository<T>
   {
      IQueryable<T> GetAll();
      IQueryable<T> Get(long id);
      long Insert(T entity);
      void Update(T entity);
      void Delete(T entity);
      void SaveChanges();
   }
}
