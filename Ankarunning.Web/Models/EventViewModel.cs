using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ankarunning.Web.Models
{
    public class EventViewModel
    {
      public long Id { get; set; }
      [Required(ErrorMessage ="Please enter")]
      public string Title { get; set; }
      [Required(ErrorMessage = "Please enter")]
      public string Location { get; set; }
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
      public DateTime DateStart { get; set; }
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
      public DateTime? DateEnd { get; set; }
      public String Description { get; set; }   
      public IFormFile Photo { get; set; }
      public string PhotoName { get; set; }

      public PhotoViewModel EventPhoto { get; set; }
   }
}
