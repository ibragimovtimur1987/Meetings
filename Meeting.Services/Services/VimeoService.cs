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
    public class VimeoService : IVimeoSevice
    {
        [Dependency]
        public IMailService IMailService { get; set; }
        [Dependency]
        public ISpecialistGtmService ISpecialistGtmService { get; set; }
       
        Logger logger = LogManager.GetCurrentClassLogger();
        public VimeoClient GetClient(string accessToken)
        {
            return new VimeoClient(accessToken);
            // return new VimeoClient("3e8d4a50fc996396910bd889d761acedf1b39205","9nFyllosEQ8Igz9dxTbRRzDh/0JPHVrZX9A7Ue3FdnzIRizgfnSSgz/hu3r6slC47bGuvoZjOkTduewXt52yU2FtsxWLSXTtBGj9yfGTHsyQ4UiiHVHowdHLeESlCaZl");
        }


        public async Task<GroupRec> AddToVimeo(VimeoClient client, AddVimeoModel addVimeoModel)
        {
            bool result = false;

          //  string password = CreatePassword(12);

            AlbumView albumView = await GetCreateAlbum(client, addVimeoModel);

            long? albumId = albumView.album.GetAlbumId();
            if (albumId == null)
            {
                logger.Warn($"Видео albumId null");
                return null;
            }
               
            long? clipId = await GetCreateVideo(client, addVimeoModel, albumId.Value);
            if (clipId == null)
            {
                logger.Info($"clipId равно null");
                return null;
            }

            await UpdateVideo(client, clipId.Value, albumView.Password, addVimeoModel.nameVideo);
            logger.Info($"Файл { addVimeoModel.path} изменен в вимео");
          
            result = await AddVideoToAlbum(client, albumId.Value, clipId.Value);
            logger.Info($"Видео добавлено в альбом result= {result}");

          

            return new GroupRec()
            {
                WbnRecPwd  = albumView.Password,
                WebinarRecordURL = albumView.album.Link,
                AlbumId = albumId.Value,
                AlbumName = albumView.album.Name,
                IsGetAlbum = albumView.isGetAlbum
            };
        }
        //private async Task<long?> GetAlbumVideos(VimeoClient client, AddVimeoModel addVimeoModel, long albumId)
        //{
        //    client.GetAlbumVideosAsync()
        //}
        private async Task<long?> GetCreateVideo(VimeoClient client, AddVimeoModel addVimeoModel,long albumId)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.DefaultConnectionLimit = 9999;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            GtmUploadVideoLog gtmUploadVideoLog = ISpecialistGtmService.GtmUploadVideoLog().FirstOrDefault(x => x.MeetingId == addVimeoModel.meetingId && x.SessionId == addVimeoModel.sessionId && x.WebinarLicenseId == addVimeoModel.webinarLicenseId);
            if (gtmUploadVideoLog == null)
            {
                // IUploadRequest completedRequest;
                using (var file = new BinaryContent(addVimeoModel.path))
                {
                    logger.Info($"Начинаем загрузку файла {addVimeoModel.path} в вимео");
                    long size = new System.IO.FileInfo(addVimeoModel.path).Length;
                    logger.Info($"Размер файла {size}");
                    IUploadRequest completedRequest = await client.UploadEntireFileAsync(file,3048576);
                    logger.Info($"Файл {addVimeoModel.path} добавлен в вимео");                  
                    ISpecialistGtmService.AddUploadVideoLog(new GtmUploadVideoLog
                    {
                        MeetingId = addVimeoModel.meetingId,
                        GtmAlbumLogId = Convert.ToInt32(albumId),                      
                        ClipId = completedRequest?.ClipId.Value,
                        ClipUri = completedRequest.ClipUri,
                        PathFile = addVimeoModel.path,
                        GtmHistoricalMeetingSubject = addVimeoModel.nameVideo,
                        SessionId = addVimeoModel.sessionId,
                        WebinarLicenseId = addVimeoModel.webinarLicenseId
                    });

                    return completedRequest?.ClipId.Value;
                }
             
            }
            else
            {
                Video video = await client.GetVideoAsync(gtmUploadVideoLog.ClipId.Value);
                logger.Info($"Файл {addVimeoModel.path} получен в вимео");

                //if(albumView.NeedRefreshPasswordVideo)
                //{
                //    await client.UpdateVideoMetadataAsync(gtmUploadVideoLog.ClipId.Value, new VideoUpdateMetadata
                //    {
                //        Password = albumView.Password
                //    });
                //    logger.Info($"Обновили пароль у видео в вимео");
                //}
                return video?.Id;
            }

        }

        //public async Task<Paginated<Video>> GetAlbumVideos(VimeoClient client,long userId, long albumId)
        //{
        //    Task<Paginated<Video>> videos = client.GetAlbumVideosAsync(client.GetUserInformationAsync, albumId, 100);
        //    return await videos;
        //}
        private async Task<AlbumView> GetCreateAlbum(VimeoClient client, AddVimeoModel addVimeoModel)
        {

            SpecialistMeeting.GtmAlbumLog gtmAlbumLog = ISpecialistGtmService.GtmAlbumLog().FirstOrDefault(x => x.AlbumName == addVimeoModel.numeAlbum);
            Album album = null;
            string password = "";
            bool isGetAlbum = false;
            if (gtmAlbumLog == null)
            {
                password = CreatePassword(12);
                if (addVimeoModel.numeAlbum == "NotFoundGroups")
                {

                    var info = await client.GetAccountInformationAsync();
                    if (info.Id == 85169858)
                    {
                        album = await client.GetAlbumAsync(UserId.Me, IdNotFoundGroups.NotFoundGroupsWebMan);//NotFoundGroups in TokenWebinarManager
                    }
                    else
                    {
                        album = await client.GetAlbumAsync(UserId.Me, IdNotFoundGroups.NotFoundGroupsMan);//NotFoundGroups in TokenManager
                    }
                    logger.Info($"Получен альбом { album.Name}");
                }
                else
                {
                    album = await client.CreateAlbumAsync(UserId.Me, new EditAlbumParameters
                    {
                        Name = addVimeoModel.numeAlbum,
                        Description = addVimeoModel.numeAlbum,
                        Sort = EditAlbumSortOption.Newest,
                        Privacy = EditAlbumPrivacyOption.Password,
                        Password = password,
                    });
                    logger.Info($"Создан альбом { album.Name}");
                    int getAlbumId = Convert.ToInt32(album.GetAlbumId());
                    GtmAlbumLog gtmAlbumLogNew = new GtmAlbumLog();
                    gtmAlbumLogNew.AlbumId = getAlbumId;
                    gtmAlbumLogNew.AlbumName = album.Name;
                    gtmAlbumLogNew.GtmAlbumLogId = getAlbumId;
                    gtmAlbumLogNew.AlbumUri = album.Link;
                    gtmAlbumLogNew.Password = password;
                    if (addVimeoModel.groupId != null) gtmAlbumLogNew.GroupId = addVimeoModel.groupId;
                    ISpecialistGtmService.AddAlbumLog(gtmAlbumLogNew);
                }
            }
            else
            {
                //if (gtmAlbumLog.Password == null)
                //{
                //    password = CreatePassword(12);
                //    EditAlbumParameters editAlbumParameters = new EditAlbumParameters
                //    {
                //        Password = password
                //    };
                //    ISpecialistGtmService.UpdatePasswordAlbumLog(password, gtmAlbumLog);
                //    logger.Info($"Обновили пароль у альбома в бд");
                //    await client.UpdateAlbumAsync(UserId.Me, gtmAlbumLog.AlbumId, editAlbumParameters);
                //    logger.Info($"Обновили пароль у альбома в вимео");
                //}
                album = await client.GetAlbumAsync(UserId.Me, gtmAlbumLog.AlbumId);              
                password = gtmAlbumLog.Password;
                logger.Info($"Получен альбом { album.Name}");
                isGetAlbum = true;
            }
            return new AlbumView
            {
                album = album,
                Password = password,
                isGetAlbum = isGetAlbum
            };

        }
        public async Task UpdateVideo(VimeoClient client, long clipId, string password, string nameVideo)
        {
            await client.UpdateVideoMetadataAsync(clipId, new VideoUpdateMetadata
            {
                Name = nameVideo,
                Privacy = VimeoDotNet.Enums.VideoPrivacyEnum.Password,
                Password = password,
            });
        }
       
        private async Task<bool> AddVideoToAlbum(VimeoClient client, long albumId, long clipId)
        {           
          //  long albumId = newAlbum.GetAlbumId().Value;
            bool result = await client.AddToAlbumAsync(UserId.Me, albumId, clipId);
            return result;
        }
        public VimeoClient ChangeClientSpaceQuota(VimeoClient client,string path)
        {
            try
            {
                long size = new System.IO.FileInfo(path).Length;
                long spaceQuota = GetSpaceQuota(client);
                if (spaceQuota < size)
                {
                    VimeoClient clientManager = GetClient(VimeoConstants.ApiAuthManager.AccessTokenManager);
                    spaceQuota = GetSpaceQuota(clientManager);
                    if (spaceQuota < size)
                    {
                        logger.Warn("Не могу записать в вимео так как мало свободного места в вимео");
                        IMailService.SendErrorDeveloper("Ошибка в вимео", "Не могу записать в вимео так как мало свободного места в вимео");
                        return null;
                    }
                    return clientManager;
                }
            }
            catch(Exception ex)
            {
                logger.Warn(ex);
            }
            return client;
        }
        public long GetSpaceQuota(VimeoClient client)
        {
            Task<User> result = client.GetAccountInformationAsync();
            result.Wait();
            if (result.IsCompleted)
            {
                User User = result.Result;
                Space spaceQouta = User?.UploadQuota?.Space;
                if (spaceQouta != null)
                {
                    return spaceQouta.Free;
                }
            }
            return 0;

        }
        public string GetGrNumber(string subject)
        {
            string numberGr = "";
            int index = subject.IndexOf("Gr");
            if (index != -1)
            {
                subject = subject.Remove(0, index + 2);
                int indexRem = subject.IndexOf(" ");
                if (indexRem != 0 && indexRem != -1)
                {
                    numberGr = subject.Remove(indexRem);
                }
                else
                {
                    numberGr = subject.ToString();
                }
            }
            return numberGr;
        }
        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
