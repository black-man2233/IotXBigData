using Confluent.Kafka;
using IotXBigData.Shared.Models;
using Newtonsoft.Json;
class Program
{
    static async Task Main()
    {

        string topic = "orders";
        string key = "CsharpProject";

        var config = new ConsumerConfig
        {
            BootstrapServers = "191.168.39.55",
            // GroupId = "M_Z_K",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var producer = new ProducerBuilder<string, string>(config).Build();
        var r = new Random();
        while (true)
         {
            var order = new OrdersDTO()
            {
                order_id = Guid.NewGuid().ToString(),
                customer_id = "1",
                ammount = r.Next(0, 1000),
                order_ts = DateTime.UtcNow
            };       
            
            string message = JsonConvert.SerializeObject(order);

            await producer.ProduceAsync(
                topic,
                new Message<string, string> { Key = key, Value = message }
            );

            Console.WriteLine($"Produced Message {message}");
            await Task.Delay(500);

         
            Thread.Sleep(1 * 1000);
            // producer.Flush(TimeSpan.FromSeconds(5));
         } 
    }
}