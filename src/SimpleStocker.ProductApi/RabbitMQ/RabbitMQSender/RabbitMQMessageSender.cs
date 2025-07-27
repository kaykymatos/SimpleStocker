using RabbitMQ.Client;
using SimpleStocker.MessageBus;
using SimpleStocker.ProductApi.RabbitMQ.RabbitMQModels;
using System.Text;
using System.Text.Json;

namespace SimpleStocker.ProductApi.RabbitMQ.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(
                    exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
            };
            message.MessageCreated = DateTime.Now;
            string json = JsonSerializer.Serialize((ProductRabbitMQModel)message, options);
            byte[] body = Encoding.UTF8.GetBytes(json);
            return body;
        }
        private void CreateConnection()
        {
            try
            {
                ConnectionFactory factory = new()
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }


    }
}