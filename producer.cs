using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Collections.Generic;


using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

class Producer
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Please provide the configuration file path as a command line argument");
        }
       
        Console.WriteLine("\r\nStart process\r\n");

        string schemaRegistryUrl = "https://confluent.cloud/environments/env-9kwydy/schema-registry/schemas/schema_test";
        using (var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = schemaRegistryUrl }))

        Console.WriteLine("start process");
        IConfiguration configuration = new ConfigurationBuilder()
            .AddIniFile(args[0])
            .SetValueSerializer(new AvroSerializer<GenericRecord>(schemaRegistry))
            .Build();


    var schema = (RecordSchema)RecordSchema.Parse(
                @"{
                    ""type"": ""event"",
                    ""name"": ""Sample"",
                    ""fields"": [
                        {""name"": ""Value"", ""type"": [""int""]}
                    ]
                  }"
            );
            
            
            
            
        const string topic = "classification";

        string eventSerialized = JsonSerializer.Serialize(GetMockMappedEvent());
    

        using (var producer = new ProducerBuilder<Null, string>(
            configuration.AsEnumerable()).Build())
        {

            producer.Produce(topic, new Message<Null, string> { Value=eventSerialized},
                    (deliveryReport) =>
                    {   
                        Console.WriteLine($"Message: {eventSerialized}");

                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"\r\nFailed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"\r\nProduced event to topic {topic}");
                        }
                    });

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"\r\nMessages were produced to topic {topic} at {DateTime.Now}");
        }
    }




   public static Event GetMockMappedEvent (){
    return new Event(){ 
            TraceId = "TraceId1",
            SpanId = "SpanId1",
            ParentSpanId = "ParentSpanId",
            SpanType = "",
            EventType = EventTypeEnum.EVT_ACK_DESTOCKING_BEGIN,
            EventId = Guid.NewGuid().ToString(),
            EventDetail = "",
            DomainName = "",
            Timestamp = DateTime.Now,
            Processor = new Processor(){ 
                Id = "",
                Type = "",
                BankCode = "",
                BranchCode = "",
                OfficeCode = "",
                Ip = "",
            }, 
            Batches = new List<Batch>(),
            Folds = new List<Fold>(),
            Documents = new List<Document>(),
            Sheets = new List<Sheet>(),
            Pages = new List<Page>(),
            Fields = new List<Field>(),
            };

            Batch batch = new Batch() { 
                Id = "",
                CustomerId = "",
                BusinessId = "",
                SiteId = "",
            };

            Document document = new Document() { 
                Id = "",
                Type = "",
                Title = "",
                PreviousType = "",
                FoldId = "",
            };

            Field field = new Field() { 
                Id = "",
                Label = "",
                Type = "",
                // Unknown Property : PreviousType 
                Value = "",
                // Unknown Property : PreviousValue 
                PageId = "",
            };

            Fold fold = new Fold() { 
                Id = "",
                BatchId = "",
            };

            Page page = new Page() { 
                Id = "",
                Type = "",
                // Unknown Property : PreviousType 
                SheetId = "",
            };

            Processor processor = new Processor() { 
                Id = "",
                Type = "",
                BankCode = "",
                BranchCode = "",
                OfficeCode = "",
                Ip = "",
            };

            Sheet sheet = new Sheet() { 
                Id = "",
                DocumentId = "",
        };
    }

}
