using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SqsWorkerServicePlayground
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAmazonSQS _sqs;

        public Worker(ILogger<Worker> logger, IAmazonSQS sqs)
        {
            _logger = logger;
            _sqs = sqs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var request = new ReceiveMessageRequest
                {
                    QueueUrl = "https://sqs.eu-west-2.amazonaws.com/CHANGE-ME", // TODO
                    MaxNumberOfMessages = 1
                };

                _logger.LogInformation("Starting receive...");

                var result = await _sqs.ReceiveMessageAsync(request);

                _logger.LogInformation("Receive completed.");
            }
        }
    }
}
