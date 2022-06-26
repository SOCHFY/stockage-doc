using Confluent.Kafka;
using System;
using System.Diagnostics;
using Classes;
using Microsoft.Extensions.Configuration;

class Producer
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Please provide the configuration file path as a command line argument");
        }
       
        Console.WriteLine("start process");
        IConfiguration configuration = new ConfigurationBuilder()
            .AddIniFile(args[0])
            .Build();

        const string topic = "classification";

        Event myEvent = new Event();
        Guid myuuid = Guid.NewGuid();

        myEvent.EventType = EventType.EVT_ACK_DESTOCKING_BEGIN;
        myEvent.EventId = myuuid.ToString();

        using (var producer = new ProducerBuilder<Null, string>(
            configuration.AsEnumerable()).Build())
        {
            producer.Produce(topic, new Message<Null, string> { Value=myEvent.ToString()},
                    (deliveryReport) =>
                    {   
                        Console.WriteLine($"EventType : {myEvent.EventType}");
                        Console.WriteLine($"EventType : {myEvent.EventId}");
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}");
                        }
                    });
            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"messages were produced to topic {topic}");
        }
    }

}
