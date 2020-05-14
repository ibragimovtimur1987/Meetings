using System;
using System.Linq;
using Gotomeeting.Core.Models;
using LogMeIn.GoToMeeting.Api;
using Meeting.Services.Services.Interface;
using Meeting.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Unity;
using static Meeting.Entities.Constants;

namespace Meeting.Test.Tests
{
    [TestClass]
    public class GotomeetingServiceTest
    {
        [TestMethod]
        public void GetUrlVideoGtm()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IGotomeetingService gotomeetingService = unity.Resolve<IGotomeetingService>();
            GtmApiSettings gtmApiSettingsManager = new GtmApiSettings
            {
                consumerKey = GtmConstants.ApiAuthManager.ConsumerKey,
                consumerSecret = GtmConstants.ApiAuthManager.ConsumerSecret,
                userName = GtmConstants.ApiAuthManager.UserName,
                userPassword = GtmConstants.ApiAuthManager.UserPassword
            };
            string accessTokenManager = gotomeetingService.GetAccessToken(gtmApiSettingsManager);
          //  gotomeetingService.GetListHistoricalMeeting(accessTokenManager);
        }
        [TestMethod]
        public void GetMeeting()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IGotomeetingService gotomeetingService = unity.Resolve<IGotomeetingService>();
            GtmApiSettings gtmApiSettingsManager = new GtmApiSettings
            {
                consumerKey = GtmConstants.ApiAuthwebinar6.ConsumerKey,
                consumerSecret = GtmConstants.ApiAuthwebinar6.ConsumerSecret,
                userName = GtmConstants.ApiAuthwebinar6.UserName,
                userPassword = GtmConstants.ApiAuthwebinar6.UserPassword
            };
            string accessTokenManager = gotomeetingService.GetAccessToken(gtmApiSettingsManager);

            MeetingsApi meetingsApi = new MeetingsApi();
            //MeetingCreated meetingReqCreate = new MeetingReqCreate
            //{
            //    meetingtype = MeetingType.recurring
            //}
            //  var ms =  meetingsApi.getMeeting(accessToken, null);
            var scheduledMeetings = meetingsApi.getUpcomingMeetings(accessTokenManager);
            //  var meeting = scheduledMeetings.FirstOrDefault(x => x.meetingId == "444885509");
            //  var meeting = scheduledMeetings.FirstOrDefault(x => x.meetingId == "592016749");
            var meeting = scheduledMeetings.FirstOrDefault(x => x.subject.Contains("317544"));
            //  gotomeetingService.GetListHistoricalMeeting(accessTokenManager);
        }
        [TestMethod]
        public void CreateMeeting()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IGotomeetingService gotomeetingService = unity.Resolve<IGotomeetingService>();
            ISpecialistReplService specialistReplService = unity.Resolve<ISpecialistReplService>();
            GtmApiSettings gtmApiSettingsManager = new GtmApiSettings
            {
                consumerKey = GtmConstants.ApiAuthManager.ConsumerKey,
                consumerSecret = GtmConstants.ApiAuthManager.ConsumerSecret,
                userName = GtmConstants.ApiAuthManager.UserName,
                userPassword = GtmConstants.ApiAuthManager.UserPassword
            };
            string accessTokenManager = gotomeetingService.GetAccessToken(gtmApiSettingsManager);
            SpecialistRepl.tGroups gr = specialistReplService.GettGroups().FirstOrDefault(x => x.Group_ID == 276305);
            gotomeetingService.specialistReplService = specialistReplService;
        //    gotomeetingService.CreateMeeting(accessTokenManager, gr);
          
            //MeetingCreated meetingReqCreate = new MeetingReqCreate
            //{
            //    meetingtype = MeetingType.recurring
            //}
            //  var ms =  meetingsApi.getMeeting(accessToken, null);
          
          
            //  gotomeetingService.GetListHistoricalMeeting(accessTokenManager);
        }
        [TestMethod]
        public void Test()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IGotomeetingService gotomeetingService = unity.Resolve<IGotomeetingService>();
            GtmApiSettings gtmApiSettingsManager = new GtmApiSettings
            {
                consumerKey = GtmConstants.ApiAuthManager.ConsumerKey,
                consumerSecret = GtmConstants.ApiAuthManager.ConsumerSecret,
                userName = GtmConstants.ApiAuthManager.UserName,
                userPassword = GtmConstants.ApiAuthManager.UserPassword
            };
            string accessTokenManager = gotomeetingService.GetAccessToken(gtmApiSettingsManager);

            //var client = new RestClient("https://api.getgo.com/admin/rest/v1/accounts/200000000000215929/licenses");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddHeader("Accept", "application/json");
            //request.AddHeader("Authorization",$"Bearer {accessTokenManager}");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);


            //var client = new RestClient("https://api.getgo.com/admin/rest/v1/accounts/200000000000215929/licenses/4441506211351419397/users/968450212590933253?suppressEmail=FALSE");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.DELETE);
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddHeader("Accept", "application/json");
            //request.AddParameter("application/x-www-form-urlencoded", "", ParameterType.RequestBody);
            //request.AddHeader("Authorization", $"Bearer {accessTokenManager}");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
        }

    }
}
