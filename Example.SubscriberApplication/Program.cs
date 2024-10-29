using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Auth.AccessControlPolicy;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Example.SubscriberApplication
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

            using (var sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1)) //RegionEndpoint.USEast1 is a region name
            using (var snsClient = new AmazonSimpleNotificationServiceClient(credentials,RegionEndpoint.USEast1))
            {
                var subscriber = new Subscriber(sqsClient, snsClient, "topicName", "queueName");

                Console.WriteLine("Listening...");

                await subscriber.ListenAsync(message => {

                    if (message != null)
                    {
                        dynamic data = JObject.Parse(message.Body);
                       // Console.WriteLine(data.Message);
                        Console.WriteLine("Received message(only message): " + data.Message);
                        Console.WriteLine("Received message (Total Info): " + message.Body);
                    }
                });
            }
        }
    }
}