using LogMeIn.GoToMeeting.Api.Model;
using Meeting.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoDotNet;
using VimeoDotNet.Net;

namespace Meeting.Services.Services.Interface
{
    public interface IVimeoSevice
    {
        ISpecialistGtmService ISpecialistGtmService { get; set; }
        IMailService IMailService { get; set; }
        VimeoClient GetClient(string accessToken);

        Task<GroupRec> AddToVimeo(VimeoClient client, AddVimeoModel addVimeoModel);

        //void AddToAlbumVideos(string path, VimeoClient client, string numeAlbum,string password, IUploadRequest uploadRequest);

        string GetGrNumber(string number);

        long GetSpaceQuota(VimeoClient client);

        VimeoClient ChangeClientSpaceQuota(VimeoClient client, string path);
       // string CreatePassword(int length);

        //Task<IUploadRequest> AddVideo(string path, VimeoClient client);

        //void UpdateVideo(VimeoClient client, IUploadRequest uploadRequest, string password, string name);
    }
}
