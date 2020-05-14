using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Entities.Models
{
    public class AddVimeoModel
    {
        public string path { get; set; } = "";
        public string nameVideo { get; set; } = "";
        public string numeAlbum { get; set; } = "";
        public decimal? groupId { get; set; } = null;
        public string meetingId { get; set; } = "";
        public string sessionId { get; set; } = "";

        public decimal webinarLicenseId { get; set; }
    }
}
