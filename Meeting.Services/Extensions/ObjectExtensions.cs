using System;
using System.Collections.Generic;
using System.Text;

namespace Tresmodo.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static SafeConverter Convert(this object obj)
        {
            return new SafeConverter(obj);
        }
    }
}
