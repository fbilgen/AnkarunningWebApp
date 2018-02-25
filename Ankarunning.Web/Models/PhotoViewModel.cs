using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankarunning.Web.Models
{
    public class PhotoViewModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
