using System;
using System.Collections.Generic;
using System.Text;
using VimeoDotNet;

namespace Vimeo.Core.Services.Inerface
{
    public interface IVimeoSevice
    {
        VimeoClient GetClient(string accessToken);
        void AddToAlbumVideos(string path, VimeoClient client);
    }
}
