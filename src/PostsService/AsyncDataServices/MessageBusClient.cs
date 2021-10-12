using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PostsService.Dtos;
using RabbitMQ.Client;

namespace PostsService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                
                Console.WriteLine(" ==> Connected to Message Bus");

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not connect to MessageBus: {ex.Message}");
            }
        }
        public void PublishNewPost(PostPublishedDto postPublishedDto)
        {
            //TODO
            var message = JsonSerializer.Serialize(postPublishedDto);

            if(_connection.IsOpen)
            {
                Console.WriteLine(" ==> RabbitMQ connection is open, Sending message ...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine(" ==> RabbitMQ connection is closed, Not sending message.");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            try
            {
                _channel.BasicPublish(exchange: "trigger",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body);
                Console.WriteLine(" ==> We hav sent message.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($" ==> Could not send message, {ex.Message}");
            }
        }

        private void Dispose()
        {
            Console.WriteLine(" ==> Message Bus Disposed");
            if(_connection.IsOpen)
            {
                _connection.Close();
                _channel.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine(" ==> RabbitMQ connection shutdown.");
        }
    }
}