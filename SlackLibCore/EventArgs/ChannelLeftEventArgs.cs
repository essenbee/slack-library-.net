namespace SlackLibCore
{
    public class ChannelLeftEventArgs
    {
        public string channel { get; }

        public ChannelLeftEventArgs(dynamic Data)
        {
            channel = Data.channel;
        }
    }
}
