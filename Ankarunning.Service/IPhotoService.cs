using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public interface IPhotoService<T> where T : class
   {

      IQueryable<T> GetAllPhotos();
      IQueryable<T> GetPhotoWithId(long id);
      T GetPhotoWithForeignKey(long id);
      long InsertPhoto(T entity);
      void UpdatePhoto(T entity);
      void DeletePhoto(T entity);
      void SavePhotoChanges();
   }
}
