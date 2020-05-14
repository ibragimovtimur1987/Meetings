using Meeting.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meeting.Services.Services.Interface
{
    public interface IMainService
    {
        [Dependency]
        IGotomeetingService GotomeetingService { get; set; }
        [Dependency]
        IFileService FileService { get; set; }
        [Dependency]
        IVimeoSevice VimeoSevice { get; set; }
        [Dependency]
        ISpecialistGtmService SpecialistGtmService { get; set; }
        [Dependency]
        ISpecialistReplService SpecialistReplService { get; set; }
        [Dependency]
        IPioneerService PioneerService { get; set; }
        [Dependency]
        IMailService MailService { get; set; }
        [Dependency]
        IGetGroupService GetGroupService { get; set; }
        void CreateMeetings();
        void TransferVideo(int skipWebinarLicenses, int takeWebinarLicenses);
    }

}
