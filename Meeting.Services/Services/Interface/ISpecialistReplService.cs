using SpecialistRepl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services.Interface
{
    public interface ISpecialistReplService
    {
        IQueryable<tLectures> GettLectures();
        IQueryable<tWebinarLicenses> GettWebinarLicenses();
        string GetNameAlbum(string grNumber);
        string GetNameRoom(decimal grNumberInt);
        IQueryable<tGroups> GettGroups();
    }
}
