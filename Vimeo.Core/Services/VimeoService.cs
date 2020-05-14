using System;
using System.Collections.Generic;
using System.Text;
using Vimeo.Core.Helpers;
using Vimeo.Core.Services.Inerface;
using VimeoDotNet;
using VimeoDotNet.Models;
using VimeoDotNet.Net;
using VimeoDotNet.Parameters;

namespace Vimeo.Core.Services
{
    public class VimeoService: IVimeoSevice
    {

        public VimeoClient GetClient(string accessToken)
        {
           return new VimeoClient(accessToken);
           // return new VimeoClient("3e8d4a50fc996396910bd889d761acedf1b39205","9nFyllosEQ8Igz9dxTbRRzDh/0JPHVrZX9A7Ue3FdnzIRizgfnSSgz/hu3r6slC47bGuvoZjOkTduewXt52yU2FtsxWLSXTtBGj9yfGTHsyQ4UiiHVHowdHLeESlCaZl");
        }
        public async void AddToAlbumVideos(string path, VimeoClient client)
        {
            //Add Album           
            //  create a new album...
            const string originalName = "Тестовый альбом";
            const string originalDesc =
                "This album was created via an automated test, and should be deleted momentarily...";
            //Add Video
            long length;
            IUploadRequest completedRequest;
            // var tempFilePath = Path.GetTempFileName() + ".mp4";
            using (var file = new BinaryContent(path))
            {
                completedRequest = await client.UploadEntireFileAsync(file);
            }
            Console.WriteLine("Файл закачен");
            var newAlbum = await client.CreateAlbumAsync(UserId.Me, new EditAlbumParameters
            {
                Name = originalName,
                Description = originalDesc,
                Sort = EditAlbumSortOption.Newest,
                Privacy = EditAlbumPrivacyOption.Password,
                Password = "test",
            });

            await client.UpdateVideoMetadataAsync(completedRequest.ClipId.Value, new VideoUpdateMetadata
            {
                Name = "TestVideo",
                Privacy = VimeoDotNet.Enums.VideoPrivacyEnum.Password,
                Password = "test",
            });

            // var videos = await client.GetVideosAsync(UserId.Me);
            //AddVideoToAlbum
            long albumId = newAlbum.GetAlbumId().Value;
            var r = await client.AddToAlbumAsync(UserId.Me, albumId, completedRequest.ClipId.Value);
            //  r.ShouldNotBeNull();

            string url = newAlbum.Uri;
            Console.WriteLine($"Файл закачен на вимео {DateTime.Now.ToLongTimeString()}");

        }
    }
}
