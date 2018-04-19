using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
   public class Route : BaseEntity
   {
      public string Name { get; set; }
      public decimal Distance { get; set; }
      public string Latitude { get; set; }
      public string Longitude { get; set; }
      public string FileName { get; set; }
      public string ContentType { get; set; }
      public byte[] Content { get; set; }

      //photo
      public string PhotoFileName { get; set; }
      public string PhotoContentType { get; set; }
      public byte[] PhotoContent { get; set; }

   }
}
