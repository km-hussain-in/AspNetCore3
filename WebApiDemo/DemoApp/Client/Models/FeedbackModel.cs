using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DemoApp.Client.Models
{
    [DataContract]
    public class Feedback
    {
        [DataMember(Name = "name")]
        public string Name {get; set;}

        [DataMember(Name = "comment")]
        public string Comment {get; set;}

        [DataMember(Name = "rating")]
        public int Rating {get; set;}
    }

    public class FeedbackModel
    {
        private HttpClient _client;

        public FeedbackModel(HttpClient client) => _client = client;

        public async Task<IEnumerable<Feedback>> ReadFeedbacksAsync()
        {
            var response = await _client.GetAsync("rest/feedbacks");
            return await response.Content.ReadAsAsync<IEnumerable<Feedback>>();
        }

        public async Task<string> WriteFeedbackAsync(Feedback info)
        {
            var response = await _client.PostAsJsonAsync("rest/feedbacks", info);
            return await response.Content.ReadAsStringAsync();
        }
    }
}