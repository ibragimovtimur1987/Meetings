using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Entities.Models
{
    public class GroupRec
    {

        public string WbnRecLogin { get; set; } = "";
        public string WbnRecPwd { get; set; } = "";
        public string WebinarRecordURL { get; set; } = "";
        public long AlbumId { get; set; } =0;

        public string AlbumName{ get; set; } = "";

        public bool IsGetAlbum = false;
    }
}
