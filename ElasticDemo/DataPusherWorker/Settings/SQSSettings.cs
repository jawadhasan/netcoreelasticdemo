namespace DataPusherWorker.Settings
{
    public class SQSSettings
    {
        public string AWSRegion { get; set; }
        public string QueueUrl { get; set; }
        public int ReceiveMessageWaitTimeInSeconds { get; set; }
        public int PollingIntervalInSeconds { get; set; }
        public int MaxNumberOfMessagesPerRequest { get; set; }
    }
}
