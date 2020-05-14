using Meeting.Entities;
using Meeting.Entities.Models;
using Meeting.Services.Interface;
using Newtonsoft.Json;
using NLog;
using SpecialistRepl;
using SpecialistWeb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Tresmodo.Core.Extensions;


namespace Meeting.Services.Services
{
    public class GetGroupService: IGetGroupService
    {
        public const string EmailDefault = "tibragimov@specialist.ru";
        public SPECREPL_replicatingEntities SPECREPL_replicatingEntities { get; set; } = new SPECREPL_replicatingEntities();

        public SpecialistWebEntities contextWeb { get; set; } = new SpecialistWebEntities();

        private SpecialistGtmService specialistGtmService = new SpecialistGtmService();

        public bool IsTest { get; set; }
        public Dictionary<decimal, DateTime> dateLastStartedDict { get; private set; }= new Dictionary<decimal, DateTime>();

        public ListGroups GetListGroups()
        {
            List<string> excludeColor = new List<string> { "Г", /*"З",*/ "Б" };
            int countDay = 2;
            if (IsTest) countDay = 2;
            DateTime dtFrom = DateTime.Now.Date.AddDays(0);
            DateTime dtTo = DateTime.Now.Date.AddDays(countDay);
            //IEnumerable<tGroup> groups = context.tGroups.Include("tLectures").Where(x => x.DateBeg >= dtFrom && x.DateBeg <= dtTo);
            IEnumerable<tGroups> groups = SPECREPL_replicatingEntities.tLectures.Include("tGroups").Where(x => x.LectureDateBeg >= dtFrom && x.LectureDateBeg <= dtTo).Select(x => x.tGroups);
            //int cntLect = item.tLectures.Count(x => x.LectureDateBeg <= dt.Date);      



            groups = groups.Where(x => x.DateEnd >= DateTime.Now.Date);
            groups = groups.Where(x => !excludeColor.Contains(x.Color_TC));


        //    DateTime dateStartRelease = "16.01.2020".Convert().GetDateTime();

           // groups = groups.Where(x => TestGroup.Contains(x.Group_ID) || TestGroup.Contains(x.MegaGroup_ID ?? -1) ); // ДЛя  теста потом убрать
            var listGroups = groups.Distinct().ToList();
            List<decimal> includeTaskGroupsId = ListGroupsForUpdate(listGroups);
            System.Console.WriteLine(DateTime.Now.ToString("HH:mm | ") + string.Join(";", includeTaskGroupsId.OrderBy(x => x).Select(x => x.ToString()).ToList()));


            listGroups = listGroups.Where(x => includeTaskGroupsId.Contains(x.Group_ID)).ToList();
            //groups = groups.Take(1).ToList(); //test
            //"Создаем команды Тимс для всех групп, или есть ограничения? (по цвету, по дате и т.д.)" – необходимо исключить голубые, белые и зелёные группы. 
            //"Голубая группа с датами" = снятая группа. "Белая" —> группа проводится у партнёров. "Зелёная" —> служебные группы для оформление оплаты за сертификаты онлайн тестирования, и прочие товары и дополнительные услуги.
            //Для группы открытого обучения нужна единая общая команда(Team/ собрание / вебинар) для слушателей всех подчинённых групп одной мегагруппы.
            //Команды Тимс не нужны для экзаменов, и для служебных групп.
            //группы ОО
            List<tGroups> megaGroups = listGroups.Where(x => x.LectureType_TC == "О").ToList();
            //группы ОЗ
            List<tGroups> singleGroups = listGroups.Where(x => x.MegaGroup_ID == null && x.LectureType_TC != "О" && x.IsIntraExtramural).ToList();
            //Группы, которые потом исключим из других выборок
            List<decimal> singleGroupsIdsExclude = singleGroups.Select(x=> x.Group_ID).ToList();
            List<decimal> megaGroupsIdsExclude = megaGroups.Select(x => x.Group_ID).ToList();
            //2019.06.22 Только ОЗ для групп одиночек
            singleGroups = singleGroups.Where(x => x.Course_TC != "ДПКОНС").ToList();
           
           // List<decimal> includeStandartGroup = specialistTeamsDbService.GetIncludeStandartGroups();
           // List<string> listTeacher_TC = new List<string> { "ПЕАР", "ОЮС", "КМН", "ДДЮ", "МЕЙС", "СФА" };
            List<string> listTeacher_TC_Promotional = new List<string> { "ГОЛ" };
            //Исключим некоторые группы
            List<decimal> excludeStandartGroups = new List<decimal> { 341149, 266611, 273613 };

            //Все группы, кроме ОО и ОЗ
            List<tGroups> standartGroups = listGroups.Where(x => !singleGroupsIdsExclude.Contains(x.Group_ID) && !megaGroupsIdsExclude.Contains(x.Group_ID)).ToList();
            standartGroups = standartGroups.Where(x => x.LectureType_TC != "С").ToList();//Исключаем спец группы
            standartGroups = standartGroups.Where(x => !excludeStandartGroups.Contains(x.Group_ID) && x.Course_TC != "ДПКОНС").ToList();
          
            List<tGroups> webinarGroups = standartGroups.Where(x => x.WebinarExists).ToList();
            //webinarGroups = webinarGroups.Where(x => !excludeStandartGroups.Contains(x.Group_ID)).ToList();//Исключаем группы
            //List<decimal> standartGroupsIncludeMember = new List<decimal> { 317629 };
            
            //Выборка групп из бд
            List<decimal> standartPromotionalGroupsIds = contextWeb.uspGetSeminarGroupsForOneDay().Select(x => x.Value).Distinct().ToList();
          //  List<decimal> standartWebinarGroupsIds = contextWeb.uspGetWebinarGroups().Select(x => x.Value).Distinct().ToList();
          //  List<decimal> standartIntramuralGroupsIds = contextWeb.uspGetIntramuralGroups().Select(x => x.Value).Distinct().ToList();

            //List<tGroup> standartGroupsMember = webinarGroups.Where(x => standartGroupsIncludeMember.Contains(x.Group_ID)).ToList();
            //рекламные группы
            List<tGroups> standartPromotionalGroups = webinarGroups.Where(x => standartPromotionalGroupsIds.Contains(x.Group_ID) && !listTeacher_TC_Promotional.Contains(x.Teacher_TC)).ToList();
            List<decimal> idsStandartPromotionalGroups = standartPromotionalGroups.Select(x => x.Group_ID).ToList();
            //группы по вебинару
            List<tGroups> standartGroupsWebinar = webinarGroups.Where(x =>!x.IsRecordRequired).ToList();           
            //очные группы
           // List<tGroup> standartGroupsIntramural = standartGroups.Where(x => (standartIntramuralGroupsIds.Contains(x.Group_ID) ) && !x.WebinarExists && !x.IsRecordRequired).ToList();

            //(x.Group_ID == 269044 || x.Group_ID == 319996 || x.Group_ID == 259338 || x.Group_ID == 259337 || x.Group_ID == 312546 || x.Group_ID == 264154 || x.Group_ID == 306126)
            //2019.05.28 отключили ОЗ, оставили только тех которые указали в БД
            //singleGroups = singleGroups.Where(x => ( TestGroup.Contains(x.Group_ID)) && x.DateBeg > dateStartOO && x.Course_TC != "ДПКОНС").ToList();
            //исключаем группы у которых нет ссылки на гоутумитинг
           // standartGroupsWebinar = standartGroupsWebinar.Where(x => x.tLectures.Any(y => y.WebinarURL == "" || y.WebinarURL == null)).ToList();
            //Исключаем рекламные семинары из очных и вебинаров
            standartGroupsWebinar = standartGroupsWebinar.Where(x => !idsStandartPromotionalGroups.Contains(x.Group_ID)).ToList();
           // standartGroupsIntramural = standartGroupsIntramural.Where(x => !idsStandartPromotionalGroups.Contains(x.Group_ID)).ToList();
            //Исключаем группы, которые сейчас идут
            standartGroupsWebinar = standartGroupsWebinar.Where(x => x.DateBeg > new DateTime(2020,1,18)).ToList();         
          //  standartGroupsIntramural = standartGroupsIntramural.Where(x => x.DateBeg > DateTime.Now.Date).ToList();


            //var test = standartGroupsWebinar.FirstOrDefault(x => x.Group_ID == 275063);
            //для теста
            //var rstandartGroupsWebinart = standartGroupsWebinar.Select(x => new { x.Group_ID,x.DateBeg }).OrderBy(x=>x.DateBeg).ToList();
          
            //var rstandartGroupsIntramural = standartGroupsIntramural.Select(x => new { x.Group_ID, x.DateBeg }).OrderBy(x => x.DateBeg).ToList();
            //var singleGroupsli = singleGroups.Select(x => x.Group_ID).ToList();
            //var megaGroupsli = megaGroups.Select(x => x.Group_ID).ToList();
            //
            //Необходимо исключить группы из тимса
            singleGroups = singleGroups.Where(x => x.Group_ID != 320305).ToList();
            megaGroups = megaGroups.Where(x => x.Group_ID != 320305).ToList();
            return new ListGroups
            {
               // megaGroups = new GroupsSet{  Groups = megaGroups, GroupsType = Constants.TypeGroups.megaGroups },
              //  singleGroups = new GroupsSet {  Groups = singleGroups, GroupsType = Constants.TypeGroups.singleGroups },
                standartGroupsWebinar = new GroupsSet {  Groups = standartGroupsWebinar, GroupsType = Constants.TypeGroups.standartGroupsWebinar },
              //  standartPromotionalGroups = new GroupsSet {  Groups = standartPromotionalGroups, GroupsType = Constants.TypeGroups.standartPromotionalGroups },
               // standartGroupsIntramural = new GroupsSet {  Groups = new List<tGroups>(), GroupsType = Constants.TypeGroups.standartGroupsIntramural },
            };
        }
        //Список групп для обновления
        public List<decimal> ListGroupsForUpdate(List<tGroups> groups)
        {

            var cntConstLect = 3;
            DateTime dt = DateTime.Now;
            TimeSpan dtCurrent = dt.TimeOfDay;
            List<decimal> idsGroupsUpdate = new List<decimal>();
            foreach (var item in groups)
            {
                int cntLect = item.tLectures.Count(x => x.LectureDateBeg <= dt.Date);
                cntConstLect = item.DayShift_TC == "В" ? 5 : 3;//Количество учитываем лекций, для вечерних групп - 5
                //Если  пройденно менее трех лекций то робот работает
                if (cntLect <= cntConstLect)
                {
                    int intervalMinute = 20;
                    //  DateTime currentGroupDateTime = item.DateBeg.Convert().GetDateTime().Date + item.TimeBeg.Convert().GetDateTime().TimeOfDay;
                    TimeSpan dtStart = item.TimeBeg.Convert().GetDateTime().AddHours(-2).TimeOfDay;
                    TimeSpan dtEnd = item.TimeBeg.Convert().GetDateTime().AddHours(1).TimeOfDay;
                    
                    if (dtStart <= dtCurrent && dtCurrent <= dtEnd) intervalMinute = 10;
                    if (dateLastStartedDict.ContainsKey(item.Group_ID))
                    {
                        if (dateLastStartedDict[item.Group_ID].AddMinutes(intervalMinute) <= dt)
                        {
                            idsGroupsUpdate.Add(item.Group_ID);
                            dateLastStartedDict[item.Group_ID] = dt;
                        }
                    }
                    else
                    {
                        idsGroupsUpdate.Add(item.Group_ID);
                        dateLastStartedDict.Add(item.Group_ID, dt);
                    }
                }

            }


            //TODO: чистить старые группы
            var keysToRemove = dateLastStartedDict.Keys.Except(groups.Select(x => x.Group_ID)).ToList();

            foreach (var key in keysToRemove)
                dateLastStartedDict.Remove(key);


            return idsGroupsUpdate;
        }
    }
}
