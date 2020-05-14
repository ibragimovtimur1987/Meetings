using Gotomeeting.Core.Models;
using LogMeIn.GoToMeeting.Api.Model;
using Meeting.Entities.Models;
using Meeting.Services.Interface;
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
using VimeoDotNet;
using static Meeting.Entities.Constants;
using static Meeting.Entities.Constants.GtmConstants;

namespace Meeting.Services.Services
{
    public class MainService:IMainService
    {
        [Dependency]
        public IGotomeetingService GotomeetingService { get; set; }
        [Dependency]
        public IFileService FileService { get; set; }
        [Dependency]
        public IVimeoSevice VimeoSevice { get; set; }
        [Dependency]
        public ISpecialistGtmService SpecialistGtmService { get; set; }
        [Dependency]
        public ISpecialistReplService SpecialistReplService { get; set; }
        [Dependency]
        public IPioneerService PioneerService { get; set; }
        [Dependency]
        public IMailService MailService { get; set; }
        [Dependency]
        public IGetGroupService GetGroupService { get; set; }
        [Dependency]
        public ISpecialistWebService SpecialistWebService { get; set; }
        public Logger logger { get; set; } 

        public MainService(IUnityContainer unity)
        {
            //IGotomeetingService gotomeetingService = unity.Resolve<IGotomeetingService>();
            //IFileService fileService = unity.Resolve<IFileService>();
            //IVimeoSevice vimeoSevice = unity.Resolve<IVimeoSevice>();
            //ISpecialistGtmService specialistGtmService = unity.Resolve<ISpecialistGtmService>();
            //ISpecialistReplService specialistReplService = unity.Resolve<ISpecialistReplService>();
            //IPioneerService pioneerService = unity.Resolve<IPioneerService>();
            //IMailService mailService = unity.Resolve<IMailService>();
            //IGetGroupService getGroupService = unity.Resolve<IGetGroupService>();
            //GotomeetingService = gotomeetingService;
            //FileService = fileService;
            //VimeoSevice = vimeoSevice;
            //SpecialistGtmService = specialistGtmService;
            //SpecialistReplService = specialistReplService;
            //PioneerService = pioneerService;
            //MailService = mailService;
            //GetGroupService = getGroupService;
          //  vimeoSevice.IMailService = mailService;
           // vimeoSevice.ISpecialistGtmService = specialistGtmService;
           // GotomeetingService.specialistReplService = specialistReplService;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void CreateMeetings()
        {
            logger.Info($"Старт CreateMeetings");
            //Получаем нужный список групп из бд
            ListGroups listGroups = GetGroupService.GetListGroups();
            logger.Info($"Будет пройдено групп standartGroupsWebinar.Count = {listGroups.standartGroupsWebinar.Groups.Count}");


        }
        public void TransferVideo(int skipWebinarLicenses, int takeWebinarLicenses)
        {
            //List<string> listWebLicenseLogin = new List<string>
            //{

            //    ApiAuthwebinar6.UserName,
             
            //};
            List<tWebinarLicenses> allWebinars = SpecialistReplService.GettWebinarLicenses().OrderBy(x=>x.LicenseLogin).ToList();
            List<tWebinarLicenses> webinars = allWebinars.Skip(skipWebinarLicenses).Take(takeWebinarLicenses).ToList();
            LoggerCountHistoryMeetingForWebinarLicenses(webinars);
            foreach (tWebinarLicenses webinar in webinars)
            {
                try
                {
                    logger.Info($"Старт трансфера лицензии webinarLicenseName {webinar.LicenseName} webinarLicenseId {webinar.WebinarLicense_ID}");
                    List<GtmUploadVideoLog> gtmUploadVideoLog = SpecialistGtmService.GtmUploadVideoLog().Where(x => x.WebinarLicenseId == webinar.WebinarLicense_ID).ToList();
                    GtmApiSettings gtmApiSettings = new GtmApiSettings
                    {
                        consumerKey = GtmConstants.ApiAuthManager.ConsumerKey,
                        consumerSecret = GtmConstants.ApiAuthManager.ConsumerSecret,
                        userName = webinar.LicenseLogin,
                        userPassword = webinar.LicensePassword
                    };
                    string accessToken = GotomeetingService.GetAccessToken(gtmApiSettings);
                    if (accessToken == null) continue;

                    List<HistoricalMeeting> listHistoricalMeeting = GotomeetingService.GetListHistoricalMeeting(accessToken, gtmUploadVideoLog);
                    if (listHistoricalMeeting == null || listHistoricalMeeting.Count == 0) continue;
                    logger.Info($"{webinar.LicenseName} exsist listHistoricalMeeting {listHistoricalMeeting.Count}");
                    foreach (HistoricalMeeting historicalMeeting in listHistoricalMeeting)
                    {
                        try
                        {
                            if (historicalMeeting.recording == null || historicalMeeting.meetingId == null || historicalMeeting.sessionId == null || String.IsNullOrEmpty(historicalMeeting.recording.downloadUrl))
                            {
                                continue;
                            }
                            if (historicalMeeting.subject == "Meet Now") continue;
                            logger.Info($"Старт сессии {historicalMeeting.sessionId} собрания {historicalMeeting.subject}");
                            if (historicalMeeting.recording != null) logger.Info($"historicalMeetingrecordingdownloadUrl = {historicalMeeting.recording.downloadUrl}");

                            string numberGr = "";
                            if (historicalMeeting.subject != null) numberGr = VimeoSevice.GetGrNumber(historicalMeeting.subject);
                            decimal? grouid = null;
                            string nameAlbum = null;
                            if (numberGr == null || numberGr == "")
                            {
                                string error = $"Номер группы не получилось получить из названия собрания в gtm {historicalMeeting.subject}";
                                logger.Info(error);
                                nameAlbum = "NotFoundGroups";
                            }
                            else
                            {
                                grouid = Convert.ToDecimal(numberGr);
                                nameAlbum = SpecialistReplService.GetNameAlbum(numberGr);
                            }
                            string gtmFolderPath = System.Configuration.ConfigurationManager.AppSettings[GtmConstants.FileConstants.GtmFolderPath];
                            string path = $"{gtmFolderPath}\\webinarLicenseId{webinar.WebinarLicense_ID}meetingId{historicalMeeting.meetingId}sessionId{historicalMeeting.sessionId}{GtmConstants.FileConstants.Expansion}";
                            bool isSaved = FileService.SaveVideoGtm(historicalMeeting.recording.downloadUrl, path);
                            logger.Info($"{path} isSaved");
                            if (numberGr != null) logger.Info($"numberGr = {numberGr}");

                            if (isSaved)
                            {
                                VimeoClient client = VimeoSevice.GetClient(VimeoConstants.ApiAuthManager.AccessTokenWebinarManager);

                                client = VimeoSevice.ChangeClientSpaceQuota(client, path);

                                Task<Entities.Models.GroupRec> taskAddToVimeo = VimeoSevice.AddToVimeo(client,
                                    new AddVimeoModel { path = path, nameVideo = historicalMeeting.subject, numeAlbum = nameAlbum, groupId = grouid, meetingId = historicalMeeting.meetingId, sessionId = historicalMeeting.sessionId, webinarLicenseId = webinar.WebinarLicense_ID });
                                taskAddToVimeo.Wait();
                                if (taskAddToVimeo.IsCompleted && taskAddToVimeo.Result != null)
                                {
                                    logger.Info($"Видео {path} загружено в вимео");
                                    if (grouid != null)
                                    {
                                        if (taskAddToVimeo.Result.AlbumId != IdNotFoundGroups.NotFoundGroupsWebMan && taskAddToVimeo.Result.AlbumId != IdNotFoundGroups.NotFoundGroupsMan)
                                        {
                                            if (!taskAddToVimeo.Result.IsGetAlbum)
                                            {
                                                PioneerService.UpdateGroup(grouid.Value, taskAddToVimeo.Result);
                                                logger.Info($"Обновили в базе пионер у группы {grouid} ссылку на запись");
                                            }
                                            SpecialistWebService.RefreshAlbum(taskAddToVimeo.Result.AlbumId);
                                            logger.Info($"Обновили в albumId в бд specialistWeb");
                                        }
                                    }
                                    SpecialistGtmService.UpdateUploadVideoLog(historicalMeeting.meetingId, historicalMeeting.sessionId, true, webinar.WebinarLicense_ID);
                                    logger.Info($"Обновили в бд в таблице логов у meetingId {historicalMeeting.meetingId} sessionId {historicalMeeting.sessionId} поле isComplete завершено");
                                    bool isDelete = FileService.DeleteVideoGtm(path);

                                }
                                else
                                {
                                    logger.Info($"Произошла ошибка при добавлении в вимео у historicalMeetingId {historicalMeeting.meetingId}", $"Произошла ошибка у historicalMeetingId {historicalMeeting.subject}");
                                    MailService.SendErrorDeveloper($"Произошла ошибка при добавлении в вимео у historicalMeetingId {historicalMeeting.meetingId}", $"Произошла ошибка у historicalMeetingId {historicalMeeting.subject}");
                                }
                            }
                            logger.Info($"Завершено сессия {historicalMeeting.sessionId} собрания {historicalMeeting.subject}");
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                            MailService.SendErrorDeveloper(ex.ToString(), $"Произошла ошибка у historicalMeetingId {historicalMeeting.meetingId}");
                        }
                        //  return;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error($"webinarLicense_ID {webinar.WebinarLicense_ID} {ex.ToString()}");
                    MailService.SendErrorDeveloper(ex.ToString(), $"Произошла ошибка у webinarLicenseName {webinar.LicenseName}");
                }
            }
            logger.Info($"Финиш TransferVideo");
        }

        private void LoggerCountHistoryMeetingForWebinarLicenses(List<tWebinarLicenses> webinars)
        {
            int CountWebinar = 0;
            foreach (tWebinarLicenses webinar in webinars)
            {
                try
                {
                    logger.Info($"Сканирование лицензии webinarLicenseName {webinar.LicenseName} webinarLicenseId {webinar.WebinarLicense_ID}");
                    List<GtmUploadVideoLog> gtmUploadVideoLog = SpecialistGtmService.GtmUploadVideoLog().Where(x => x.WebinarLicenseId == webinar.WebinarLicense_ID).ToList();
                    GtmApiSettings gtmApiSettings = new GtmApiSettings
                    {
                        consumerKey = GtmConstants.ApiAuthManager.ConsumerKey,
                        consumerSecret = GtmConstants.ApiAuthManager.ConsumerSecret,
                        userName = webinar.LicenseLogin,
                        userPassword = webinar.LicensePassword
                    };
                    string accessToken = GotomeetingService.GetAccessToken(gtmApiSettings);
                    if (accessToken == null) continue;

                    List<HistoricalMeeting> listHistoricalMeeting = GotomeetingService.GetListHistoricalMeeting(accessToken, gtmUploadVideoLog);
                    if (listHistoricalMeeting == null || listHistoricalMeeting.Count == 0) continue;
                    logger.Info($"Сканирование лицензии {webinar.LicenseName} exsist listHistoricalMeeting {listHistoricalMeeting.Count}");
                    CountWebinar = CountWebinar + listHistoricalMeeting.Count;
                }
               
                catch (Exception ex)
                {
                    logger.Error(ex);
                    //  MailService.SendErrorDeveloper(ex.ToString(), $"Произошла ошибка у historicalMeetingId {historicalMeeting.meetingId}");
                }
                
            }
            logger.Info($"Сканирование осталось записей {CountWebinar}");
        }
    }
}
