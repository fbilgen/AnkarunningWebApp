using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public interface IRouteService<T> where T : Route
   {
      IQueryable<T> GetAllRoutes();
      IQueryable<T> GetRouteWithId(long id);
      long InsertRoute(T entity);
      void UpdateRoute(T entity);
      void SaveRouteChanges();
   }
}
