using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoDotNet.Models;

namespace Meeting.Entities.Models
{
    public class AlbumView
    {
        public Album album { get; set; }
        public string Password { get; set; }

        public bool isGetAlbum { get; set; }
    }
}
