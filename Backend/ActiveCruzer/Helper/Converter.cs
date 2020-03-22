using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Helper
{
    public class Converter
    {

        public static long ConvertToUnixTimestamp(DateTime Time)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, Time.Kind);
            var unixTimestamp = System.Convert.ToInt64((Time - date).TotalSeconds);
            return unixTimestamp;
        }
    }
}
