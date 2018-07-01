using System.Collections.Generic;
using System.Text;

namespace SlackLibCore
{
    public class CommandEventArgs
    {
        public string FullCommandText { get; }
        public string Command { get; }
        public List<string> ArgsAsList { get; }
        public string ArgsAsString { get; }
        public string CommandIdentifier { get; } = Client.COMMAND_PREFIX;
        public string Channel { get; }
        public string User { get; }
        public string UserName { get; }

        private readonly Client _client; 

        public CommandEventArgs(Client client, string text, dynamic Data)
        {
            _client = client;
            Channel = Data.channel;
            FullCommandText = text;
            var commandPieces = text.Split(' ');
            Command = commandPieces[0].Replace(Client.COMMAND_PREFIX, string.Empty);
            ArgsAsList = new List<string>();
            var sb = new StringBuilder();

            for (var i = 1; i < commandPieces.Length; i++)
            {
                ArgsAsList.Add(commandPieces[i]);
                sb.Append(commandPieces[i]).Append(" ");
            }

            ArgsAsString = sb.ToString().Trim();
            User = Data.user ?? "<Unknown>";
            UserName = GetUserName(User);
        }

        private string GetUserName(string userId)
        {
                foreach (RTM.User user in _client.MetaData.users)
                {
                    if (user.id == userId)
                    {
                        return user.name;
                    }
                }

                return null;
        }
    }
}
