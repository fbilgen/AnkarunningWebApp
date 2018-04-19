using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public class EventService : IEventService<Event>
   {
      private IAnkarunningRepository<Event> _ankarunningRepository;

      //DI of repository to service
      public EventService(IAnkarunningRepository<Event> ankarunningRepository)
      {
         this._ankarunningRepository = ankarunningRepository;
      }

      public void DeleteEvent(Event entity)
      {
         throw new NotImplementedException();
      }

      public IQueryable<Event> GetAllEvents()
      {
         return _ankarunningRepository.GetAll();
      }

      public IQueryable<Event> GetEventWithId(long id)
      {
         return _ankarunningRepository.Get(id);
      }

      public long InsertEvent(Event entity)
      {
         return _ankarunningRepository.Insert(entity);
      }

      public void SaveEventChanges()
      {
         _ankarunningRepository.SaveChanges();
      }

      public void UpdateEvent(Event entity)
      {
         _ankarunningRepository.Update(entity);
      }
   }
}
