using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoApp.Models
{
    public class Rating
    {
		[JsonPropertyName("from")]
        public string Name {get; set;}

		[JsonPropertyName("rating")]
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
			{
                string content = await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<Rating>(content);
			}
            return null;
        }

        public async Task<int> WriteRatingAsync(Rating info)
        {
			var content = JsonSerializer.Serialize(info); 
            var response = await _client.PostAsync("rest/feedbacks/secondary", new StringContent(content, Encoding.UTF8, "application/json"));
            return (int)response.StatusCode;
        }
    }
}

