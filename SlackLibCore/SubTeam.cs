using System;

namespace SlackLibCore
{
    //https://api.slack.com/events/subteam_created

    public class SubTeam
    {
        public string id { get; }
        public string team_id { get; }
        public bool is_usergroup { get; }
        public string name { get; }
        public string description { get; }
        public string handle { get; }
        public bool is_external { get; }
        public int date_created { get; }
        public int date_updated { get; }
        public int date_delete { get; }
        public string auto_type { get; }
        public string created_by { get; }
        public string updated_by { get; }
        public string deleted_by { get; }
        public Prefs prefs { get; }
        public string user_count { get; }

        public SubTeam(dynamic Data)
        {
            id = Data.id;
            team_id = Data.team_id;
            is_usergroup = Data.is_usergroup;
            name = Data.name;
            description = Data.description;
            handle = Data.handle;
            is_external = Data.is_external;
            date_created = Data.date_created;
            date_updated = Data.date_updated;
            date_delete = Data.date_delete;
            auto_type = Data.auto_type;
            created_by = Data.created_by;
            updated_by = Data.updated_by;
            deleted_by = Data.deleted_by;
            prefs = Data.prefs;
            user_count = Data.user_count;
        }
    }
}