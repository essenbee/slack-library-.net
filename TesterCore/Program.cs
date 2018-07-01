using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SlackLibCore;

namespace TesterCore
{
    class Program
    {
        private static Client client;
        private static Int32 connectionFailures = 0;
        private static Boolean shutdown = false;

        public static IConfiguration Configuration { get; set; }

        //main entry point for application
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var apiKey = Configuration["SlackApiKey"];

            //create a new slack client
            client = new Client(apiKey);

            // Subscribe to any of the events that may interest you
            client.ServiceConnected += new Client.ServiceConnectedEventHandler(client_ServiceConnected);
            client.ServiceConnectionFailed += new Client.ServiceConnectionFailedEventHandler(client_ServiceDisconnected_ServiceConnectionFailure);
            client.ServiceDisconnected += new Client.ServiceDisconnectedEventHandler(client_ServiceDisconnected_ServiceConnectionFailure);

            client.Hello += new Client.HelloEventHandler(client_Hello);
            client.DataReceived += new Client.DataReceivedEventHandler(client_DataReceived);
            client.UserTyping += new Client.UserTypingEventHandler(client_UserTyping);
            client.Message += new Client.MessageEventHandler(client_Message);
            client.MesssageEdit += new Client.MessageEditEventHandler(client_MessageEdit);

            client.CommandReceived += new Client.CommandEventHandler(client_ProcessCommand);

            client.DoNotDisturbUpdatedUser += new Client.DoNotDistrubUpdatedUserEventHandler(client_DoNotDisturbUpdatedUser);

            //connect to the slack service
            client.Connect();

            var general = Configuration["Channels:General"];

            Chat.PostMessageArguments msgArgs = new Chat.PostMessageArguments
            {
                channel = general,
                text = "CoreBot entered the channel at: " + DateTime.Now
            };

            client.Chat.PostMessage(msgArgs);

            //simply hold application open until user presses enter
            Console.WriteLine("Press Enter to Terminate.");

            Console.ReadLine();

            //disconnect from slack service
            shutdown = true;
            client.Disconnect();
        }

        #region Slack Events

        private static void client_ServiceConnected()
        {
            connectionFailures = 0;
            Console.WriteLine("Connected to slack service.");
        }

        private static void client_ServiceDisconnected_ServiceConnectionFailure()
        {
            if (shutdown)
            {   //don't restart
                return;
            }
            try
            {
                connectionFailures++;
                if (connectionFailures < 13)
                {   //wait 5 seconds and try to reconnect
                    System.Threading.Thread.Sleep(5 * 1000);
                }
                else
                {   //wait 1 minute and try to reconnect
                    System.Threading.Thread.Sleep(60 * 1000);
                }
                Console.WriteLine("Attempting to reconnect to slack service. Attempt " + connectionFailures);
                client.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not handle service disconnected.\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private static void client_Hello(HelloEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tHello");
        }

        private static void client_ProcessCommand(CommandEventArgs e)
        {
            var cmd = e?.Command ?? "<< none >>";
            var args = e?.ArgsAsString ?? string.Empty;
            var channel = e?.Channel ?? string.Empty;
            Console.WriteLine($">> Channel [{channel}] ...");
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tCommand.\t\t[{e.UserName}] invoked [{cmd}] with args [{args}]");
        }

        private static void client_DoNotDisturbUpdatedUser(DoNotDisturbUpdatedUserEventArgs e)
        {
            if (e.dnd_status.dnd_enabled)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\t\t" + e.UserInfo.name + " " + e.dnd_status.next_dnd_start_ts.Date + " " + e.dnd_status.next_dnd_end_ts.Date);
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\t\t" + e.UserInfo.name + " DND disabled");
            }
        }

        private static void client_DataReceived(String data)
        {
            //Console.WriteLine("Received: " + data);
        }

        private static void client_UserTyping(UserTypingEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tUser Typing.\t\t[" + e.UserInfo.name + "] [" + e.channel + "]");
        }

        private static void client_Message(MessageEventArgs e)
        {
            var user = e?.UserInfo?.name ?? string.Empty;
            var text = e?.text ?? "<< none >>";

            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
        }

        private static void client_MessageEdit(MessageEditEventArgs e)
        {
            var user = e?.UserInfo?.name ?? string.Empty;
            var text = e?.message?.text ?? "<< deleted >>";

            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
        }

        #endregion

        //This example method processes commands from users and optionally sends a message back to the user
        private static void Process_Message(String userName, String channel, String text)
        {
            const Int32 MAX_NEWS_ITEMS = 10;
            Chat.PostMessageArguments args = new Chat.PostMessageArguments();
            args.channel = channel;
            text = text.Trim().ToLower();
            switch (text)
            {
                case "time":
                    args.text = "Server time is: " + DateTime.Now.ToString();
                    client.Chat.PostMessage(args);
                    break;
                case "local news":
                    args.text = "";
                    System.Net.WebClient wcLocalNews = new System.Net.WebClient();
                    String strLocalNewsXML = wcLocalNews.DownloadString("http://www.inkfreenews.com/feed/");
                    System.Xml.XmlDocument xmlLocalNews = new System.Xml.XmlDocument();
                    xmlLocalNews.LoadXml(strLocalNewsXML);
                    Int32 intLocalNewsRemaining = MAX_NEWS_ITEMS;
                    foreach (System.Xml.XmlNode xmlNode in xmlLocalNews.SelectNodes("rss/channel/item"))
                    {
                        intLocalNewsRemaining--;
                        if (intLocalNewsRemaining == 0)
                        {
                            break;
                        }
                        args.text +=
                            xmlNode.SelectSingleNode("title").InnerText + "\r\n\t" +
                            xmlNode.SelectSingleNode("link").InnerText + "\r\n";
                    }
                    client.Chat.PostMessage(args);
                    break;
                case "news":
                    args.text = "";
                    System.Net.WebClient wcReutersTechNews = new System.Net.WebClient();
                    String strReutersTechNewsXML = wcReutersTechNews.DownloadString("http://feeds.reuters.com/reuters/technologyNews");
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(strReutersTechNewsXML);
                    Int32 intReutersTechNewsRemaining = MAX_NEWS_ITEMS;
                    foreach (System.Xml.XmlNode xmlNode in xmlDoc.SelectNodes("rss/channel/item"))
                    {
                        intReutersTechNewsRemaining--;
                        if (intReutersTechNewsRemaining == 0)
                        {
                            break;
                        }
                        args.text +=
                            xmlNode.SelectSingleNode("title").InnerText + "\r\n\t" +
                            xmlNode.SelectSingleNode("link").InnerText + "\r\n";
                    }
                    client.Chat.PostMessage(args);
                    break;
            }
        }
    }
}
