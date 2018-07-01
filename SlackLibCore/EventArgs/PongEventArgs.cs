using Newtonsoft.Json;

namespace SlackLibCore
{
    public class PongEventArgs
    {
        public string Reply { get; }

        public PongEventArgs(dynamic Data)
        {
            Reply = JsonConvert.SerializeObject(Data);
        }
    }
}
