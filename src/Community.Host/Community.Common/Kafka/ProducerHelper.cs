
using System.Text;

using RdKafka;

namespace Community.Common.Kafka
{
    public class ProducerHelper
    {
        
        public static bool Produce(string brokerList, string topicName, string payload)
        {
            using (var producer = new Producer(brokerList))
            using (var topic = producer.Topic(topicName))
            {
                var data = Encoding.UTF8.GetBytes(payload);
                var deliveryReport = topic.Produce(data).Result;
                return deliveryReport.Offset > 0;
            }
        }
    }
}
