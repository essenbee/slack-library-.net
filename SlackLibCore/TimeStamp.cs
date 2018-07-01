using System;

namespace SlackLibCore
{
    public class TimeStamp
    {
        public DateTime Date { get; set; }
        public int Order => intOrder;

        public static DateTime MinValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private int intOrder;
        
        public TimeStamp(DateTime date)
        {
            Date = SetDateFromDateTime(date);
        }

        public TimeStamp(Double unixTimeStamp)
        {
            Date = MinValue.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        public TimeStamp(dynamic dyn)
        {
            switch (dyn)
            {
                case Int32 i:
                    Date = MinValue.AddSeconds(i).ToLocalTime();
                    break;
                case Double d:
                    Date = MinValue.AddSeconds(d).ToLocalTime();
                    break;
                case DateTime dt:
                    Date = Date = SetDateFromDateTime(dt); ;
                    break;
                case String s:
                    Date = SetDateFromString(s);
                    break;
            }
        }

        public TimeStamp(string timeStamp)
        {
            Date = SetDateFromString(timeStamp);
        }
        
        public override string ToString()
        {
            var utc = Date.ToUniversalTime();
            var seconds = utc.Subtract(MinValue).TotalSeconds;

            if (intOrder > 0)
            {
                return seconds.ToString() + "." + intOrder.ToString();
            }

            return seconds.ToString();
        }

        private DateTime SetDateFromDateTime(DateTime date)
        {
            if (date < MinValue)
            {
                date = MinValue;
            }

            return date;
        }

        private DateTime SetDateFromString(string timeStamp)
        {
            var strTime = timeStamp;
            intOrder = 0;

            if (timeStamp.Contains("."))
            {
                strTime = timeStamp.Substring(0, timeStamp.IndexOf("."));
                var strOrder = timeStamp.Substring(timeStamp.IndexOf(".") + 1);
                Int32.TryParse(strOrder, out intOrder);
            }

            Double.TryParse(strTime, out var dblTimeStamp);
            return MinValue.AddSeconds(dblTimeStamp).ToLocalTime();
        }
    }
}
