using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore
{


    //https://api.slack.com/events/presence_change


    public class PresenceChangeEventArgs
    {


        private Client _client;

        private String _user;
        private String _presence;


        public PresenceChangeEventArgs(Client Client, dynamic Data)
        {
            _client = Client;
            _presence = Utility.TryGetProperty(Data, "presence");
            _user = Utility.TryGetProperty(Data, "user");
        }


        public String user
        {
            get
            {
                return _user;   
            }
        }


        public String presence
        {
            get
            {
                return _presence;
            }
        }


        public RTM.user UserInfo
        {
            get
            {
                foreach (RTM.user user in _client.MetaData.users)
                {
                    if (user.id == _user)
                    {
                        return user;
                    }
                }
                _client.RefreshUsers();
                foreach (RTM.user user in _client.MetaData.users)
                {
                    if (user.id == _user)
                    {
                        return user;
                    }
                }
                return null;
            }
        }


    }


}
