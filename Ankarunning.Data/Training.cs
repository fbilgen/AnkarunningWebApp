using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
   public class Training : BaseEntity
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public long TrainingPlaceId { get; set; }
      public DateTime DateTime { get; set; }
      public Int16 Distance { get; set; }

      //navigation props
      public virtual TrainingPhoto TrainingPhoto { get; set; }
      public virtual TrainingPlace TrainingPlace { get; set; }
   }
}
