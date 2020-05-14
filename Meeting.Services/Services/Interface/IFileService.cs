using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services.Interface
{
    public interface IFileService
    {
       bool SaveVideoGtm(string downloadUrl, string path);
       bool DeleteVideoGtm(string path);
    }
}
