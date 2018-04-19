using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ankarunning.Web.Models
{
   public class TrainingViewModel
   {
      public long Id { get; set; }
      [Required]
      public string Title { get; set; }
      public string Route { get; set; }
      public long RouteId { get; set; }
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
      public DateTime Date { get; set; }

      [DataType(DataType.Time)]
      [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
      public DateTime Time { get; set; }

      [DataType(DataType.DateTime)]
      //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}")]
      public DateTime DateTime { get; set; }

      public String Description { get; set; }
      public decimal Distance { get; set; }
      public string AvgPace { get; set; }
      public IFormFile Photo { get; set; }

      public string PhotoName { get; set; }

      public TrainingPhotoViewModel TrainingPhoto { get; set; }

   }
}
