using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ankarunning.Web.Models
{
    public class RouteViewModel
    {
      public long Id { get; set; }
      public string Name { get; set; }
      public decimal Distance { get; set; }
      public string Latitude { get; set; }
      public string Longitude { get; set; }

      public string FileName { get; set; }
      public string ContentType { get; set; }
      public byte[] Content { get; set; }

      public string PhotoFileName { get; set; }
      public string PhotoContentType { get; set; }
      public byte[] PhotoContent { get; set; }

      public IFormFile RouteFile { get; set; }
      public IFormFile RoutePhotoFile { get; set; }

   } 
}
