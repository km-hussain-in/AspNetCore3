using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DemoApp.Models
{
    [DataContract]
    public class Rating
    {
        [DataMember(Name = "from")]
        public string Name {get; set;}

        [DataMember(Name = "rating")]
        public int Rank {get; set;}
    }

    public class RatingModel
    {
        private HttpClient _client;

        public RatingModel(HttpClient client) => _client = client;

        public async Task<Rating> ReadRatingAsync(string name)
        {
            var response = await _client.GetAsync($"rest/feedbacks/secondary/{name}");
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<Rating>();
            return null;
        }

        public async Task<int> WriteRatingAsync(Rating info)
        {
            var response = await _client.PostAsJsonAsync("rest/feedbacks/secondary", info);
            return (int)response.StatusCode;
        }
    }
}
