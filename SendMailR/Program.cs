using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "admin",
    Password = "admin"
};

try
{
    var connection = factory.CreateConnection();
    using
    var channel = connection.CreateModel();
    channel.QueueDeclare("ejemplo", exclusive: false);
    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, EventArgs) =>
    {
        var body = EventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine("Mensaje recibido: " + message);
    };
    channel.BasicConsume("ejemplo", true, consumer);
    Console.ReadKey();

}
catch (Exception ex)
{
    Console.WriteLine("Error leyendo el mensaje " + ex.Message);
}