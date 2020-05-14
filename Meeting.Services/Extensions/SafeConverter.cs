using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Tresmodo.Core.Extensions
{
    public class SafeConverter
    {
        private object convertedObject;

        public SafeConverter(object obj)
        {
            convertedObject = obj;
        }

        public string GetString()
        {
            string retStr = string.Empty;
            if (convertedObject != null)
                retStr = convertedObject.ToString();
            return retStr;
        }

        public int GetInt()
        {
            int i;
            if (convertedObject == null || !int.TryParse(convertedObject.ToString(), out i))
                return 0;
            return i;
        }


        public long GetLong()
        {
            long i;
            if (convertedObject == null || !long.TryParse(convertedObject.ToString(), out i))
                return 0;
            return i;
        }

        public double GetDouble()
        {
            double i;
            if (convertedObject == null || !double.TryParse(convertedObject.ToString().Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out i))
                return 0;
            return i;
        }

        public float GetFloat()
        {
            float i;
            if (convertedObject == null || !float.TryParse(convertedObject.ToString().Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out i))
                return 0;
            return i;
        }

        public decimal GetDecimal()
        {
            decimal i;
            if (convertedObject == null || !decimal.TryParse(convertedObject.ToString().Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out i))
                return 0;
            return i;
        }

        public Guid GetGuid()
        {
            Guid i = Guid.Empty;
            if (convertedObject != null)
                Guid.TryParse(convertedObject.ToString(), out i);
            return i;
        }

        public DateTime GetDateTime(string format = "")
        {
            DateTime i = DateTime.MinValue;
            if (convertedObject != null)
            {
                if (format != "" && DateTime.TryParseExact(convertedObject.ToString(), "dd'.'MM'.'yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out i))
                {
                    return i;
                }


                DateTime.TryParse(convertedObject.ToString(), out i);
            }
            return i;
        }

        public DateTime GetDateTimeFromTimeSpan()
        {
            DateTime i = DateTime.MinValue;
            if (convertedObject != null)
            {
                DateTime dt = new DateTime(2012, 01, 01);
                TimeSpan ts = TimeSpan.Parse(convertedObject.ToString());
                i = dt + ts;
               
            }
            return i;
        }

        public bool GetBoolean()
        {
            if (convertedObject == DBNull.Value || convertedObject == null) return false;
            if (convertedObject is string)
            {
                if (string.IsNullOrEmpty((string)convertedObject)) return false;
                if (((string)convertedObject).ToLower() == "true") return true;
                if (((string)convertedObject).ToLower() == "false") return false;
                if (((string)convertedObject).ToLower() == "1") return true;
                if (((string)convertedObject).ToLower() == "0") return false;
            }
            return Convert.ToBoolean(convertedObject);
        }

        public T GetEnum<T>()
        {
            T result = default(T);
            if (convertedObject != null)
            {
                var type = typeof(T);
                if (!type.IsEnum) throw new InvalidOperationException();
                foreach (var field in type.GetFields())
                {
                    var attribute = Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attribute != null)
                    {
                        if (attribute.Description == convertedObject.ToString())
                            result = (T)field.GetValue(null);
                    }
                    else
                    {
                        if (field.Name == convertedObject.ToString())
                            result = (T)field.GetValue(null);
                    }
                }
            }

            return result;
        }
    }
}
