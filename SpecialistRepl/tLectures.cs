//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpecialistRepl
{
    using System;
    using System.Collections.Generic;
    
    public partial class tLectures
    {
        public decimal Lecture_ID { get; set; }
        public System.DateTime LectureDateBeg { get; set; }
        public System.DateTime LectureDateEnd { get; set; }
        public short Breaks { get; set; }
        public string Teacher_TC { get; set; }
        public string ClassRoom_TC { get; set; }
        public string Notes { get; set; }
        public System.DateTime InputDate { get; set; }
        public string Employee_TC { get; set; }
        public System.DateTime LastChangeDate { get; set; }
        public string LastChanger_TC { get; set; }
        public Nullable<System.DateTime> TrainerComingTime { get; set; }
        public bool IsQualityAssurance { get; set; }
        public Nullable<decimal> WebinarLicense_ID { get; set; }
        public string WebinarURL { get; set; }
        public Nullable<decimal> WebCam_ID { get; set; }
    
        public virtual tGroups tGroups { get; set; }
    }
}
