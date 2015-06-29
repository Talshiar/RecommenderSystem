using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SystemRecommenderApp.Models
{
    public class Summary
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }

    public class MutualFriends
    {
        [JsonProperty("data")]
        public List<object> Data { get; set; }
        [JsonProperty("summary")]
        public Summary Summary { get; set; }
    }

    public class MutualLikes : MutualFriends
    {

    }

    public class Context
    {

        [JsonProperty("mutual_friends")]
        public MutualFriends MutualFriends { get; set; }
        [JsonProperty("mutual_likes")]
        public MutualLikes MutualLikes { get; set; }

    }

    public class FacebookJson
    {
        [JsonProperty("context")]
        public Context Context { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
    public class Data
    {
        [JsonProperty("to")]
        public To To { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("updated_time")]
        public DateTime Updated_Time { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class To
    {
        [JsonProperty("data")]
        public List<object> Data { get; set; }
    }

}
