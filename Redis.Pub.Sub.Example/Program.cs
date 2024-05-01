using StackExchange.Redis;

ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync("localhost:6379");


ISubscriber subscriber =  connectionMultiplexer.GetSubscriber();

while (true)
{
    Console.WriteLine("Message:");
    var message = Console.ReadLine();
    await subscriber.PublishAsync("myChannel", message);
}