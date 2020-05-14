using Meeting.Entities.Models;
using SpecialistRepl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services.Interface
{
    public interface IPioneerService
    {
        void UpdateGroup(decimal group_ID, GroupRec groupRec);
    }
}
