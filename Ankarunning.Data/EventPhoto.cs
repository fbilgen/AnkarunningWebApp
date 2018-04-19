using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
    public class EventPhoto : BaseEntity
    {
      public string Name { get; set; }
      public string ContentType { get; set; }
      public byte[] Content { get; set; }

      //1-1 relation to Training
      public long EventId { get; set; }
      //nav prop
      public virtual Event Event { get; set; }
   }
}
