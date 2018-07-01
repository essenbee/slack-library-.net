using System;
using System.Collections.Generic;

namespace SlackLibCore
{
    public partial class RTM
    {
        public struct Team
        {
            public string domain;
            public string email_domain;
            public Icon icon;
            public string id;
            public int msg_edit_window_mins;
            public string name;
            public bool over_integrations_limit;
            public bool over_storage_limit;
            public string plan;
            public Dictionary<String, String> prefs;
        }
    }
}