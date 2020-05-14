using Meeting.Services.Services.Interface;
using SpecialistMeeting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services
{
    public class SpecialistGtmService : ISpecialistGtmService, IDisposable
    {
        SpecialistMeetingEntities specialistMeetingEntities { get; set; }
        public SpecialistGtmService()
        {
            specialistMeetingEntities = new SpecialistMeetingEntities();
        }
        public IQueryable<GtmUploadVideoLog> GtmUploadVideoLog()
        {       
          return specialistMeetingEntities.GtmUploadVideoLog.AsQueryable();       
        }
        public GtmUploadVideoLog UpdateUploadVideoLog(string meetingId, string sessionId, bool IsComplete, decimal webinarLicenseId)
        {
            GtmUploadVideoLog gtmUploadVideoLog = GtmUploadVideoLog().FirstOrDefault(x => x.MeetingId == meetingId && x.SessionId == sessionId && x.WebinarLicenseId == webinarLicenseId);
            if (gtmUploadVideoLog!=null)
            {
                gtmUploadVideoLog.IsComplete = IsComplete;
                specialistMeetingEntities.SaveChanges();
            }
            // specialistMeetingEntities.GtmUploadVideoLog.Add(gtmUploadVideoLog);        
            return gtmUploadVideoLog;
        }
        public GtmUploadVideoLog AddUploadVideoLog(GtmUploadVideoLog gtmUploadVideoLog)
        {
            specialistMeetingEntities.GtmUploadVideoLog.Add(gtmUploadVideoLog);
            specialistMeetingEntities.SaveChanges();
                return gtmUploadVideoLog;         
        }
        public IQueryable<GtmAlbumLog> GtmAlbumLog()
        {     
             return specialistMeetingEntities.GtmAlbumLog.AsQueryable();        
        }
        public GtmAlbumLog UpdatePasswordAlbumLog(string password, GtmAlbumLog gtmAlbumLog)
        {

            gtmAlbumLog.Password = password;
            specialistMeetingEntities.SaveChanges();
            // specialistMeetingEntities.SaveChanges();
            return gtmAlbumLog;
        }
        public GtmAlbumLog AddAlbumLog(GtmAlbumLog gtmAlbumLog)
        {
            specialistMeetingEntities.GtmAlbumLog.Add(gtmAlbumLog);
            specialistMeetingEntities.SaveChanges();
            // specialistMeetingEntities.SaveChanges();
            return gtmAlbumLog;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                specialistMeetingEntities.Dispose();
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
