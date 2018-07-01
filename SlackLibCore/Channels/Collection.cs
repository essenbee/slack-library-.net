using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace SlackLibCore.Channels
{


    public class Collection
    {


        private Client _client;


        public Collection(Client Client)
        {
            _client = Client;
        }


        public ListResponse List()
        {
            //https://api.slack.com/methods/channels.list
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.list?token=" + _client.APIKey);
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not list channels.", ex);
            }
            _client.CheckForError(Response);
            return new ListResponse(_client.MetaData, Response);
        }


        public ArchiveResponse Archive(String name)
        {
            //https://api.slack.com/methods/channels.archive
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.archive?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(name));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not archive channels.", ex);
            }
            _client.CheckForError(Response);
            return new ArchiveResponse();
        }


        public CreateResponse Create(String name)
        {
            //https://api.slack.com/methods/channels.create
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.create?token=" + _client.APIKey + "&name=" + System.Web.HttpUtility.UrlEncode(name));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not create channel.", ex);
            }
            _client.CheckForError(Response);
            return new CreateResponse(Response);
        }


        public HistoryResponse History(HistoryRequestArgs args)
        {
            //https://api.slack.com/methods/channels.history
            dynamic Response;
            try
            {
                String strGet =
                    "token=" + _client.APIKey +
                    "&channel=" + System.Web.HttpUtility.UrlEncode(args.channel) +
                    "&latest=" + args.latest.ToString() +
                    "&oldest=" + args.oldest.ToString() +
                    "&inclusive=" + ((args.inclusive) ? 1 : 0) +
                    "&count=" + args.count +
                    "&unreads=" + ((args.unreads) ? 1 : 0);
                String strResponse = _client.APIRequest("https://slack.com/api/channels.history?" + strGet);
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not create channel.", ex);
            }
            _client.CheckForError(Response);
            return new HistoryResponse(_client, args, Response);
        }


        public InfoResponse Info(String id)
        {
            //https://api.slack.com/methods/channels.info
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.info?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(id));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get channel info.", ex);
            }
            _client.CheckForError(Response);
            return new InfoResponse(Response);
        }


        public InviteResponse Invite(String channelID, String userID)
        {
            //https://api.slack.com/methods/channels.invite
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.invite?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(channelID) + "&user=" + System.Web.HttpUtility.UrlEncode(userID));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not invite user to channel.", ex);
            }
            _client.CheckForError(Response);
            return new InviteResponse(Response);
        }


        public KickResponse Kick(String channelID, String userID)
        {
            //https://api.slack.com/methods/channels.kick
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.kick?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(channelID) + "&user=" + System.Web.HttpUtility.UrlEncode(userID));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not kick user channel.", ex);
            }
            _client.CheckForError(Response);
            return new KickResponse();
        }


        public LeaveResponse Leave(String channelID)
        {
            //https://api.slack.com/methods/channels.leave
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.leave?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(channelID));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not leave channel.", ex);
            }
            _client.CheckForError(Response);
            return new LeaveResponse();
        }


        public JoinResponse Join(String name)
        {
            //https://api.slack.com/methods/channels.join
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.join?token=" + _client.APIKey + "&name=" + System.Web.HttpUtility.UrlEncode(name));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not perform auth test.", ex);
            }
            _client.CheckForError(Response);
            return new JoinResponse(Response);
        }


        public MarkResponse Mark(String channelID, TimeStamp ts)
        {
            //https://api.slack.com/methods/channels.mark
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.mark?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(channelID) + "&ts=" + System.Web.HttpUtility.UrlEncode(ts.ToString()));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not mark channel.", ex);
            }
            _client.CheckForError(Response);
            return new MarkResponse();
        }


        public RenameResponse Rename(String channelID, String newName)
        {
            //https://api.slack.com/methods/channels.rename
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.rename?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(channelID) + "&name=" + System.Web.HttpUtility.UrlEncode(newName));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not rename channel.", ex);
            }
            _client.CheckForError(Response);
            return new RenameResponse(Response);
        }


        public SetPurposeResponse SetPurpose(String channelID, String newPurpose)
        {
            //https://api.slack.com/methods/channels.setPurpose
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.setPurpose?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(channelID) + "&purpose=" + System.Web.HttpUtility.UrlEncode(newPurpose));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not rename channel.", ex);
            }
            _client.CheckForError(Response);
            return new SetPurposeResponse(Response);
        }


        public SetTopicResponse SetTopic(String channelID, String newTopic)
        {
            //https://api.slack.com/methods/channels.setTopic
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.setTopic?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(channelID) + "&topic=" + System.Web.HttpUtility.UrlEncode(newTopic));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not set channel topic.", ex);
            }
            _client.CheckForError(Response);
            return new SetTopicResponse(Response);
        }


        public UnarchiveResponse Unarchive(String id)
        {
            //https://api.slack.com/methods/channels.archive
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/channels.unarchive?token=" + _client.APIKey + "&channel=" + System.Web.HttpUtility.UrlEncode(id));
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not unarchive channels.", ex);
            }
            _client.CheckForError(Response);
            return new UnarchiveResponse();
        }


    }


}
