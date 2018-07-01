using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using Ninja.WebSockets;

namespace SlackLibCore
{
    public class Client : IDisposable
    {
        #region Declerations

        const string COMMAND_PREFIX = "!";

        public string APIKey { get; }
        public RTM.MetaData MetaData { get; private set; }
        public Channels.Collection Channels { get; }
        public Chat Chat { get; }
        public DND DND { get; }
        public IM IM { get; }

        #region Delegates

        public delegate void ServiceConnectedEventHandler();
        public delegate void ServiceConnectionFailedEventHandler();
        public delegate void ServiceDisconnectedEventHandler();
        public delegate void DataReceivedEventHandler(String data);
        public delegate void HelloEventHandler(HelloEventArgs e);
        public delegate void PongEventHandler(PongEventArgs e);
        public delegate void AccountsChangedEventHandler(AccountsChangedEventArgs e);
        public delegate void BotAddedEventHandler(BotAddedEventArgs e);
        public delegate void BotChangedEventHandler(BotChangedEventArgs e);
        public delegate void ChannelArchiveEventHandler(ChannelArchiveEventArgs e);
        public delegate void ChannelCreatedEventHandler(ChannelCreatedEventArgs e);
        public delegate void ChannelDeletedEventHandler(ChannelDeletedEventArgs e);
        public delegate void ChannelHistoryChangedEventHandler(ChannelHistoryChangedEventArgs e);
        public delegate void ChannelJoinedEventHandler(ChannelJoinedEventArgs e);
        public delegate void ChannelLeftEventHandler(ChannelLeftEventArgs e);
        public delegate void ChannelMarkedEventHandler(ChannelMarkedEventArgs e);
        public delegate void ChannelRenameEventHandler(ChannelRenameEventArgs e);
        public delegate void ChannelUnarchiveEventHandler(ChannelUnarchiveEventArgs e);
        public delegate void CommandsChangedEventHandler(CommandsChangedEventArgs e);
        public delegate void DoNotDistrubUpdatedEventHandler(DoNotDisturbUpdatedEventArgs e);
        public delegate void DoNotDistrubUpdatedUserEventHandler(DoNotDisturbUpdatedUserEventArgs e);
        public delegate void EmailDomainChangedEventHandler(EmailDomainChangedEventArgs e);
        public delegate void EmojiChangedEventHandler(EmojiChangedEventArgs e);
        public delegate void FileChangedEventHandler(FileChangeEventArgs e);
        public delegate void FileCommentAddedEventHandler(FileCommentAddedEventArgs e);
        public delegate void FileCommentEditedEventHandler(FileCommentEditedEventArgs e);
        public delegate void FileCommentDeletedEventHandler(FileCommentDeletedEventArgs e);
        public delegate void FileCreatedEventHandler(FileCreatedEventArgs e);
        public delegate void FileDeletedEventHandler(FileDeletedEventArgs e);
        public delegate void FilePrivateEventHandler(FilePrivateEventArgs e);
        public delegate void FilePublicEventHandler(FilePublicEventArgs e);
        public delegate void FileSharedEventHandler(FileSharedEventArgs e);
        public delegate void FileUnsharedEventHandler(FileUnsharedEventArgs e);
        public delegate void GroupArchiveEventHandler(GroupArchiveEventArgs e);
        public delegate void GroupCloseEventHandler(GroupCloseEventArgs e);
        public delegate void GroupHistoryChangedEventHandler(GroupHistoryChangedEventArgs e);
        public delegate void GroupJoinedEventHandler(GroupJoinedEventArgs e);
        public delegate void GroupLeftEventHandler(GroupLeftEventArgs e);
        public delegate void GroupMarkedEventHandler(GroupMarkedEventArgs e);
        public delegate void GroupOpenEventHandler(GroupOpenEventArgs e);
        public delegate void GroupRenameEventHandler(GroupRenameEventArgs e);
        public delegate void GroupUnarchiveEventHandler(GroupUnarchiveEventArgs e);
        public delegate void IMCloseEventHandler(IMCloseEventArgs e);
        public delegate void IMCreatedEventHandler(IMCreatedEventArgs e);
        public delegate void IMHistoryChangedEventHandler(IMHistoryChangedEventArgs e);
        public delegate void IMMarkedEventHandler(IMMarkedEventArgs e);
        public delegate void IMOpenEventHandler(IMOpenEventArgs e);
        public delegate void ManualPresenceChangeEventHandler(ManualPresenceChangeEventArgs e);
        public delegate void MessageEventHandler(MessageEventArgs e);
        public delegate void MessageEditEventHandler(MessageEditEventArgs e);
        public delegate void PinAddedEventHandler(PinAddedEventArgs e);
        public delegate void PinRemovedEventHandler(PinRemovedEventArgs e);
        public delegate void PrefChangedEventHandler(PrefChangedEventArgs e);
        public delegate void PresenceChangedEventHandler(PresenceChangeEventArgs e);
        public delegate void ReactionAddedEventHandler(ReactionAddedEventArgs e);
        public delegate void ReactionRemovedEventHandler(ReactionRemovedEventArgs e);
        public delegate void StarAddedEventHandler(StarAddedEventArgs e);
        public delegate void StarRemovedEventHandler(StarRemovedEventArgs e);
        public delegate void SubTeamCreatedEventHandler(SubTeamCreatedEventArgs e);
        public delegate void SubTeamSelfAddedEventHandler(SubTeamSelfAddedEventArgs e);
        public delegate void SubTeamSelfRemovedEventHandler(SubTeamSelfRemovedEventArgs e);
        public delegate void SubTeamUpdatedEventHandler(SubTeamUpdatedEventArgs e);
        public delegate void TeamDomainChangeEventHandler(TeamDomainChangeEventArgs e);
        public delegate void TeamJoinEventHandler(TeamJoinEventArgs e);
        public delegate void TeamMigrationStartedEventHandler(TeamMigrationStartedEventArgs e);
        public delegate void TeamPlanChangeEventHandler(TeamPlanChangeEventArgs e);
        public delegate void TeamPrefChangeEventHandler(TeamPrefChangeEventArgs e);
        public delegate void TeamProfileChangeEventHandler(TeamProfileChangeEventArgs e);
        public delegate void TeamProfileDeleteEventHandler(TeamProfileDeleteEventArgs e);
        public delegate void TeamProfileReorderEventHandler(TeamProfileReorderEventArgs e);
        public delegate void TeamRenameEventHandler(TeamRenameEventArgs e);
        public delegate void UserChangeEventHandler(UserChangeEventArgs e);
        public delegate void UserTypingEventHandler(UserTypingEventArgs e);


        #endregion

        private const string URL_RTM_START = "https://slack.com/api/rtm.start";

        private WebSocket webSocket;
        private Thread clientThread = null;

        #region Public Events

        public event ServiceConnectedEventHandler ServiceConnected = null;
        public event ServiceConnectionFailedEventHandler ServiceConnectionFailed = null;
        public event ServiceDisconnectedEventHandler ServiceDisconnected = null;

        public event DataReceivedEventHandler DataReceived = null;
        public event HelloEventHandler Hello = null;
        public event PongEventHandler Pong = null;

        public event AccountsChangedEventHandler AccountsChanged = null;
        public event BotAddedEventHandler BotAdded = null;
        public event BotChangedEventHandler BotChanged = null;
        public event ChannelArchiveEventHandler ChannelArchive = null;
        public event ChannelCreatedEventHandler ChannelCreated = null;
        public event ChannelDeletedEventHandler ChannelDeleted = null;
        public event ChannelHistoryChangedEventHandler ChannelHistoryChanged = null;
        public event ChannelJoinedEventHandler ChannelJoined = null;
        public event ChannelLeftEventHandler ChannelLeft = null;
        public event ChannelMarkedEventHandler ChannelMarked = null;
        public event ChannelRenameEventHandler ChannelRename = null;
        public event ChannelUnarchiveEventHandler ChannelUnarchive = null;
        public event CommandsChangedEventHandler CommandsChanged = null;
        public event DoNotDistrubUpdatedEventHandler DoNotDisturbUpdated = null;
        public event DoNotDistrubUpdatedUserEventHandler DoNotDisturbUpdatedUser = null;
        public event EmailDomainChangedEventHandler EmailDomainChanged = null;
        public event EmojiChangedEventHandler EmojiChanged = null;
        public event FileChangedEventHandler FileChanged = null;
        public event FileCommentAddedEventHandler FileCommentAdded = null;
        public event FileCommentEditedEventHandler FileCommentEdited = null;
        public event FileCommentDeletedEventHandler FileCommentDeleted = null;
        public event FileCreatedEventHandler FileCreated = null;
        public event FileDeletedEventHandler FileDeleted = null;
        public event FilePrivateEventHandler FilePrivate = null;
        public event FilePublicEventHandler FilePublic = null;
        public event FileSharedEventHandler FileShared = null;
        public event FileUnsharedEventHandler FileUnshared = null;
        public event GroupArchiveEventHandler GroupArchive = null;
        public event GroupCloseEventHandler GroupClose = null;
        public event GroupHistoryChangedEventHandler GroupHistoryChanged = null;
        public event GroupJoinedEventHandler GroupJoined = null;
        public event GroupLeftEventHandler GroupLeft = null;
        public event GroupMarkedEventHandler GroupMarked = null;
        public event GroupOpenEventHandler GroupOpen = null;
        public event GroupRenameEventHandler GroupRename = null;
        public event GroupUnarchiveEventHandler GroupUnarchive = null;
        public event IMCloseEventHandler IMClose = null;
        public event IMCreatedEventHandler IMCreated = null;
        public event IMHistoryChangedEventHandler IMHistoryChanged = null;
        public event IMMarkedEventHandler IMMarked = null;
        public event IMOpenEventHandler IMOpened = null;
        public event ManualPresenceChangeEventHandler ManualPresenceChange = null;
        public event MessageEventHandler Message = null;
        public event MessageEditEventHandler MesssageEdit = null;
        public event PinAddedEventHandler PinAdded = null;
        public event PinRemovedEventHandler PinRemoved = null;
        public event PrefChangedEventHandler PrefChanged = null;
        public event PresenceChangedEventHandler PresenceChanged = null;
        public event ReactionAddedEventHandler ReactionAdded = null;
        public event ReactionRemovedEventHandler ReactionRemoved = null;
        public event StarAddedEventHandler StarAdded = null;
        public event StarRemovedEventHandler StarRemoved = null;
        public event SubTeamCreatedEventHandler SubTeamCreated = null;
        public event SubTeamSelfAddedEventHandler SubTeamSelfAdded = null;
        public event SubTeamSelfRemovedEventHandler SubTeamSelfRemoved = null;
        public event SubTeamUpdatedEventHandler SubTeamUpdated = null;
        public event TeamDomainChangeEventHandler TeamDomainChange = null;
        public event TeamJoinEventHandler TeamJoin = null;
        public event TeamMigrationStartedEventHandler TeamMigrationStarted = null;
        public event TeamPlanChangeEventHandler TeamPlanChange = null;
        public event TeamPrefChangeEventHandler TeamPrefChange = null;
        public event TeamProfileChangeEventHandler TeamProfileChange = null;
        public event TeamProfileDeleteEventHandler TeamProfileDelete = null;
        public event TeamProfileReorderEventHandler TeamProfileReorder = null;
        public event TeamRenameEventHandler TeamRename = null;
        public event UserChangeEventHandler UserChange = null;
        public event UserTypingEventHandler UserTyping = null;

        #endregion

        #endregion

        public Client(String apiKey)
        {
            APIKey = apiKey;
            Channels = new Channels.Collection(this);
            Chat = new Chat(this);
            DND = new DND(this);
            IM = new IM(this);
        }

        public void Dispose()
        {
            Disconnect();
        }

        public void RefreshUsers()
        {
            try
            {
                _refreshRTMMetaData();
            }
            catch (Exception)
            {
                throw new Exceptions.UnknownErrorException();
            }
        }

        private void _refreshRTMMetaData()
        {
            try
            {
                var strJSON = _downloadConnectionInfo();
                dynamic Message = null;
                Message = JObject.Parse(strJSON);
                MetaData = new RTM.MetaData(Message);
            }
            catch (Exception ex)
            {
                throw new Exceptions.ServiceDisconnectedException(ex);
            }
        }

        private string _downloadConnectionInfo()
        {
            try
            {
                var strURL = URL_RTM_START + "?token=" + APIKey;
                var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                var httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get connection info.", ex);
            }
        }

        public void Connect()
        {
            //run client on it's own thread
            try
            {
                clientThread?.Abort();
            }
            catch (ThreadAbortException)
            {
                //this is fine, client could be shut down
            }
            catch (Exception ex)
            {
                throw ex;
            }
            clientThread = new Thread(_connect);
            clientThread.Start();
        }

        private void _connect()
        {
            try
            {
                _refreshRTMMetaData();
            }
            catch (Exceptions.ServiceDisconnectedException)
            {
                ServiceDisconnected?.Invoke();
                return;
            }
            try
            {
                var factory = new WebSocketClientFactory();
                webSocket = factory.ConnectAsync(new Uri(MetaData.url), new WebSocketClientOptions
                {
                    KeepAliveInterval = TimeSpan.Zero,
                    NoDelay = true,
                }).Result;

                var pinger = new Pinger(webSocket, new TimeSpan(0, 0, 30), CancellationToken.None);

            }
            catch (Exception)
            {
                ServiceConnectionFailed?.Invoke();
                return; //bail....we can do nothing
            }
            try
            {
                ServiceConnected?.Invoke();
                _processMessages();
            }
            catch (ThreadAbortException)
            {
                //this is fine, client is being shutdown
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public void Disconnect()
        {
            _disconnect();
        }

        private void _disconnect()
        {
            try
            {
                if (webSocket != null)
                {
                    Task tsk = webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    tsk.Wait();
                    webSocket.Dispose();
                    webSocket = null;
                }

                ServiceDisconnected?.Invoke();
            }
            catch (ThreadAbortException)
            {
                //this is fine, client is being shutdown
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        public String APIRequest(String strURL)
        {
            try
            {
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    String strResponse = streamReader.ReadToEnd();
                    DataReceived?.Invoke(strResponse);
                    return strResponse;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not submit api request.", ex);
            }
        }

        public Boolean APITest()
        {
            try
            {
                String strResponse = APIRequest("https://slack.com/api/api.test");
                dynamic Response = JObject.Parse(strResponse);
                return Response.ok;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not perform API test.", ex);
            }
        }

        public AuthTestResponse AuthTest()
        {
            //https://api.slack.com/methods/api.test
            dynamic Response;
            try
            {
                String strResponse = APIRequest("https://slack.com/api/auth.test?token=" + APIKey);
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not perform auth test.", ex);
            }
            CheckForError(Response);
            return new AuthTestResponse(Response);
        }

        public void CheckForError(dynamic Response)
        {
            if (!Utility.HasProperty(Response, "ok"))
            {
                //no "ok" property.....that should probably be an exception too??
                return;
            }

            var okay = (bool)Utility.TryGetProperty(Response, "ok", false);

            if (okay)
            {
                switch ((string)Response.error)
                {
                    case "account_inactive":
                        throw new Exceptions.AccountInactiveException();
                    case "already_archived":
                        throw new Exceptions.AlreadyArchivedException();
                    case "already_in_channel":
                        throw new Exceptions.AlreadyInChannelException();
                    case "cant_archive_general":
                        throw new Exceptions.CantArchiveGeneralException();
                    case "cant_delete_message":
                        throw new Exceptions.CantDeleteMessageException();
                    case "cant_invite":
                        throw new Exceptions.CantInviteException();
                    case "cant_invite_self":
                        throw new Exceptions.CantInviteSelfException();
                    case "cant_kick_self":
                        throw new Exceptions.CantKickSelfException();
                    case "cant_kick_from_general":
                        throw new Exceptions.CantKickFromGeneralException();
                    case "cant_kick_from_last_channel":
                        throw new Exceptions.CantKickFromLastChannelException();
                    case "cant_leave_general":
                        throw new Exceptions.CantLeaveGeneralException();
                    case "cant_update_message":
                        throw new Exceptions.CantUpdateMessageException();
                    case "channel_not_found":
                        throw new Exceptions.ChannelNotFoundException();
                    case "compliance_exports_prevent_deletion":
                        throw new Exceptions.ComplianceExportsPreventDeletionException();
                    case "edit_window_closed":
                        throw new Exceptions.EditWindowClosedException();
                    case "invalid_array_arg":
                        throw new Exceptions.InvalidArrayArgException();
                    case "invalid_auth":
                        throw new Exceptions.InvalidAuthException();
                    case "invalid_charset":
                        throw new Exceptions.InvalidCharsetException();
                    case "invalid_form_data":
                        throw new Exceptions.InvalidFormDataException();
                    case "invalid_post_type":
                        throw new Exceptions.InvalidPostTypeException();
                    case "invalid_timestamp":
                        throw new Exceptions.InvalidTimestampException();
                    case "invalid_ts_latest":
                        throw new Exceptions.InvalidTSLatestException();
                    case "invalid_ts_oldest":
                        throw new Exceptions.InvalidTSOldestException();
                    case "is_archived":
                        throw new Exceptions.IsArchivedException();
                    case "last_ra_channel":
                        throw new Exceptions.LastRestrictedAccountException();
                    case "message_not_found":
                        throw new Exceptions.MessageNotFoundException();
                    case "missing_duration":
                        throw new Exceptions.MissingDurationException();
                    case "missing_post_type":
                        throw new Exceptions.MissingPostTypeException();
                    case "msg_too_long":
                        throw new Exceptions.MessageTooLongException();
                    case "name_taken":
                        throw new Exceptions.NameTakenException();
                    case "no_channel":
                        throw new Exceptions.NoChannelException();
                    case "no_text":
                        throw new Exceptions.NoTextException();
                    case "not_archived":
                        throw new Exceptions.NotArchivedException();
                    case "not_authed":
                        throw new Exceptions.NotAuthedException();
                    case "not_in_channel":
                        throw new Exceptions.NotInChannelException();
                    case "rate_limited":
                        throw new Exceptions.RateLimitedException();
                    case "restricted_action":
                        throw new Exceptions.RestrictedActionException();
                    case "request_timeout":
                        throw new Exceptions.RequestTimeoutException();
                    case "too_long":
                        throw new Exceptions.TooLongException();
                    case "unknown_error":
                        throw new Exceptions.UnknownErrorException();
                    case "user_disabled":
                        throw new Exceptions.UserDisabledException();
                    case "user_is_bot":
                        throw new Exceptions.UserIsBotException();
                    case "user_is_restricted":
                        throw new Exceptions.UserIsRestrictedException();
                    case "user_is_ultra_restricted":
                        throw new Exceptions.UserIsUltraRestrictedException();
                    case "user_not_found":
                        throw new Exceptions.UserNotFoundException();
                    case "user_not_visible":
                        throw new Exceptions.UserNotVisibleException();
                }
            }
        }

        private void _processMessages()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Task<String> tsk = _readMessage();
                        tsk.Wait();
                        var strMessage = tsk.Result;
                        DataReceived?.Invoke(strMessage);
                        dynamic Data = JObject.Parse(strMessage);
                        switch ((string)Data.type)
                        {
                            case "hello":
                                HelloEventArgs helloEventArgs = new HelloEventArgs();
                                Hello?.Invoke(helloEventArgs);
                                break;
                            case "pong":
                                var pongEventArgs = new PongEventArgs(Data);
                                Pong?.Invoke(pongEventArgs);

                                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tPong!");
                                break;
                            case "accounts_changed":
                                _refreshRTMMetaData();
                                AccountsChangedEventArgs accountsChangedEventArgs = new AccountsChangedEventArgs(Data);
                                AccountsChanged?.Invoke(accountsChangedEventArgs);
                                break;
                            case "bot_added":
                                BotAddedEventArgs botAddedEventArgs = new BotAddedEventArgs(Data);
                                BotAdded?.Invoke(botAddedEventArgs);
                                break;
                            case "bot_changed":
                                BotChangedEventArgs botChangedEventArgs = new BotChangedEventArgs(Data);
                                BotChanged?.Invoke(botChangedEventArgs);
                                break;
                            case "channel_archive":
                                ChannelArchiveEventArgs channelArchiveEventArgs = new ChannelArchiveEventArgs(Data);
                                ChannelArchive?.Invoke(channelArchiveEventArgs);
                                break;
                            case "channel_created":
                                ChannelCreatedEventArgs channelCreatedEventArgs = new ChannelCreatedEventArgs(Data);
                                ChannelCreated?.Invoke(channelCreatedEventArgs);
                                break;
                            case "channel_deleted":
                                ChannelDeletedEventArgs channelDeletedEventArgs = new ChannelDeletedEventArgs(Data);
                                ChannelDeleted?.Invoke(channelDeletedEventArgs);
                                break;
                            case "channel_history_changed":
                                ChannelHistoryChangedEventArgs channelHistoryChangedEventArgs = new ChannelHistoryChangedEventArgs(Data);
                                ChannelHistoryChanged?.Invoke(channelHistoryChangedEventArgs);
                                break;
                            case "channel_joined":
                                ChannelJoinedEventArgs channelJoinedEventArgs = new ChannelJoinedEventArgs(Data);
                                ChannelJoined?.Invoke(channelJoinedEventArgs);
                                break;
                            case "channel_left":
                                ChannelLeftEventArgs channelLeftEventArgs = new ChannelLeftEventArgs(Data);
                                ChannelLeft?.Invoke(channelLeftEventArgs);
                                break;
                            case "channel_marked":
                                ChannelMarkedEventArgs channelMarkedEventArgs = new ChannelMarkedEventArgs(Data);
                                ChannelMarked?.Invoke(channelMarkedEventArgs);
                                break;
                            case "channel_rename":
                                ChannelRenameEventArgs channelRenameEventArgs = new ChannelRenameEventArgs(Data);
                                ChannelRename?.Invoke(channelRenameEventArgs);
                                break;
                            case "channel_unarchive":
                                ChannelUnarchiveEventArgs channelUnarchiveEventArgs = new ChannelUnarchiveEventArgs(Data);
                                ChannelUnarchive?.Invoke(channelUnarchiveEventArgs);
                                break;
                            case "commands_changed":
                                CommandsChangedEventArgs commandsChangedEventArgs = new CommandsChangedEventArgs(Data);
                                CommandsChanged?.Invoke(commandsChangedEventArgs);
                                break;
                            case "dnd_updated":
                                DoNotDisturbUpdatedEventArgs dndUpdatedEventArgs = new DoNotDisturbUpdatedEventArgs(Data);
                                DoNotDisturbUpdated?.Invoke(dndUpdatedEventArgs);
                                break;
                            case "dnd_updated_user":
                                DoNotDisturbUpdatedUserEventArgs dndUpdatedUserEventArgs = new DoNotDisturbUpdatedUserEventArgs(this, Data);
                                DoNotDisturbUpdatedUser?.Invoke(dndUpdatedUserEventArgs);
                                break;
                            case "email_domain_changed":
                                EmailDomainChangedEventArgs emailDomainChangedEventArgs = new EmailDomainChangedEventArgs(Data);
                                EmailDomainChanged?.Invoke(emailDomainChangedEventArgs);
                                break;
                            case "emoji_changed":
                                EmojiChangedEventArgs emojiChangedEventArgs = new EmojiChangedEventArgs(Data);
                                EmojiChanged?.Invoke(emojiChangedEventArgs);
                                break;
                            case "file_change":
                                FileChangeEventArgs fileChangeEventArgs = new FileChangeEventArgs(Data);
                                FileChanged?.Invoke(fileChangeEventArgs);
                                break;
                            case "file_comment_added":
                                FileCommentAddedEventArgs fileCommentAddedEventArgs = new FileCommentAddedEventArgs(Data);
                                FileCommentAdded?.Invoke(fileCommentAddedEventArgs);
                                break;
                            case "file_comment_edited":
                                FileCommentEditedEventArgs fileCommentEditedEventArgs = new FileCommentEditedEventArgs(Data);
                                FileCommentEdited?.Invoke(fileCommentEditedEventArgs);
                                break;
                            case "file_comment_deleted":
                                FileCommentDeletedEventArgs fileCommentDeletedEventArgs = new FileCommentDeletedEventArgs(Data);
                                FileCommentDeleted?.Invoke(fileCommentDeletedEventArgs);
                                break;
                            case "file_created":
                                FileCreatedEventArgs fileCreatedEventArgs = new FileCreatedEventArgs(Data);
                                FileCreated?.Invoke(fileCreatedEventArgs);
                                break;
                            case "file_deleted":
                                FileDeletedEventArgs fileDeletedEventArgs = new FileDeletedEventArgs(Data);
                                FileDeleted?.Invoke(fileDeletedEventArgs);
                                break;
                            case "file_private":
                                FilePrivateEventArgs filePrivateEventArgs = new FilePrivateEventArgs(Data);
                                FilePrivate?.Invoke(filePrivateEventArgs);
                                break;
                            case "file_public":
                                FilePublicEventArgs filePublicEventArgs = new FilePublicEventArgs(Data);
                                FilePublic?.Invoke(filePublicEventArgs);
                                break;
                            case "file_shared":
                                FileSharedEventArgs fileSharedEventArgs = new FileSharedEventArgs(Data);
                                FileShared?.Invoke(fileSharedEventArgs);
                                break;
                            case "file_unshared":
                                FileUnsharedEventArgs fileUnsharedEventArgs = new FileUnsharedEventArgs(Data);
                                FileUnshared?.Invoke(fileUnsharedEventArgs);
                                break;
                            case "group_archive":
                                GroupArchiveEventArgs groupArchiveEventArgs = new GroupArchiveEventArgs(Data);
                                GroupArchive?.Invoke(groupArchiveEventArgs);
                                break;
                            case "group_close":
                                GroupCloseEventArgs groupCloseEventArgs = new GroupCloseEventArgs(Data);
                                GroupClose?.Invoke(groupCloseEventArgs);
                                break;
                            case "group_history_changed":
                                GroupHistoryChangedEventArgs groupHistoryChangedEventArgs = new GroupHistoryChangedEventArgs(Data);
                                GroupHistoryChanged?.Invoke(groupHistoryChangedEventArgs);
                                break;
                            case "group_joined":
                                GroupJoinedEventArgs groupJoinedEventArgs = new GroupJoinedEventArgs(Data);
                                GroupJoined?.Invoke(groupJoinedEventArgs);
                                break;
                            case "group_left":
                                GroupLeftEventArgs groupLeftEventArgs = new GroupLeftEventArgs(Data);
                                GroupLeft?.Invoke(groupLeftEventArgs);
                                break;
                            case "group_marked":
                                GroupMarkedEventArgs groupMarkedEventArgs = new GroupMarkedEventArgs(Data);
                                GroupMarked?.Invoke(groupMarkedEventArgs);
                                break;
                            case "group_open":
                                GroupOpenEventArgs groupOpenEventArgs = new GroupOpenEventArgs(Data);
                                GroupOpen?.Invoke(groupOpenEventArgs);
                                break;
                            case "group_rename":
                                GroupRenameEventArgs groupRenameEventArgs = new GroupRenameEventArgs(Data);
                                GroupRename?.Invoke(groupRenameEventArgs);
                                break;
                            case "group_unarchive":
                                GroupUnarchiveEventArgs groupUnarchiveEventArgs = new GroupUnarchiveEventArgs(Data);
                                GroupUnarchive?.Invoke(groupUnarchiveEventArgs);
                                break;
                            case "im_close":
                                IMCloseEventArgs imCloseEventArgs = new IMCloseEventArgs(Data);
                                IMClose?.Invoke(imCloseEventArgs);
                                break;
                            case "im_created":
                                IMCreatedEventArgs imCreatedEventArgs = new IMCreatedEventArgs(Data);
                                IMCreated?.Invoke(imCreatedEventArgs);
                                break;
                            case "im_history_changed":
                                IMHistoryChangedEventArgs imHistoryChangedEventArgs = new IMHistoryChangedEventArgs(Data);
                                IMHistoryChanged?.Invoke(imHistoryChangedEventArgs);
                                break;
                            case "im_marked":
                                IMMarkedEventArgs imMarkedEventArgs = new IMMarkedEventArgs(Data);
                                IMMarked?.Invoke(imMarkedEventArgs);
                                break;
                            case "im_open":
                                IMOpenEventArgs imOpenEventArgs = new IMOpenEventArgs(Data);
                                IMOpened?.Invoke(imOpenEventArgs);
                                break;
                            case "manual_presence_change":
                                ManualPresenceChangeEventArgs manualPresenceChangeEventArgs = new ManualPresenceChangeEventArgs(Data);
                                ManualPresenceChange?.Invoke(manualPresenceChangeEventArgs);
                                break;
                            case "message":
                                if (Data.previous_message == null)
                                {
                                    // Commands
                                    string text = Utility.TryGetProperty(Data, "text", string.Empty);
                                    if (text.StartsWith(COMMAND_PREFIX))
                                    {
                                        text = text.ToLower();

                                        Console.WriteLine($"Command detected: {Data.text}");
                                    }

                                    MessageEventArgs messagEventArgs = new MessageEventArgs(this, Data);
                                    Message?.Invoke(messagEventArgs);
                                }
                                else
                                {
                                    MessageEditEventArgs messageEditEventArgs = new MessageEditEventArgs(this, Data);
                                    MesssageEdit?.Invoke(messageEditEventArgs);
                                }
                                break;
                            case "pin_added":
                                PinAddedEventArgs pinAddedEventArgs = new PinAddedEventArgs(Data);
                                PinAdded?.Invoke(pinAddedEventArgs);
                                break;
                            case "pin_removed":
                                PinRemovedEventArgs pinRemovedEventArgs = new PinRemovedEventArgs(Data);
                                PinRemoved?.Invoke(pinRemovedEventArgs);
                                break;
                            case "pref_changed":
                                PrefChangedEventArgs prefChangedEventArgs = new PrefChangedEventArgs(Data);
                                PrefChanged?.Invoke(prefChangedEventArgs);
                                break;
                            case "presence_change":
                                PresenceChangeEventArgs presenceChangeEventArgs = new PresenceChangeEventArgs(this, Data);
                                PresenceChanged?.Invoke(presenceChangeEventArgs);
                                break;
                            case "reaction_added":
                                ReactionAddedEventArgs reactionAddedEventArgs = new ReactionAddedEventArgs(Data);
                                ReactionAdded?.Invoke(reactionAddedEventArgs);
                                break;
                            case "reaction_removed":
                                ReactionRemovedEventArgs reactionRemovedEventArgs = new ReactionRemovedEventArgs(Data);
                                ReactionRemoved?.Invoke(reactionRemovedEventArgs);
                                break;
                            case "star_added":
                                StarAddedEventArgs starAddedEventArgs = new StarAddedEventArgs(Data);
                                StarAdded?.Invoke(starAddedEventArgs);
                                break;
                            case "star_removed":
                                StarRemovedEventArgs starRemovedEventArgs = new StarRemovedEventArgs(Data);
                                StarRemoved?.Invoke(starRemovedEventArgs);
                                break;
                            case "subteam_created":
                                SubTeamCreatedEventArgs subTeamCreatedEventArgs = new SubTeamCreatedEventArgs(Data);
                                SubTeamCreated?.Invoke(subTeamCreatedEventArgs);
                                break;
                            case "subteam_self_added":
                                SubTeamSelfAddedEventArgs subTeamSelfAddedEventArgs = new SubTeamSelfAddedEventArgs(Data);
                                SubTeamSelfAdded?.Invoke(subTeamSelfAddedEventArgs);
                                break;
                            case "subteam_self_removed":
                                SubTeamSelfRemovedEventArgs subTeamSelfRemovedEventArgs = new SubTeamSelfRemovedEventArgs(Data);
                                SubTeamSelfRemoved?.Invoke(subTeamSelfRemovedEventArgs);
                                break;
                            case "subteam_updated":
                                SubTeamUpdatedEventArgs subTeamUpdatedEventArgs = new SubTeamUpdatedEventArgs(Data);
                                SubTeamUpdated?.Invoke(subTeamUpdatedEventArgs);
                                break;
                            case "team_domain_change":
                                TeamDomainChangeEventArgs teamDomainChangeEventArgs = new TeamDomainChangeEventArgs(Data);
                                TeamDomainChange?.Invoke(teamDomainChangeEventArgs);
                                break;
                            case "team_join":
                                TeamJoinEventArgs teamJoinEventArgs = new TeamJoinEventArgs(Data);
                                TeamJoin?.Invoke(teamJoinEventArgs);
                                break;
                            case "team_migration_started":
                                TeamMigrationStartedEventArgs teamMigrationStartedEventArgs = new TeamMigrationStartedEventArgs(Data);
                                TeamMigrationStarted?.Invoke(teamMigrationStartedEventArgs);
                                break;
                            case "team_plan_change":
                                TeamPlanChangeEventArgs teamPlanChangeEventArgs = new TeamPlanChangeEventArgs(Data);
                                TeamPlanChange?.Invoke(teamPlanChangeEventArgs);
                                break;
                            case "team_pref_change":
                                TeamPrefChangeEventArgs teamPrefChangeEventArgs = new TeamPrefChangeEventArgs(Data);
                                TeamPrefChange?.Invoke(teamPrefChangeEventArgs);
                                break;
                            case "team_profile_change":
                                TeamProfileChangeEventArgs teamProfileChangeEventArgs = new TeamProfileChangeEventArgs(Data);
                                TeamProfileChange?.Invoke(teamProfileChangeEventArgs);
                                break;
                            case "team_profile_delete":
                                TeamProfileDeleteEventArgs teamProfileDeleteEventArgs = new TeamProfileDeleteEventArgs(Data);
                                TeamProfileDelete?.Invoke(teamProfileDeleteEventArgs);
                                break;
                            case "team_profile_reorder":
                                TeamProfileReorderEventArgs teamProfileReorderEventArgs = new TeamProfileReorderEventArgs(Data);
                                TeamProfileReorder?.Invoke(teamProfileReorderEventArgs);
                                break;
                            case "team_rename":
                                TeamRenameEventArgs teamRenameEventArgs = new TeamRenameEventArgs(Data);
                                TeamRename?.Invoke(teamRenameEventArgs);
                                break;
                            case "user_change":
                                UserChangeEventArgs userChangeEventArgs = new UserChangeEventArgs(Data);
                                UserChange?.Invoke(userChangeEventArgs);
                                break;
                            case "user_typing":
                                UserTypingEventArgs userTypingEventArgs = new UserTypingEventArgs(this, Data);
                                UserTyping?.Invoke(userTypingEventArgs);
                                break;
                            default: //null
                                break;
                        }
                    }
                    catch (AggregateException ex)
                    {
                        throw ex.InnerException;
                    }
                    catch (Exceptions.ServiceDisconnectedException ex)
                    {
                        throw ex;
                    }
                    catch (ThreadAbortException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    }
                }
            }
            catch (Exceptions.ServiceDisconnectedException)
            {
                //do nothing....normal for disconnected slack service
            }
            catch (ThreadAbortException)
            {
                //this is fine, client is being shutdown
            }
            catch (Exception)
            {

            }
        }


        private async Task<String> _readMessage()
        {
            try
            {
                ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[8192]);

                WebSocketReceiveResult result = null;

                while (true)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        do
                        {
                            result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                            ms.Write(buffer.Array, buffer.Offset, result.Count);
                        } while (!result.EndOfMessage);

                        ms.Seek(0, System.IO.SeekOrigin.Begin);

                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            using (var reader = new System.IO.StreamReader(ms, Encoding.UTF8))
                            {
                                // do stuff
                            }
                        }
                        Byte[] bytMessage = ms.ToArray();
                        return Encoding.ASCII.GetString(bytMessage);
                    }
                }
            }
            catch (WebSocketException ex)
            {
                _disconnect();
                throw new Exceptions.ServiceDisconnectedException(ex);
            }
            catch (Exception ex)
            {
                _disconnect();
                throw new Exceptions.ServiceDisconnectedException(ex);
            }
        }
    }
}
