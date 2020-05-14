using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Entities.Models
{
    public class ListGroups
    {
        /// <summary>
        /// ОО
        /// </summary>
        public GroupsSet megaGroups { get; set; }
        /// <summary>
        /// ОЗ
        /// </summary>
        public GroupsSet singleGroups { get; set; }
        /// <summary>
        /// Группы по вебинару, слушатели подключаются как гости
        /// </summary>
        //public GroupsSet standartGroupsGuests { get; set; }
        /// <summary>
        ///  Группы по вебинару, слушатели подключаются как участники
        /// </summary>
        public GroupsSet standartGroupsWebinar { get; set; }
        /// <summary>
        /// Очные группы
        /// </summary>
        public GroupsSet standartGroupsIntramural { get; set; }
        /// <summary>
        /// Рекламные семинары
        /// </summary>
        public GroupsSet standartPromotionalGroups { get; set; }
    }
}
