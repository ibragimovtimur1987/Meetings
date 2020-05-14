using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Entities.Models
{
    public class UploadFile
    {
        public string Name { get; set; }

        public int ContentLength { get; set; }

        public bool IsEmpty
        {
            get { return ContentLength == 0; }
        }

        public Stream Stream { get; set; }
    }
}
