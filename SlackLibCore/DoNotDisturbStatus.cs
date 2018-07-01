namespace SlackLibCore
{
    public class DoNotDisturbStatus
    {
        public bool dnd_enabled { get; }
        public TimeStamp next_dnd_start_ts { get; }
        public TimeStamp next_dnd_end_ts { get; }
        public bool snooze_enabled { get; }
        public TimeStamp snooze_endtime { get; }
        public int snooze_remaining { get; }

        public DoNotDisturbStatus(dynamic Data)
        {
            if (!Utility.HasProperty(Data, "dnd_status"))
            {
                return;
            }

            dnd_enabled = Utility.TryGetProperty(Data.dnd_status, "dnd_enabled");
            next_dnd_end_ts = new TimeStamp(Utility.TryGetProperty(Data.dnd_status, "next_dnd_end_ts"));
            next_dnd_start_ts = new TimeStamp(Utility.TryGetProperty(Data.dnd_status, "dnd_start_ts"));
            snooze_enabled = Utility.TryGetProperty(Data.dnd_status, "snooze_enabled", false);
            snooze_endtime = new TimeStamp(Utility.TryGetProperty(Data.dnd_status, "snooze_endtime"));
            snooze_remaining = Utility.TryGetProperty(Data.dnd_status, "snooze_remaining", 0);
        }
    }
}
