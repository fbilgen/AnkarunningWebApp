using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
    public class Training : BaseEntity
    {
        public string Title { get; set; }
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public String Description { get; set; }

        //navigation props
        public virtual TrainingPhoto TrainingPhoto { get; set; }
    }
}
