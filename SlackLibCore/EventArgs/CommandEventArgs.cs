using System;
using System.Collections.Generic;
using System.Text;

namespace SlackLibCore
{
    public class CommandEventArgs
    {
        public string FullCommandText { get; }
        public string User { get; }

        private readonly Client _client; 

        public CommandEventArgs(Client client, string text, dynamic Data)
        {
            _client = client;
            FullCommandText = text;
            User = Data.user ?? "<Unknown>";
        }
    }
}
