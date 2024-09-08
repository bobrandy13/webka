using System.Net;
using Kafka_for_web.DataAccess;
using RestSharp;

namespace Tests;

public class ApiTest : IDisposable
{
    private const string baseURL = "http://localhost:9092/api";
    private RestClient client;

    [SetUp]
    public void Setup()
    {
        // Should verify that the api is open 
        client = new RestClient(baseURL);

        var testingProjectDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Kafka_for_web");
        
        Directory.SetCurrentDirectory(testingProjectDirectory);
    }

    [TearDown]
    public void TearDown()
    {
        Dispose();
    }

    public void Dispose()
    {
        client?.Dispose();
    }

    // Testing that it can consume a message within the 5-second time out period 
    [Test]
    public void CanFetchConsumer()
    {
        var restRequest = new RestRequest("/consumer/6");

        var restResponse = client.Execute(restRequest);
        Console.WriteLine(restResponse.Content);

        Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void CanFetchMessage()
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        var req = new RestRequest("/Consumer/subscribe", Method.Post)
            .AddParameter("consumerName", "consumer_2", ParameterType.QueryString)
            .AddParameter("topicName", "Buyers", ParameterType.QueryString)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(new
            {
                fromBeginning = true,
                numMessages = 0
            });

        var res = client.Execute(req);

        Console.WriteLine("Status Code: " + res.StatusCode);
        Console.WriteLine("Response Content: " + res.Content);
        Console.WriteLine("Error Message: " + res.ErrorMessage);
        Console.WriteLine("Error Exception: " + res.ErrorException);

        Assert.That(res.StatusCode, Is.AnyOf(HttpStatusCode.OK, HttpStatusCode.NoContent, HttpStatusCode.Accepted));
    }

    [Test]
    public void CanSendMessage()
    {
        var req = new RestRequest("/Producer/message", Method.Post)
            .AddJsonBody(new
            {
                value = "New New Message Incoming",
                topicId = 35,
                producerId = 2
            });

        var res = client.Execute(req);

        Console.WriteLine("Status Code: " + res.StatusCode);
        Console.WriteLine("Response Content: " + res.Content);
        Console.WriteLine("Error Message: " + res.ErrorMessage);
        Console.WriteLine("Error Exception: " + res.ErrorException);

        Assert.Pass();
    }

    [Test]
    public void CanMassSendMessages()
    {
        // test topic Direcotry
        const string logDir = "logs/EccomerceStore/test/log.txt";

        const int numMessagesToSend = 10320;

        var lineLengthBeforeSend = Logger.GetSize(logDir);

        for (var i = 0; i < numMessagesToSend; i++)
        {
            var req = new RestRequest("/Producer/message", Method.Post)
                .AddJsonBody(new
                {
                    value = "Message: " + i.ToString(),
                    topicId = 36,
                    producerId = 2
                });

            var res = client.Execute(req);

            // Console.WriteLine("Status Code: " + res.StatusCode);
            // Console.WriteLine("Response Content: " + res.Content);
            // Console.WriteLine("Error Message: " + res.ErrorMessage);
            // Console.WriteLine("Error Exception: " + res.ErrorException);
        }

        var lineLengthAfterSend = Logger.GetSize(logDir);

        // check the length that it has increased by the number of messages sent
        Assert.That(lineLengthAfterSend, Is.EqualTo(lineLengthBeforeSend + numMessagesToSend));
    }

    [Test]
    public void CanMassReadMessages()
    {
        Assert.Pass();
    }

    [Test]
    public void CanReadMessagesInGroups()
    {
        Assert.Pass();
    }

    [Test]
    public async Task CanSendMessageInMiddleOfPolling()
    {
        // create the test topic
        var createTopicReq = new RestRequest("/topic", Method.Post)
            .AddParameter("topicName", "test_topic", ParameterType.QueryString);

        var createTopicRes = client.Execute(createTopicReq);

        Console.WriteLine(createTopicRes.Content);

        var consumerRequest = new RestRequest("/consumer/subscribe", Method.Post)
            .AddParameter("consumerName", "test_consumer", ParameterType.QueryString)
            .AddParameter("topicName", "test_topic", ParameterType.QueryString)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(new
            {
                fromBeginning = true,
                numMessages = 0
            });

        var consumerRes = client.ExecuteAsync(consumerRequest);

        // since there are no new messages, then we can send a message after 2 seconds
        await Task.Delay(2000);

        var producerRequest = new RestRequest("/Producer/message", Method.Post)
            .AddJsonBody(new
            {
                value = "ABCDEF",
                topicId = 35,
                producerId = 2
            });

        var producerRes = client.ExecuteAsync(producerRequest);

        // Contains new message
        Assert.Pass();
    }
}