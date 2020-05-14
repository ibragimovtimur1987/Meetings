
using Meeting.Entities.Extension;
using Meeting.Services.Extensions;
using Meeting.Services.Services.Interface;
using SpecialistRepl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meeting.Entities.Constants;

namespace Meeting.Services.Services
{
    public class SpecialistReplService : ISpecialistReplService, IDisposable
    {
        SPECREPL_replicatingEntities SPECREPL_replicatingEntities { get; set; }
        public SpecialistReplService()
        {
            SPECREPL_replicatingEntities = new SPECREPL_replicatingEntities();
        }
        public IQueryable<tLectures> GettLectures()
        {
            return SPECREPL_replicatingEntities.tLectures;
        }
        public IQueryable<tWebinarLicenses> GettWebinarLicenses()
        {
          return SPECREPL_replicatingEntities.tWebinarLicenses;
        }
        public IQueryable<tGroups> GettGroups()
        {
            return SPECREPL_replicatingEntities.tGroups;
        }
        public string GetNameAlbum(string grNumber)
        {
            string grName = "";
            decimal grNumberInt = Convert.ToDecimal(grNumber);

            tGroups gr = SPECREPL_replicatingEntities.tGroups.FirstOrDefault(x => x.Group_ID == grNumberInt);
            if(gr!=null)
            {
                string dateBeg = gr.DateBeg.Value != null ? gr.DateBeg.Value.DefaultString() : "";
                string dateEnd = gr.DateEnd.Value != null ? gr.DateEnd.Value.DefaultString() : "";
                string courseName = "";
                tCourses course = SPECREPL_replicatingEntities.tCourses.FirstOrDefault(x => x.Course_TC == gr.Course_TC);
                if(course!=null)
                {
                    courseName = course.Name;
                }
                grName = $"{courseName} {gr.Teacher_TC} {dateBeg} - {dateEnd}";
            }
            return grName;
        }
        public string GetNameRoom(decimal grNumberInt)
        {
            string grName = "";
           // decimal grNumberInt = Convert.ToDecimal(grNumber);

            tGroups gr = SPECREPL_replicatingEntities.tGroups.FirstOrDefault(x => x.Group_ID == grNumberInt);
            if (gr != null)
            {
                string dateBeg = gr.DateBeg.Value != null ? gr.DateBeg.Value.DefaultString() : "";
                string dateEnd = gr.DateEnd.Value != null ? gr.DateEnd.Value.DefaultString() : "";
                string courseNameTranslate = "";
                tCourses course = SPECREPL_replicatingEntities.tCourses.FirstOrDefault(x => x.Course_TC == gr.Course_TC);
                string complexNameTranslate = "";
                string dayShiftTranslate = "";
                tComplexes complex = SPECREPL_replicatingEntities.tComplexes.FirstOrDefault(x => x.Complex_TC == gr.Complex_TC);
                Translit translit = new Translit();
                if (complex!=null)
                {
                    string complexName = complex.Complex_TC;
                    complexNameTranslate = translit.TranslitName(complexName, TypeTranslateName.complex);
                }
                if (course != null)
                {
                    string courseName = course.Name;
                    courseNameTranslate = translit.TranslitName(courseName, TypeTranslateName.course);
                }
                if (gr.DayShift_TC != null)
                {
                    string dayShift_TC = gr.DayShift_TC;
                    dayShiftTranslate = translit.TranslitName(dayShift_TC, TypeTranslateName.dayShift_TC);
                }

                grName = $"{courseNameTranslate} {dateBeg} - {dateEnd} Gr{gr.Group_ID} {dayShiftTranslate} {complexNameTranslate}";
            }
            return grName;
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    SPECREPL_replicatingEntities.Dispose();
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
