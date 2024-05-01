using StackExchange.Redis;

ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync("localhost:6379");


ISubscriber subscriber = connectionMultiplexer.GetSubscriber();

await subscriber.SubscribeAsync("myChannel", (channel, message) =>
{
    Console.WriteLine(message);
});
Console.Read();