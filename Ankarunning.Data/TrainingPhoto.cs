using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
    public class TrainingPhoto : BaseEntity
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        //1-1 relation to Training
        public long TrainingId { get; set; }
        //nav prop
        public virtual Training Training { get; set; }
    }
}
