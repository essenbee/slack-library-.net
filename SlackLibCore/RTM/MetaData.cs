﻿using System;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;

namespace SlackLibCore
{
    public partial class RTM
    {
        public class MetaData
        {
            public List<Bot> bots;
            public TimeStamp cache_ts;
            public string cache_ts_version;
            public String cache_version;
            public List<Channel> channels;
            public Dnd dnd;
            public List<dynamic> groups;
            public List<Ims> ims;
            public TimeStamp latest_event_ts;
            public Boolean ok;
            public Self self;
            public Team team;
            public String url;
            public List<User> users;

            public MetaData(dynamic Message)
            {
                this.cache_ts = new TimeStamp((Int32)Utility.TryGetProperty(Message, "cache_ts", 0));
                this.bots = new List<RTM.Bot>();
                if (Utility.HasProperty(Message, "bots"))
                {
                    RTM.Bot rtmBot;
                    foreach (dynamic bot in Message.bots)
                    {
                        rtmBot = new RTM.Bot();
                        rtmBot.deleted = Utility.TryGetProperty(bot, "deleted", false);
                        rtmBot.id = Utility.TryGetProperty(bot, "id", "");
                        rtmBot.name = Utility.TryGetProperty(bot, "name", "");
                        this.bots.Add(rtmBot);
                    }
                }
                this.cache_ts_version = Utility.TryGetProperty(Message, "cache_ts_version");
                this.cache_version = Message.cache_version;
                this.channels = new List<RTM.Channel>();
                if (Utility.HasProperty(Message, "channels"))
                {
                    RTM.Channel rtmChannel;
                    foreach (dynamic channel in Message.channels)
                    {
                        rtmChannel = new RTM.Channel(this);
                        var ts = channel.created;

                        try
                        {
                            rtmChannel.created = new TimeStamp(ts);
                        }
                        catch (RuntimeBinderException)
                        {
                            rtmChannel.created = new TimeStamp(DateTime.Today);
                        }

                        rtmChannel.creator = channel.creator;
                        rtmChannel.has_pins = channel.has_pins;
                        rtmChannel.id = channel.id;
                        rtmChannel.is_archived = channel.is_archived;
                        rtmChannel.is_channel = channel.is_channel;
                        rtmChannel.is_general = channel.is_general;
                        rtmChannel.is_member = channel.is_member;
                        rtmChannel.name = channel.name;
                        this.channels.Add(rtmChannel);
                    }
                }
                this.dnd = new RTM.Dnd();
                if (Utility.HasProperty(Message, "dnd"))
                {
                    this.dnd.dnd_enabled = Utility.TryGetProperty(Message.dnd, "dnd_enabled", false);
                    this.dnd.next_dnd_end_ts = new TimeStamp(Utility.TryGetProperty(Message.dnd, "next_dnd_end_ts", "0"));
                    this.dnd.next_dnd_start_ts = new TimeStamp(Utility.TryGetProperty(Message.dnd, "next_dnd_start_ts", "0"));
                    this.dnd.snooze_enabled = Utility.TryGetProperty(Message.dnd, "snooze_enabled", true);
                }
                this.groups = new List<dynamic>();
                this.ims = new List<Ims>();
                if (Utility.HasProperty(Message, "ims"))
                {
                    Ims rtmIMS;
                    foreach (dynamic ims in Message.ims)
                    {
                        rtmIMS = new Ims(this);
                        rtmIMS.created = new TimeStamp(Utility.TryGetProperty(ims, "created", "0"));
                        rtmIMS.has_pins = Utility.TryGetProperty(ims, "has_pins", false);
                        rtmIMS.id = Utility.TryGetProperty(ims, "id", "");
                        rtmIMS.is_im = Utility.TryGetProperty(ims, "is_im", false);
                        rtmIMS.is_open = Utility.TryGetProperty(ims, "is_open", false);
                        rtmIMS.last_read = Utility.TryGetProperty(ims, "last_read");
                        rtmIMS.unread_count = Utility.TryGetProperty(ims, "unread_count", 0);
                        rtmIMS.unread_count_display = Utility.TryGetProperty(ims, "unread_count_display", 0);
                        rtmIMS.user = Utility.TryGetProperty(ims, "user");
                        this.ims.Add(rtmIMS);
                    }
                }
                this.latest_event_ts = new TimeStamp(Utility.TryGetProperty(Message, "latest_event_ts", "0"));
                this.ok = Utility.TryGetProperty(Message, "ok", false);
                this.self = new RTM.Self();
                if (Utility.HasProperty(Message, "self"))
                {
                    this.self.created = new TimeStamp(Utility.TryGetProperty(Message.self, "created", 0));
                    this.self.id = Utility.TryGetProperty(Message.self, "id");
                    this.self.manual_presence = Utility.TryGetProperty(Message.self, "manual_presence");
                    this.self.name = Utility.TryGetProperty(Message.self, "name");
                    this.self.prefs = new System.Dynamic.ExpandoObject();
                }
                this.team = new RTM.Team();
                if (Utility.HasProperty(Message, "team"))
                {
                    this.team.domain = Utility.TryGetProperty(Message.team, "domain");
                    this.team.email_domain = Utility.TryGetProperty(Message.team, "email_domain");
                    this.team.icon = new RTM.Icon();
                    if (Utility.HasProperty(Message.team, "icon"))
                    {
                        this.team.icon.image_102 = Utility.TryGetProperty(Message.team.icon, "image_102");
                        this.team.icon.image_132 = Utility.TryGetProperty(Message.team.icon, "image_132");
                        this.team.icon.image_34 = Utility.TryGetProperty(Message.team.icon, "image_34");
                        this.team.icon.image_44 = Utility.TryGetProperty(Message.team.icon, "image_44");
                        this.team.icon.image_68 = Utility.TryGetProperty(Message.team.icon, "image_68");
                        this.team.icon.image_88 = Utility.TryGetProperty(Message.team.icon, "image_88");
                        this.team.icon.image_default = Utility.TryGetProperty(Message.team.icon, "image_default", true);
                    }
                }
                this.url = Message.url;
                this.users = new List<RTM.User>();
                if (Utility.HasProperty(Message, "users"))
                {
                    RTM.User rtmUser;
                    foreach (dynamic user in Message.users)
                    {
                        rtmUser = new RTM.User();
                        rtmUser.color = Utility.TryGetProperty(user, "color");
                        rtmUser.deleted = Utility.TryGetProperty(user, "deleted", false);
                        rtmUser.id = Utility.TryGetProperty(user, "id");
                        rtmUser.is_bot = Utility.TryGetProperty(user, "is_bot", false);
                        if (!rtmUser.is_bot)
                        {
                            rtmUser.is_admin = Utility.TryGetProperty(user, "is_admin", false);
                            rtmUser.is_owner = Utility.TryGetProperty(user, "is_owner", false);
                            rtmUser.is_primary_owner = Utility.TryGetProperty(user, "is_primary_owner", false);
                            rtmUser.is_restrictred = Utility.TryGetProperty(user, "is_restricted", false);
                            rtmUser.is_ultra_restricted = Utility.TryGetProperty(user, "is_ultra_restricted", false);
                            rtmUser.real_name = Utility.TryGetProperty(user, "real_name");
                            rtmUser.tz = Utility.TryGetProperty(user, "tz");
                            rtmUser.tz_label = Utility.TryGetProperty(user, "tz_label");
                            rtmUser.tz_offset = Utility.TryGetProperty(user, "tz_offset", 0);
                        }
                        rtmUser.name = Utility.TryGetProperty(user, "name");
                        rtmUser.presence = Utility.TryGetProperty(user, "presence");
                        rtmUser.profile = new Dictionary<string, string>();
                        rtmUser.team_id = Utility.TryGetProperty(user, "team_id");
                        this.users.Add(rtmUser);
                    }
                }
            }
        }
    }
}
