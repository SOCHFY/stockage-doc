# stockage-doc
dotnet run --project producer.csproj $(pwd)/getting-started.properties



1- Ajouter dans votre projet les dépendances (les versions sont à confirmer avec l'équipe système et réseau) : 

  <ItemGroup>
    <PackageReference Include="Confluent.Kafka" Version="1.8.2" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="6.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="6.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Ini" Version="6.0.0" />
  </ItemGroup>

2- Créer un fichier properties dans le projet : 
bootstrap.servers=
security.protocol=SASL_SSL
sasl.mechanisms=PLAIN
sasl.username=< CLUSTER API KEY >
sasl.password=< CLUSTER API SECRET >

3-Dans la classe de production:

		1ere étape:
    
    Instancier la classe ConfigurationBuilder qui nous permettra de lire les variables du fichier properties (dans mon cas je recuperer les variables du fichier de paramétrage passé en paramètre) :
	   IConfiguration configuration = new ConfigurationBuilder()
	            .AddIniFile(args[0])
	            .Build();
              
		2eme étape: 
    Construire l'évènement 
		
    3eme étape: 
      Produire le message dans le topic : 
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
			












![image](https://user-images.githubusercontent.com/108058805/175830315-b3a60416-e19c-4b59-8312-1ff6c7d86543.png)





https://developer.confluent.io/get-started/dotnet/?_ga=2.112325636.127534075.1655996156-1996032525.1655276490&_gac=1.125679992.1655996156.Cj0KCQjwntCVBhDdARIsAMEwACnlObCjiC6q64zgDOwWvCdtXsvcGeZE9KjNRHswI5pDA6vFqj1gdFEaAhHDEALw_wcB#produce-events
