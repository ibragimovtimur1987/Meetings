using Gotomeeting.Core.Models;
using LogMeIn.GoToMeeting.Api.Model;
using Pioneer;
using RestSharp;
using SpecialistMeeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services.Interface
{
    public interface IGotomeetingService
    {
        ISpecialistReplService specialistReplService { get; set; }
        string GetAccessToken(GtmApiSettings gtmApiSettings);
        List<HistoricalMeeting> GetListHistoricalMeeting(string accessToken, List<GtmUploadVideoLog> gtmUploadVideoLog);

        IRestResponse GetLicenses(string accessToken);
        void CreateMeeting(string accessToken, SpecialistRepl.tGroups tGroup);
    }
}
