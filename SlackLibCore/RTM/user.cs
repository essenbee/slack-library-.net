using System.Collections.Generic;

namespace SlackLibCore
{
    public partial class RTM
    {
        public class User
        {
            public string color;
            public bool deleted;
            public string id;
            public bool is_admin;
            public bool is_bot;
            public bool is_owner;
            public bool is_primary_owner;
            public bool is_restrictred;
            public bool is_ultra_restricted;
            public string name;
            public string presence;
            public Dictionary<string, string> profile;
            public string real_name;
            public string team_id;
            public string tz;
            public string tz_label;
            public int tz_offset;

        }
    }
}