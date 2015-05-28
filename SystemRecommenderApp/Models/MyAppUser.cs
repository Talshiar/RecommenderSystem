using Microsoft.AspNet.Facebook;
using Newtonsoft.Json;
using System.Collections.Generic;

// Add any fields you want to be saved for each user and specify the field name in the JSON coming back from Facebook
// http://go.microsoft.com/fwlink/?LinkId=301877

namespace SystemRecommenderApp.Models
{
    public class MyAppUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        private string maxFactorFriend;

        public void SetFriend(string friend)
        {
            maxFactorFriend = friend;
        }
        public string GetFriend()
        {
            return maxFactorFriend;
        }

        private Dictionary<string, int> FriendsFactor = new Dictionary<string, int>();

        public void SetFactor(string key, int value)
        {
            if (FriendsFactor.ContainsKey(key))
            {
                FriendsFactor[key] = value;
            }
            else
            {
                FriendsFactor.Add(key, value);
            }
        }

        public int GetFactor(string key)
        {
            int result = 0;

            if (FriendsFactor.ContainsKey(key))
            {
                result = FriendsFactor[key];
            }

            return result;
        }
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

    }
}
