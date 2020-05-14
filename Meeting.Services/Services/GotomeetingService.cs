using Gotomeeting.Core.Models;
using LogMeIn.GoToCoreLib.Api;
using LogMeIn.GoToMeeting.Api;
using LogMeIn.GoToMeeting.Api.Model;
using Meeting.Services.Services.Interface;
using NLog;
using RestSharp;
using SpecialistMeeting;
using SpecialistRepl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meeting.Services.Services
{
    public class GotomeetingService : IGotomeetingService
    {
        [Dependency]
        public ISpecialistReplService specialistReplService { get; set; }

        // public ISpecialistReplService SpecialistReplService { get; set; }

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
        //public string code = "24cb27f7793a49388355215ddbff456b";
        //public string auth = "Basic MlhxVmU2cE81STFMbHhYYnp5RVR4bW50ZkpOdTlWT1Y6SXNpQUFCU1RDZ2NNQVNOVg";
        public void CreateMeeting(string accessToken, tGroups tGroup)
        {
          
                //List<string> gtmUploadVideoLogMeetingIds = gtmUploadVideoLog.Where(x => x.IsComplete == true).Select(x => x.MeetingId).ToList();
                //List<string> gtmUploadVideoLogSessionIdIds = gtmUploadVideoLog.Where(x => x.IsComplete == true).Select(x => x.SessionId).ToList();
                //DateTime? from = DateTime.Now.AddDays(-7);
                //DateTime? to = DateTime.Now;
                MeetingsApi meetingsApi = new MeetingsApi();
                string roomName = specialistReplService.GetNameRoom(tGroup.Group_ID);
                MeetingReqCreate meetingReqCreate = new MeetingReqCreate
                {
                    subject = "Test4",
                    coorganizerKeys = new List<string>(),
#pragma warning disable CS0612 // Type or member is obsolete
                    timezonekey = "America/Los_Angeles",
#pragma warning restore CS0612 // Type or member is obsolete
                    passwordrequired = false,  
                    conferencecallinfo = "hybrid",
                    starttime = DateTime.Now.AddMonths(1).ToUniversalTime(),
                    endtime = DateTime.Now.AddMonths(1).AddHours(1).ToUniversalTime(),
                    meetingtype = MeetingType.immediate,
                
                    //meetingtype = MeetingType.immediate
                };

                meetingsApi.createMeeting(accessToken, meetingReqCreate);
                //MeetingCreated meetingReqCreate = new MeetingReqCreate
                //{
                //    meetingtype = MeetingType.recurring
                ////}
                ////  var ms =  meetingsApi.getMeeting(accessToken, null);
                //List<HistoricalMeeting> historicalMeetings = meetingsApi.getHistoricalMeetings(accessToken, from, to);
                //List<HistoricalMeeting> historicalMeetingsList = historicalMeetings.Where(x => x.recording != null && !(gtmUploadVideoLogMeetingIds.Contains(x.meetingId) && gtmUploadVideoLogSessionIdIds.Contains(x.sessionId))).OrderByDescending(x => x.endTime).ToList();
                //List<HistoricalMeeting> listHistoricalMeeting = historicalMeetingsList.Where(x => x.recording != null).ToList();
                //return listHistoricalMeeting;
           
            // SaveVideoGtm(downloadUrl, path);
            // Console.WriteLine($"Файл скачен {DateTime.Now.ToLongTimeString()}");
            //AddToAlbumVideos(path);
            //meetings.ForEach(m => Trace.WriteLine(m.startTime));
        }
        public List<HistoricalMeeting> GetListHistoricalMeeting(string accessToken, List<GtmUploadVideoLog> gtmUploadVideoLog)
        {
            try
            {
                
                List<string> gtmUploadVideoLogMeetingIds = gtmUploadVideoLog.Where(x=>x.IsComplete == true).Select(x => x.MeetingId).ToList();
                List<string> gtmUploadVideoLogSessionIdIds = gtmUploadVideoLog.Where(x => x.IsComplete == true).Select(x => x.SessionId).ToList();
                DateTime? from = DateTime.Now.AddDays(-7);
                DateTime? to = DateTime.Now;
                MeetingsApi meetingsApi = new MeetingsApi();
                //MeetingCreated meetingReqCreate = new MeetingReqCreate
                //{
                //    meetingtype = MeetingType.recurring
                //}
                //  var ms =  meetingsApi.getMeeting(accessToken, null);
                List<HistoricalMeeting> historicalMeetings = meetingsApi.getHistoricalMeetings(accessToken, from, to);
                //есть запись в гтм и (не (данная группа и данная сессия была пройдена) = попадают в выброрку те у которых данной сессии и группы не пройдены)
                List<HistoricalMeeting> historicalMeetingsList = historicalMeetings.Where(x => x.recording != null && !(gtmUploadVideoLogMeetingIds.Contains(x.meetingId) && gtmUploadVideoLogSessionIdIds.Contains(x.sessionId))).OrderByDescending(x => x.endTime).ToList();
                List<HistoricalMeeting> listHistoricalMeeting = historicalMeetingsList.Where(x => x.recording != null).ToList();
                return listHistoricalMeeting;
            }
            catch
            {
                return null;
            }
            // SaveVideoGtm(downloadUrl, path);
            // Console.WriteLine($"Файл скачен {DateTime.Now.ToLongTimeString()}");
            //AddToAlbumVideos(path);
            //meetings.ForEach(m => Trace.WriteLine(m.startTime));
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
        //private string GetAccessToken()
        //{
        //    string accessToken;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.getgo.com/oauth/v2/token");
        //    request.Method = "POST"; // для отправки используется метод Post
        //                             // данные для отправки
        //    string data = $"grant_type=authorization_code&code={code}";
        //    //// преобразуем данные в массив байтов
        //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
        //    // устанавливаем тип содержимого - параметр ContentType
        //   // request.ContentType = "application/x-www-form-urlencoded";
        //    // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
        //    request.ContentLength = byteArray.Length;
        //    request.Headers.Add("Authorization", auth);
        //    request.ContentType = "application/json";
        //    //request.Accept = "application/json";
        //    //request.ContentType = "application/x-www-form-urlencoded";
        //    //записываем данные в поток запроса
        //    using (Stream dataStream = request.GetRequestStream())
        //    {
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //    }

        //    WebResponse response = request.GetResponse();
        //    using (Stream stream = response.GetResponseStream())
        //    {
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            accessToken = reader.ReadToEnd();
        //        }
        //    }
        //    response.Close();
        //    return accessToken;
        //    // Console.WriteLine("Запрос выполнен...");
        //}
        //private void GetCode()
        //{
        //    string accessToken;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.getgo.com/oauth/v2/authorize?client_id={consumerKey}&response_type=code");
        //    request.Method = "GET"; // для отправки используется метод Post
        //                             // данные для отправки
        //    //string data = $"grant_type=authorization_code&code={code}";
        //    ////// преобразуем данные в массив байтов
        //    //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
        //    //// устанавливаем тип содержимого - параметр ContentType
        //    //request.ContentType = "application/x-www-form-urlencoded";
        //    //// Устанавливаем заголовок Content-Length запроса - свойство ContentLength
        //    //request.ContentLength = byteArray.Length;
        //    //request.Headers.Add("Authorization", auth);
        //    //request.Accept = "application/json";
        //    //request.ContentType = "application/x-www-form-urlencoded";
        //    //записываем данные в поток запроса
        //    //using (Stream dataStream = request.GetRequestStream())
        //    //{
        //    //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    //}

        //    WebResponse response = request.GetResponse();
        //    using (Stream stream = response.GetResponseStream())
        //    {
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            code = reader.ReadToEnd();
        //        }
        //    }
        //    response.Close();
        //    //return accessToken;
        //}
    }
}
