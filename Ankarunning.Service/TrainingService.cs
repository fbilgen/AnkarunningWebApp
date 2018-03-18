using System;
using System.Linq;
using Ankarunning.Data;

namespace Ankarunning.Service
{
    public class TrainingService : ITrainingService<Training> {

        private IAnkarunningRepository<Training> _ankarunningRepository;

        //DI of repository to service
        public TrainingService(IAnkarunningRepository<Training> ankarunningRepository) {
            this._ankarunningRepository = ankarunningRepository;
        }
        public IQueryable<Training> GetAllTrainings() {
           return _ankarunningRepository.GetAll();
        }

        public IQueryable<Training> GetTrainingWithId(long id) {
            return _ankarunningRepository.Get(id);
        }

        public long InsertTraining(Training entity) {
            return _ankarunningRepository.Insert(entity);
        }
    
        public void DeleteTraining(Training entity) {
            _ankarunningRepository.Delete(entity);
        }


        public void UpdateTraining(Training entity) {
            _ankarunningRepository.Update(entity);
        }

        public void SaveTrainingChanges() {
            _ankarunningRepository.SaveChanges();
        }
    }
}
