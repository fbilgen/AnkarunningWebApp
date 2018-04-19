using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public class RouteService : IRouteService<Route>
   {
      private IAnkarunningRepository<Route> _ankarunningRepository;

      //DI of repository to service
      public RouteService(IAnkarunningRepository<Route> ankarunningRepository)
      {
         this._ankarunningRepository = ankarunningRepository;
      }

      public IQueryable<Route> GetAllRoutes()
      {
         return _ankarunningRepository.GetAll();
      }

      public IQueryable<Route> GetRouteWithId(long id)
      {
         return _ankarunningRepository.Get(id);
      }

      public long InsertRoute(Route entity)
      {
         return _ankarunningRepository.Insert(entity);
      }

      public void SaveRouteChanges()
      {
         _ankarunningRepository.SaveChanges();
      }

      public void UpdateRoute(Route entity)
      {
         _ankarunningRepository.Update(entity);
      }
   }
}
