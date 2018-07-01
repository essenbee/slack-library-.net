using System;

namespace SlackLibCore
{
    public class TimeStamp
    {
        public DateTime Date { get; set; }
        public int Order => intOrder;

        public static DateTime MinValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        private int intOrder;
        
        public TimeStamp(DateTime date)
        {

            if (date < MinValue)
            {
                date = MinValue;
            }
            Date = date;
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
                    if (dt < MinValue)
                    {
                        dt = MinValue;
                    }
                    this.Date = dt;
                    break;
                case String s:
                    var strTime = s;
                    intOrder = 0;

                    if (s.Contains("."))
                    {
                        strTime = s.Substring(0, s.IndexOf("."));
                        var strOrder = s.Substring(s.IndexOf(".") + 1);
                        Int32.TryParse(strOrder, out intOrder);
                    }

                    Double.TryParse(strTime, out var dblTimeStamp);
                    Date = MinValue.AddSeconds(dblTimeStamp).ToLocalTime();
                    break;
            }
        }

        public TimeStamp(string timeStamp)
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
            Date = MinValue.AddSeconds(dblTimeStamp).ToLocalTime();
        }
        
        public override string ToString()
        {
            DateTime dtUTC = Date.ToUniversalTime();
            Double dblSeconds = dtUTC.Subtract(MinValue).TotalSeconds;

            if (intOrder > 0)
            {
                return dblSeconds.ToString() + "." + intOrder.ToString();
            }
            return dblSeconds.ToString();
        }
    }
}
