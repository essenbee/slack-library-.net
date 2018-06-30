using Newtonsoft.Json;
using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlackLibCore
{
    public class Pinger
    {
        private int _id = 1;
        private readonly WebSocket _webSocket;
        private readonly TimeSpan _keepAliveInterval;
        private readonly Task _pingTask;
        private readonly CancellationToken _cancellationToken;
        private readonly PingPongManager _mgr;

        public Pinger(WebSocket websocket, TimeSpan keepAliveInterval, PingPongManager mgr, CancellationToken token)
        {
            _webSocket = websocket;
            _keepAliveInterval = keepAliveInterval;
            _mgr = mgr;
            _cancellationToken = token;

            Task.Run(PingForever, _cancellationToken);
        }
        
        private async Task PingForever()
        {
            try
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(_keepAliveInterval, _cancellationToken);

                    if (_webSocket.State != WebSocketState.Open)
                    {
                        break;
                    }

                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        dynamic ping = new ExpandoObject();
                        ping.id = _id++;
                        ping.type = "ping";
                        var payload = JsonConvert.SerializeObject(ping);
                        var encoded = Encoding.UTF8.GetBytes(payload);
                        var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);
                        await _mgr.SendPing(buffer, _cancellationToken);
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
