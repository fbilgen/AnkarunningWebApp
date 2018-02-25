using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ankarunning.Web.Models {
    public class TrainingViewModel {
        [Required]
        public string Name { get; set; }
        public string Place { get; set; }

        [Display(Name = "Training Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public TimeSpan Time { get; set; }
        public String Description { get; set; }
        public PhotoViewModel Photo {get;set;}

    }
}
