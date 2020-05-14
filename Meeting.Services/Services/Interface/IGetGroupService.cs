using Meeting.Entities.Models;
using SpecialistRepl;
using SpecialistWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Meeting.Services.Interface
{
    public interface IGetGroupService
    {
        ListGroups GetListGroups();

        SPECREPL_replicatingEntities SPECREPL_replicatingEntities { get; set; }

        SpecialistWebEntities contextWeb { get; set; }

        bool IsTest { get; set; }

    }
}
