using Ankarunning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Service
{
   public interface IParameterService<T> where T : BaseEntity
   {
      IQueryable<T> GetAllPlaces();
   }
}
