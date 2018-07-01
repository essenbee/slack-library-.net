namespace SlackLibCore
{
    public partial class RTM
    {
        public struct Dnd
        {
            public bool dnd_enabled;
            public TimeStamp next_dnd_end_ts;
            public TimeStamp next_dnd_start_ts;
            public bool snooze_enabled;
        }
    }
}