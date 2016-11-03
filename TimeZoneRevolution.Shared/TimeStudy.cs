using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeZoneRevolution.Shared
{
    public class TimeStudy
    {
        public static string getTimeZones()
        {
            StringBuilder builder = new StringBuilder();

            var localTzi = TimeZoneInfo.Local;
            var utcTzi = TimeZoneInfo.Utc;

            //------------------------------------------
            var dtLocalOffset = DateTimeOffset.Now;
            var dtUTCOffset = DateTimeOffset.UtcNow;

            //------------------------------------------

            var dtLocalDt = DateTime.Now;
            var dtUTCDt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            //------------------------------------------


            var fixedUtcDt = DateTime.UtcNow;

            var localDt = fixedUtcDt.AddHours(DateTimeOffset.Now.Offset.Hours);


            builder.Append("Data UTC: ").Append(fixedUtcDt.ToString("g"))
                    .AppendLine("\nData Local: ").Append(localDt.ToString("g"));

            return builder.ToString();
        }


        private static void converstionTest(DateTime date)
        {

        }
    }
}
