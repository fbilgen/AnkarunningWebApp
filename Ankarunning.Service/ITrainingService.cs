using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
    public interface ITrainingService<T> where T : Training
    {
        IQueryable<T> GetAllTrainings();
        T GetTrainingWithId(long id);
        long InsertTraining(T entity);
        void UpdateTraining(T entity);
        void DeleteTraining(T entity);
        void SaveTrainingChanges();
    }
}
