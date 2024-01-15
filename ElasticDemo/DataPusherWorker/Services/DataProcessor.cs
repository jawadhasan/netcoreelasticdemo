﻿using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IotFleet;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DataPusherWorker.Services
{
    //worker's services


    public class WebSocketProcessor : IDataProcessor
    {
        private readonly Uri _socketServer;

        private readonly ClientWebSocket _socket = new();

        public WebSocketProcessor(string url)
        {
            _socketServer = new Uri(url);
        }

        public async Task Setup()
        {
            try
            {
                await _socket.ConnectAsync(_socketServer, CancellationToken.None);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - {ex.Message}");
            }
        }

        public async Task Process(RideData rideData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(rideData, Formatting.None, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                await Send(json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR {e.Message}");
            }
        }

        private async Task Send(string data)
        {

            await _socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true,
                CancellationToken.None);
        }
    }


    public class SampleWebsocketService
    {
        private static readonly string Connection = "connectionstring";

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                using (var socket = new ClientWebSocket())
                    try
                    {
                        await socket.ConnectAsync(new Uri(Connection), stoppingToken);

                        await Send(socket, "data", stoppingToken);
                        await Receive(socket, stoppingToken);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR - {ex.Message}");
                    }
        }

        private async Task Send(ClientWebSocket socket, string data, CancellationToken stoppingToken) =>
            await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true, stoppingToken);

        private async Task Receive(ClientWebSocket socket, CancellationToken stoppingToken)
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            while (!stoppingToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result;
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await socket.ReceiveAsync(buffer, stoppingToken);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                        Console.WriteLine(await reader.ReadToEndAsync());
                }
            };
        }
    }

}
