using LogMeIn.GoToMeeting.Api.Model;
using Meeting.Entities.Models;
using Meeting.Services.Services.Interface;
using NLog;
using SpecialistMeeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unity;
using VimeoDotNet;
using VimeoDotNet.Models;
using VimeoDotNet.Net;
using VimeoDotNet.Parameters;
using static Meeting.Entities.Constants;

namespace Meeting.Services.Services
{
    public class VimeoServiceTest : IVimeoSevice
    {
        public ISpecialistGtmService ISpecialistGtmService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMailService IMailService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<GroupRec> AddToVimeo(VimeoClient client, AddVimeoModel addVimeoModel)
        {
            return null;

         
        }

        public VimeoClient ChangeClientSpaceQuota(VimeoClient client, string path)
        {
            throw new NotImplementedException();
        }

        public VimeoClient GetClient(string accessToken)
        {
            throw new NotImplementedException();
        }

        public string GetGrNumber(string number)
        {
            throw new NotImplementedException();
        }

        public long GetSpaceQuota(VimeoClient client)
        {
            throw new NotImplementedException();
        }
    }
}
