using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Entities
{
    public static class Constants
    {
        public static class TypeTranslateName
        {
            public const string complex = "complex";
            public const string course = "course";
            public const string dayShift_TC = "DayShift_TC";          
        }
        public static class TypeGroups
        {
            public const string megaGroups = "megaGroups";
            public const string singleGroups = "singleGroups";
            //public const string standartGroupsGuests = "standartGroupsGuests";
            public const string standartGroupsWebinar = "standartGroupsWebinar";
            public const string standartPromotionalGroups = "standartPromotionalGroups";
            public const string standartGroupsIntramural = "standartGroupsIntramural";
        }
        public static class IdNotFoundGroups
        {          
            public static int NotFoundGroupsWebMan = 6862467;
            public static int NotFoundGroupsMan = 6862566;
        }
        public static class VimeoConstants
        {
            public static class ApiAuthManager
            {
                public static string AccessTokenManager = "test";// webinar@specialist.ru
                public static string AccessTokenWebinarManager = "test"; //webinarmanager

            }
        }
        public static class AppConfigConst
        {
            public static string SkipWebinarLicenses = @"SkipWebinarLicenses";
            //  public static string Path = @"D:\temp";
            public static string TakeWebinarLicenses = "TakeWebinarLicenses";
        }
        public static class GtmConstants
        {
            public static class ApiAuthManager
            {
                public static string UserName = "test";
                public static string UserPassword = "test";
                public static string ConsumerKey = "test";
                public static string ConsumerSecret = "test";
            }

            public static class ApiAuthCorporate22
            {
                public static string UserName = "test";
                public static string UserPassword = "test";
                public static string ConsumerKey = "test";
                public static string ConsumerSecret = "test";

            }
            public static class ApiAuthwebinar6
            {
                public static string UserName = "test";
                public static string UserPassword = "test";
                public static string ConsumerKey = "test";
                public static string ConsumerSecret = "test";

            }
            public static class FileConstants
            {
                public static string GtmFolderPath = @"GtmFolderPath";
              //  public static string Path = @"D:\temp";
                public static string Expansion = ".mp4";
            }
        }
    }
}
