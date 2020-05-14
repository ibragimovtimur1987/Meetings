using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meeting.Services.Services.VimeoGetVideosService;

namespace Meeting.Services.Services.Interface
{
    public interface IVimeoGetVideosService
    {
        List<Video> GetVideos(string albumId);
    }
}
