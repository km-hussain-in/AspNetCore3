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
        public int Rating {get;} = 5;
    }

    public class FeedbackModel
    {
        private HttpClient _client;

        public FeedbackModel(HttpClient client) => _client = client;

        public async Task<Feedback> ReadFeedbackAsync(string name)
        {
            var response = await _client.GetAsync($"rest/feedbacks/secondary/{name}");
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<Feedback>();
            return null;
        }

        public async Task<int> WriteFeedbackAsync(Feedback info)
        {
            var response = await _client.PostAsJsonAsync("rest/feedbacks/secondary", info);
            return (int)response.StatusCode;
        }
    }
}
