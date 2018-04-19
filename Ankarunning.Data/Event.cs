using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
    public class Event : BaseEntity
    {
      public string Title { get; set; }
      public string Description { get; set; }
      public DateTime DateTimeStart { get; set; }
      public DateTime? DateTimeEnd { get; set; }
      public string Location { get; set; }

      //navigation props
      public virtual EventPhoto EventPhoto { get; set; }

   }
}
