﻿using Microsoft.AspNet.Facebook;
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

        [JsonProperty("picture")] // This renames the property to picture.
        [FacebookFieldModifier("type(large)")] // This sets the picture size to large.
        public FacebookConnection<FacebookPicture> ProfilePicture { get; set; }

        [FacebookFieldModifier("limit(8)")] // This sets the size of the friend list to 8, remove it to get all friends.
        public FacebookGroupConnection<MyAppUserFriend> Friends { get; set; }

        [FacebookFieldModifier("limit(16)")] // This sets the size of the photo list to 16, remove it to get all photos.
        public FacebookGroupConnection<FacebookPhoto> Photos { get; set; }
        public FacebookGroupConnection<Movies> Movies { get; set; }

        public class Movies
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }

        public class Like
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public string Id { get; set; }


        }
    }
}
