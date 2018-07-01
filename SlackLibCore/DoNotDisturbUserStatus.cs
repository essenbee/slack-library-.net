using System;

namespace SlackLibCore
{
    public class DoNotDisturbUserStatus
    {
        public bool dnd_enabled { get; }
        public TimeStamp next_dnd_start_ts { get; }
        public TimeStamp next_dnd_end_ts { get; }

        public DoNotDisturbUserStatus(dynamic Data)
        {
            dnd_enabled = Data.dnd_enabled;
            if (dnd_enabled)
            {
                next_dnd_end_ts = new TimeStamp(((Double)Data.next_dnd_end_ts).ToString());
                next_dnd_start_ts = new TimeStamp(((Double)Data.next_dnd_start_ts).ToString());
            }
        }
    }
}
