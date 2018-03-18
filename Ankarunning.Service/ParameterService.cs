using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public class ParameterService : IParameterService<TrainingPlace>
   {
      private IAnkarunningRepository<TrainingPlace> _ankarunningRepository;

      //DI of repository to service
      public ParameterService(IAnkarunningRepository<TrainingPlace> ankarunningRepository)
      {
         this._ankarunningRepository = ankarunningRepository;
      }

      public IQueryable<TrainingPlace> GetAllPlaces()
      {
         return _ankarunningRepository.GetAll();
      }
   }
}
