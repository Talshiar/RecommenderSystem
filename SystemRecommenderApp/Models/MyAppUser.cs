using Microsoft.AspNet.Facebook;
using Newtonsoft.Json;

// Add any fields you want to be saved for each user and specify the field name in the JSON coming back from Facebook
// http://go.microsoft.com/fwlink/?LinkId=301877

namespace SystemRecommenderApp.Models
{
    public class MyAppUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [FacebookFieldModifier("limit(100)")]
        public FacebookGroupConnection<Like> Likes { get; set; }

        [JsonProperty("picture")]
        [FacebookFieldModifier("type(large)")]
        public FacebookConnection<FacebookPicture> ProfilePicture { get; set; }

        [FacebookFieldModifier("limit(8)")]
        public FacebookGroupConnection<MyAppUserFriend> Friends { get; set; }

        [FacebookFieldModifier("limit(16)")]
        public FacebookGroupConnection<FacebookPhoto> Photos { get; set; }
        public FacebookGroupConnection<Movie> Movies { get; set; }
        public FacebookGroupConnection<Book> Books { get; set; }
        public FacebookGroupConnection<TV> Television { get; set; }
        public FacebookGroupConnection<Bands> Music { get; set; } 

        public class Bands
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Link { get; set; }
        }
        public class Movie
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Link { get; set; }
        }
        public class Like
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public string Id { get; set; }
            public string Link { get; set; }
        }
        public class Book
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Link { get; set; }
        }
        public class TV
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Link { get; set; }
        }
    }
}
