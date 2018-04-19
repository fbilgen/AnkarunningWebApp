using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public class EventPhotoService : IPhotoService<EventPhoto>
   {
      private IAnkarunningRepository<EventPhoto> _ankarunningRepository;

      //DI of repository to service
      public EventPhotoService(IAnkarunningRepository<EventPhoto> ankarunningRepository)
      {
         this._ankarunningRepository = ankarunningRepository;
      }


      public void DeletePhoto(EventPhoto entity)
      {
         _ankarunningRepository.Delete(entity);
      }

      public IQueryable<EventPhoto> GetAllPhotos()
      {
         throw new NotImplementedException();
      }

      public EventPhoto GetPhotoWithForeignKey(long id)
      {
         return _ankarunningRepository.GetAll().Where(e => e.EventId == id).FirstOrDefault();
      }

      public IQueryable<EventPhoto> GetPhotoWithId(long id)
      {
         return _ankarunningRepository.Get(id);
      }

      public long InsertPhoto(EventPhoto entity)
      {
         return _ankarunningRepository.Insert(entity);
      }

      public void SavePhotoChanges()
      {
         _ankarunningRepository.SaveChanges();
      }

      public void UpdatePhoto(EventPhoto entity)
      {
         throw new NotImplementedException();
      }
   }
}
