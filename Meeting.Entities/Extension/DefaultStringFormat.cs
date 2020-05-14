using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Entities.Extension
{
    public static class DefaultStringFormat
    {
        public static string MoneyString(this decimal d)
        {
            return d.ToString("n0");
        }

        /*   public static string WithoutZero(this decimal d)
           {
               return d.ToString("n2");
           }*/

        public static string MoneyString(this decimal? d)
        {
            if (!d.HasValue)
                return string.Empty;
            return d.Value.MoneyString();
        }
        public static string DefaultString(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }
        public static string OnlyDM(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM");
        }
        public static string OnlyMY(this DateTime dateTime)
        {
            return dateTime.ToString("MM.yyyy");
        }
        public static string DefaultWithTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public static string ShortString(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM");
        }

        public static string DateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public static string DefaultString(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return string.Empty;
            return dateTime.Value.DefaultString();
        }

        public static string ShortString(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return string.Empty;
            return dateTime.Value.ShortString();
        }
    }
}
