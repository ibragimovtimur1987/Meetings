using SpecialistMeeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services.Interface
{
    public interface ISpecialistGtmService
    {
        GtmAlbumLog UpdatePasswordAlbumLog(string password, GtmAlbumLog gtmAlbumLog);
        IQueryable<GtmUploadVideoLog> GtmUploadVideoLog();

        GtmUploadVideoLog AddUploadVideoLog(GtmUploadVideoLog gtmUploadVideoLog);

        GtmAlbumLog AddAlbumLog(GtmAlbumLog gtmAlbumLog);

        IQueryable<GtmAlbumLog> GtmAlbumLog();

        GtmUploadVideoLog UpdateUploadVideoLog(string meetingId, string sessionId, bool IsComplete, decimal webinarLicenseId);
    }
}
