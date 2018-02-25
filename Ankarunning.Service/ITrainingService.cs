using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
    public interface ITrainingService<T> where T : Training
    {
        IQueryable<Training> GetAllTrainings();
        Training GetTrainingWithId(long id);
        long InsertTraining(Training entity);
        void UpdateTraining(T entity);
        void DeleteTraining(T entity);
        void SaveTrainingChanges();
    }
}
