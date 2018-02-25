using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
    public class TrainingPhotoService : IPhotoService<TrainingPhoto> {

        private IAnkarunningRepository<TrainingPhoto> _ankarunningRepository;

        //DI of repository to service
        public TrainingPhotoService(IAnkarunningRepository<TrainingPhoto> ankarunningRepository) {
            this._ankarunningRepository = ankarunningRepository;
        }

        public void DeletePhoto(TrainingPhoto entity) {
            _ankarunningRepository.Delete(entity);
        }

        public IQueryable<TrainingPhoto> GetAllPhotos() {
            throw new NotImplementedException();
        }

        public TrainingPhoto GetPhotoWithForeignKey(long id) {
            return _ankarunningRepository.GetAll().Where(e => e.TrainingId == id).FirstOrDefault();
        }

        public TrainingPhoto GetPhotoWithId(long id) {
          return _ankarunningRepository.Get(id);
        }

        public long InsertPhoto(TrainingPhoto entity) {
            return _ankarunningRepository.Insert(entity);
        }

        public void SavePhotoChanges() {
            _ankarunningRepository.SaveChanges();
        }

        public void UpdatePhoto(TrainingPhoto entity) {
            throw new NotImplementedException();
        }
    }
}
