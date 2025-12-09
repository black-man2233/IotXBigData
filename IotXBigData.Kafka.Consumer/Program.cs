using Confluent.Kafka;

namespace Consumer;
class Program
{
  static string url = "kafka:9092";
    static void Main()
    {
        // var db = new LocalDatabase();

        var config = new ConsumerConfig
        {
            BootstrapServers = "192.168.39.55",
            GroupId = "console-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe("Kevin_Zilas_Mathias");

        Console.WriteLine("Consumer running...");

        while (true)
        {
            var cr = consumer.Consume();
            Console.WriteLine($"Received: {cr.Message.Value}");
        }
    }
}
