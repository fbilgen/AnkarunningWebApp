using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
   public class Training : BaseEntity
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public DateTime DateTime { get; set; }
      public string AvgPace { get; set; }
      public long RouteId { get; set; }

      //navigation props
      public virtual TrainingPhoto TrainingPhoto { get; set; }
      public virtual Route Route { get; set; }
   }
}
