using Meeting.Entities.Models;
using Meeting.Services.Services.Interface;
using SimpleUtils.Common.Extensions;
using SpecialistWeb;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meeting.Services.Services
{
    public class SpecialistWebService: ISpecialistWebService
    {
        public SpecialistWebService()
        {

        }
        SpecialistWebEntities SpecialistWebEntitiesEntities { get; set; } = new SpecialistWebEntities();
        [Dependency]
        public ISpecialistGtmService SpecialistGtmService { get; set; }
        [Dependency]
        public IVimeoGetVideosService VimeoGetVideosService { get; set; }
        public void RefreshAlbum(long albumId)
        {
            AlbumVideos album = this.GettAlbumVideos().FirstOrDefault(x => x.AlbumId == albumId);
            if (album==null)
            {
                this.AddVimeoAlbum(albumId);
            }
            else
            {
                this.UpdateVimeoAlbum(album);
            }
        }
        private void UpdateVimeoAlbum(AlbumVideos albumVideos)
        {
            var videos = VimeoGetVideosService.GetVideos(albumVideos.AlbumId.ToString());
            albumVideos.Videos = videos.Select(x => x.GetId()).JoinWith(",");
            albumVideos.Titles = videos.Select(x => x.GetName()).JoinWith(",");
            SpecialistWebEntitiesEntities.SaveChanges();
        }
        private void AddVimeoAlbum(long albumId)
        {

            var videos = VimeoGetVideosService.GetVideos(albumId.ToString());
       

            //var videos = SpecialistGtmService.GtmUploadVideoLog().Where(x => x.GtmAlbumLogId == albumVideos.AlbumId).ToList();
            //var titles = videos.Select(x => x.GtmHistoricalMeetingSubject).ToList();
            //var videosSubject = videos.Select(x => x.GtmHistoricalMeetingSubject).ToList();
            // var videoIds = VimeoService.GetVideos(albumId.ToString());
            var album = new AlbumVideos
            {
                AlbumId = albumId,
                Videos = videos.Select(x => x.GetId()).JoinWith(","),
                Titles = videos.Select(x => x.GetName()).JoinWith(",")
            };
            SpecialistWebEntitiesEntities.AlbumVideos.Add(album);
            SpecialistWebEntitiesEntities.SaveChanges();
        }
        public IQueryable<AlbumVideos> GettAlbumVideos()
        {
            return SpecialistWebEntitiesEntities.AlbumVideos;
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    SpecialistWebEntitiesEntities.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
