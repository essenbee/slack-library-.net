using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlackLibCore
{
    public class Pinger
    {
        public string Payload { get; set; } = string.Empty;
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
        public TimeSpan KeepAliveInterval = new TimeSpan(0, 1, 0);

        private int _id = 1;
        private readonly WebSocket _webSocket;

        public Pinger(WebSocket websocket, TimeSpan keepAliveInterval, CancellationToken token)
        {
            _webSocket = websocket;

            KeepAliveInterval = keepAliveInterval;
            CancellationToken = token;

            Task.Run(PingForever, CancellationToken);
        }
        
        private async Task PingForever()
        {
            try
            {
                while (!CancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(KeepAliveInterval, CancellationToken);

                    if (_webSocket.State != WebSocketState.Open)
                    {
                        break;
                    }

                    if (!CancellationToken.IsCancellationRequested)
                    {
                        if (string.IsNullOrWhiteSpace(Payload))
                        {
                            dynamic ping = new ExpandoObject();
                            ping.id = _id++;
                            ping.type = "ping";
                            Payload = JsonConvert.SerializeObject(ping);
                        }

                        var encoded = Encoding.UTF8.GetBytes(Payload);
                        var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);

                        await _webSocket.SendAsync(buffer,WebSocketMessageType.Text, true, CancellationToken);

                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tPing!"); ;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // normal, do nothing
            }
        }
    }
}
