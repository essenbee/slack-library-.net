using System;

namespace SlackLibCore
{
    //https://api.slack.com/types/file
    
    public class FileObject
    {
        public class reaction
        {
            public String name { get; }
            public Int32 count { get; }
            public String[] users { get; }

            public reaction(dynamic Data)
            {
                name = Data.name;
                count = Data.count;
                users = Data.users;
            }
        }
        
        public String _id;

        public Boolean _editable;
               
        public String id => _id;

        public Int32 created { get; }

        public Int32 timestamp { get; }
        
        public String name { get; }

        public String title { get; }
        
        public String mimetype { get; }
        
        public String Filetype { get; }
        
        public String pretty_type { get; }
        
        public String user { get; }
        
        public String mode { get; }
        
        public Boolean editable
        {
            get
            {
                return _editable;
            }
        }
        
        public Boolean is_external { get; }
        
        public String external_type { get; }

        public Int32 size { get; }
        
        public String url_private { get; }
        
        public String url_private_download { get; }
        
        public String thumb_64 { get; }

        public String thumb_80 { get; }

        public String thumb_360 { get; }
        
        public String thumb_360_gif { get; }
        
        public Int32 thumb_360_w { get; }
        
        public Int32 thumb_360_h { get; }
        
        public String permalink { get; }
        
        public String edit_link { get; }
        
        public String preview { get; }
        
        public String preview_highlight { get; }
        
        public Int32 lines { get; }
        
        public Int32 lines_more { get; }
        
        public Boolean is_public { get; }
        
        public Boolean public_url_shared { get; }
        
        public String[] channels { get; }
        
        public String[] groups { get; }
        
        public String[] ims { get; }
        
        public String initial_comment { get; }
        
        public Int32 num_stars { get; }
        
        public Boolean is_starred { get; }
        
        public String[] pinned_to { get; }
        
        public reaction[] reactions { get; }

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
                reactions = new reaction[Data.reactions.length];

                for (var intCounter = 0; intCounter < Data.reactions.length; intCounter++)
                {
                    reactions[intCounter] = new reaction(Data.reactions[intCounter]);
                }
            }
        }
    }
}
