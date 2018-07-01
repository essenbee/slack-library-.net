using System;

namespace SlackLibCore
{
    //https://api.slack.com/types/file
    
    public class FileObject
    {
        public class Reaction
        {
            public string name { get; }
            public int count { get; }
            public string[] users { get; }

            public Reaction(dynamic Data)
            {
                name = Data.name;
                count = Data.count;
                users = Data.users;
            }
        }
        
        public string _id;
        public bool _editable;
        public string id => _id;
        public int created { get; }
        public int timestamp { get; }       
        public string name { get; }
        public string title { get; }
        public string mimetype { get; }
        public string Filetype { get; }
        public string pretty_type { get; }
        public string user { get; }
        public string mode { get; }

        public bool editable
        {
            get
            {
                return _editable;
            }
        }
        
        public bool is_external { get; }
        public string external_type { get; }
        public int size { get; }
        public string url_private { get; }
        public string url_private_download { get; }
        public string thumb_64 { get; }
        public string thumb_80 { get; }
        public string thumb_360 { get; }
        public string thumb_360_gif { get; }
        public int thumb_360_w { get; }
        public int thumb_360_h { get; }
        public string permalink { get; }
        public string edit_link { get; }
        public string preview { get; }
        public string preview_highlight { get; } 
        public int lines { get; } 
        public int lines_more { get; }
        public bool is_public { get; }
        public bool public_url_shared { get; }
        public string[] channels { get; }
        public string[] groups { get; }
        public string[] ims { get; }
        public string initial_comment { get; }
        public int num_stars { get; }
        public bool is_starred { get; }
        public string[] pinned_to { get; }
        public Reaction[] reactions { get; }

        public FileObject(dynamic Data)
        {
            _id = Data.id;
            created = Utility.TryGetProperty(Data, "created", 0);
            timestamp = Utility.TryGetProperty(Data, "timestamp", 0);
            name = Data.name;
            title = Data.title;
            mimetype = Data.mimetype;
            Filetype = Data.filetype;
            pretty_type = Data.pretty_type;
            user = Data.user;
            mode = Data.mode;

            if (!Boolean.TryParse(Data.editable, out _editable))
            {
                _editable = false;
            }

            is_external = Utility.TryGetProperty(Data, "is_external", false);
            external_type = Data.external_type;
            size = Data.size ?? 0;
            url_private = Data.url_private;
            url_private_download = Data.url_private_download;
            thumb_64 = Data.thumb_64;
            thumb_80 = Data.thumb_80;
            thumb_360 = Data.thumb_360;
            thumb_360_gif = Data.thumb_360_gif;
            thumb_360_w = Data.thumb_360_w ?? 0;
            thumb_360_h = Data.thumb_360_h ?? 0;
            permalink = Data.permalink;
            edit_link = Data.edit_link;
            preview = Data.preview;
            preview_highlight = Data.preview_highlight;
            lines = Data.lines ?? 0;
            lines_more = Data.lines_more ?? 0;
            is_public = Data.is_public ?? false;
            public_url_shared = Data.public_url_shared ?? false;
            channels = Data.channels;
            groups = Data.groups;
            ims = Data.ims;
            initial_comment = Data.initial_comment;
            num_stars = Data.num_stars ?? 0;
            is_starred = Data.is_starred ?? false;
            pinned_to = Data.pinned_to;

            var numReactions = Data.reactions?.length ?? 0;

            if (numReactions > 0)
            {
                reactions = new Reaction[Data.reactions.length];

                for (var intCounter = 0; intCounter < Data.reactions.length; intCounter++)
                {
                    reactions[intCounter] = new Reaction(Data.reactions[intCounter]);
                }
            }
        }
    }
}
