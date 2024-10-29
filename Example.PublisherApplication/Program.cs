using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.Runtime;
using Amazon;

namespace Example.PublisherApplication
{
    class Program
    {
        static void Main()
        {
            Run().GetAwaiter().GetResult();
        }

        private static async Task Run()
        {
            var credentials = new BasicAWSCredentials("accessKey", "secretKey");

            using (var snsClient = new AmazonSimpleNotificationServiceClient(credentials, RegionEndpoint.USEast1)) //RegionEndpoint.USEast1 is a region name
            {
                var publisher = new Publisher(snsClient, "topicName");

                while (true)
                {
                    Console.WriteLine("Type a message to send:");
                    var message = Console.ReadLine();

                    await publisher.PublishAsync(message);
                }
            }
        }
    }
}