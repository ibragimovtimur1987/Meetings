using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Extension
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
            if (convertedObject != null && convertedObject != DBNull.Value)
                retStr = convertedObject.ToString();
            return retStr;
        }

        public int GetInt()
        {
            int i;
            if (convertedObject == null || convertedObject == DBNull.Value || !int.TryParse(convertedObject.ToString(), out i))
                return 0;
            return i;
        }

        public double GetDouble(double defaultValue = 0)
        {
            double i;
            if (convertedObject == null || convertedObject == DBNull.Value || !double.TryParse(convertedObject.ToString().Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out i))
                return defaultValue;
            return i;
        }

        public float GetFloat()
        {
            float i;
            if (convertedObject == null || convertedObject == DBNull.Value || !float.TryParse(convertedObject.ToString().Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out i))
                return 0;
            return i;
        }

        public decimal GetDecimal(decimal defaultValue = 0)
        {
            decimal i;
            if (convertedObject == null || convertedObject == DBNull.Value || !decimal.TryParse(convertedObject.ToString().Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out i))
                return defaultValue;
            return i;
        }

        public Guid GetGuid()
        {
            Guid i = Guid.Empty;
            if (convertedObject != null && convertedObject != DBNull.Value)
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
