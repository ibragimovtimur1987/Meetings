using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotomeeting.Core.Models
{

    public class GtmApiSettings
    {
       public string userName { get; set; }= "";
       public string userPassword { get; set; } =  "";
       public string consumerKey { get; set; } = "";
       public string consumerSecret { get; set; } = "";
    }
}
