using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Extension
{
    public static class ObjectExtensions
    {
        public static SafeConverter Convert(this object obj)
        {
            return new SafeConverter(obj);
        }
    }
}
