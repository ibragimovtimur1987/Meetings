using Meeting.Entities.Models;
using Pioneer;
using SpecialistRepl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services.Interface
{
    public class PioneerService: IPioneerService, IDisposable
    {
        SPECIALISTEntities pioneer { get; set; }
        public PioneerService()
        {
            pioneer = new SPECIALISTEntities();
        }
        //IQueryable<tWebinarLicenses> GettWebinarLicenses();
        public void UpdateGroup(decimal group_ID, GroupRec groupRec)
        {
                var group = pioneer.tGroups.FirstOrDefault(x => x.Group_ID == group_ID);
                if (group != null)
                {
                    group.WbnRecPwd = groupRec.WbnRecPwd;
                    group.WebinarRecordURL = groupRec.WebinarRecordURL;
                    pioneer.SaveChanges();
                }
         
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    pioneer.Dispose();
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
