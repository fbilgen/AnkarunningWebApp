using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankarunning.Web
{
    public class Auth0Authorization
    {
      [JsonProperty("groups")]
      public string[] Groups { get; set; }
   }
}
