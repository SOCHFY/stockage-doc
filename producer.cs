using Confluent.Kafka;
using System;
using System.Text.Json;

using Avro;
using Avro.Generic;

using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic; 
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class Producer
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Please provide the configuration file path as a command line argument");
        }
       
        Console.WriteLine("\r\nStart process\r\n");

        string schemaRegistryUrl = "https://confluent.cloud/environments/env-9kwydy/schema-registry/schemas/schema_test";
        var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = schemaRegistryUrl });

        Console.WriteLine("start process");
        IConfiguration configuration = new ConfigurationBuilder()
            .AddIniFile(args[0])
            .Build();


            
            
            
            
        const string topic = "classification";

        string eventSerialized = JsonSerializer.Serialize(GetMockMappedEvent());
    
        var bootstrapServers = "pkc-4nmjv.francecentral.azure.confluent.cloud:9092";
        var security="SASL_SSL";

        var config = new ProducerConfig {
        BootstrapServers = bootstrapServers,
        SecurityProtocol = SecurityProtocol.SaslSsl,
        SaslMechanism = SaslMechanism.ScramSha256,
        SaslUsername = "YGCMMKHH7L43DF5P",  
        SaslPassword = "ZVw0FEFWEKQbjeGstBqvillXJrvbdZprR1TLvWou39NzrWYimfHIv/88pnAz/oLM",
          
        };

        using (var producer = new ProducerBuilder<string, GenericRecord>(configuration.AsEnumerable()).SetValueSerializer(new AvroSerializer<GenericRecord>(schemaRegistry)).Build())
        {
        var logLevelSchema = (Avro.EnumSchema)Avro.Schema.Parse(
                    File.ReadAllText("LogLevel.asvc"));

                var logMessageSchema = (Avro.RecordSchema)Avro.Schema
                    .Parse(File.ReadAllText("LogMessage.V1.asvc")
                        .Replace(
                            "MessageTypes.LogLevel", 
                            File.ReadAllText("LogLevel.asvc")));

                var record = new GenericRecord(logMessageSchema);
                record.Add("IPS", "Souka");
                record.Add("Message", "a test log message");
                record.Add("Severity", new GenericEnum(logLevelSchema, "Error"));

          /*  producer.Produce(topic, new Message<Null, GenericRecord> { Value=eventSerialized},
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
            Console.WriteLine($"\r\nMessages were produced to topic {topic} at {DateTime.Now}");*/
              producer
                    .ProduceAsync("log-messages", new Message<string, GenericRecord> { Key ="A" ,Value = record})
                    .ContinueWith(task => Console.WriteLine(
                        task.IsFaulted
                            ? $"error producing message: {task.Exception.Message}"
                            : $"produced to: {task.Result.TopicPartitionOffset}"));
               Console.WriteLine($"Message: {record}");
               Console.WriteLine($"\r\nProduced event to topic {topic}");
                producer.Flush(TimeSpan.FromSeconds(30));
        }
    }




   public static Event GetMockMappedEvent (){
    return new Event(){ 
            TraceId = "TraceId1",
            SpanId = "SpanId1",
            ParentSpanId = "ParentSpanId",
            SpanType = "",
            EventType = EventType.EVT_ACK_DESTOCKING_BEGIN,
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
