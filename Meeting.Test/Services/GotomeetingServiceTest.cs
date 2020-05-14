using Gotomeeting.Core.Models;
using LogMeIn.GoToCoreLib.Api;
using LogMeIn.GoToMeeting.Api;
using LogMeIn.GoToMeeting.Api.Model;
using Meeting.Services.Services.Interface;
using RestSharp;
using SpecialistMeeting;
using SpecialistRepl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Test.Services
{
    public class GotomeetingServiceTest : IGotomeetingService
    {
        public ISpecialistReplService specialistReplService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CreateMeeting(string accessToken, tGroups tGroup)
        {
            throw new NotImplementedException();
        }

        public string GetAccessToken(GtmApiSettings gtmApiSettings)
        {
            try
            {
                var authApi = new OAuth2Api(gtmApiSettings.consumerKey, gtmApiSettings.consumerSecret);
                var tokenResponse = authApi.DirectLogin(gtmApiSettings.userName, gtmApiSettings.userPassword);
                string accessToken = tokenResponse.access_token; // => "RlUe11faKeyCWxZToK3nk0uTKAL"
                return accessToken;
            }
            catch
            {
                return null;
            }
        }

        public IRestResponse GetLicenses(string accessToken)
        {
            var client = new RestClient("https://api.getgo.com/admin/rest/v1/accounts/200000000000215929/licenses");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            IRestResponse response = client.Execute(request);
            return response;
        }

        public List<HistoricalMeeting> GetListHistoricalMeeting(string accessToken, List<GtmUploadVideoLog> gtmUploadVideoLog)
        {
            DateTime? from = DateTime.Now.AddDays(-90);
            DateTime? to = DateTime.Now;
            MeetingsApi meetingsApi = new MeetingsApi();
            //MeetingCreated meetingReqCreate = new MeetingReqCreate
            //{
            //    meetingtype = MeetingType.recurring
            //}
            //  var ms =  meetingsApi.getMeeting(accessToken, null);
            List<HistoricalMeeting> historicalMeetings = meetingsApi.getHistoricalMeetings(accessToken, from, to);
            return historicalMeetings.Where(x => x.meetingId == "176111349" ).ToList();
        }

    }
}
