using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public interface IEventService<T> where T : Event
   {
      IQueryable<T> GetAllEvents();
      IQueryable<T> GetEventWithId(long id);
      long InsertEvent(T entity);
      void UpdateEvent(T entity);
      void DeleteEvent(T entity);
      void SaveEventChanges();
   }
}
