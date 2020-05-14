using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meeting.Services.Services.Interface
{
    public interface ISpecialistWebService
    {
        void RefreshAlbum(long albumId);
        [Dependency]
        ISpecialistGtmService SpecialistGtmService { get; set; }
        [Dependency]
        IVimeoGetVideosService VimeoGetVideosService { get; set; }
    }
}
