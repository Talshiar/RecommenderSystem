using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Facebook;
using Microsoft.AspNet.Facebook.Client;
using SystemRecommenderApp.Models;
using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SystemRecommenderApp.Controllers
{
    public class HomeController : Controller
    {
        [FacebookAuthorize("email", "user_photos", "user_likes", "user_friends", "user_actions.movies", "user_actions.music", "user_actions.books", "read_mailbox", "user_posts")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {  
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();
                var client = new Facebook.FacebookClient(context.Client.AccessToken);
                double maxFactor = 0;
                //slike
                JsonObject data = (JsonObject)client.Get("me/photos?fields=tags{name}&limit=300");
                string dataString = data.ToString();
                dynamic dynData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(dataString);
                string id;
                foreach (var dat in dynData.data)
                {
                    foreach (var outer in dat.tags.data)
                    {
                        id = outer.id;
                        foreach (var f in user.Friends.Data)
                        {
                            if (id == f.Id) user.SetFactor(f.Id, 1);
                        }
                    }

                }
                //postovi
                data = (JsonObject)client.Get("me/feed?fields=from{id},to{id}");
                dataString = data.ToString();
                dynData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(dataString);
                List<string> idList = new List<string>();                
                foreach (var post in dynData.data)
                {
                    id = post.from.id;
                    idList.Add(id);
                    if (post.to != null)
                    {
                        foreach (var d in post.to.data)
                        {
                            id = d.id;
                            idList.Add(id);
                        }
                    }

                    foreach (var f in user.Friends.Data)
                    {
                        if (idList.Contains(f.Id)) user.SetFactor(f.Id, 0.5);
                    }

                    idList.Clear();
                }


                foreach (var f in user.Friends.Data)
                {
                    //zajednicki prijatelji
                    data = (JsonObject)client.Get("/" + f.Id + "?fields=context.fields(mutual_friends)");
                    dataString = data.ToString();
                    var jobject = JObject.Parse(dataString);
                    var facebookJson = jobject.ToObject<FacebookJson>();
                    int mFriends = facebookJson.Context.MutualFriends.Summary.TotalCount;
                    
                    //zajednicki likeovi
                    data = (JsonObject)client.Get("/" + f.Id + "?fields=context.fields(mutual_likes)");
                    dataString = data.ToString();
                    jobject = JObject.Parse(dataString);
                    facebookJson = jobject.ToObject<FacebookJson>();
                    int mLikes = facebookJson.Context.MutualLikes.Summary.TotalCount;
                    double factor = mFriends + mLikes * 2;
                    user.SetFactor(f.Id, factor);
                    factor = user.GetFactor(f.Id);
                    //postavljanje prijatelja s najvecim faktorom
                    if (factor > maxFactor) { user.SetFriend(f.Id); maxFactor = factor; }

                }

          
                return View(user);
            }

            return View("Error");
        }

        // This action will handle the redirects from FacebookAuthorizeFilter when
        // the app doesn't have all the required permissions specified in the FacebookAuthorizeAttribute.
        // The path to this action is defined under appSettings (in Web.config) with the key 'Facebook:AuthorizationRedirectPath'.
        public ActionResult Permissions(FacebookRedirectContext context)
        {
            if (ModelState.IsValid)
            {
                return View(context);
            }

            return View("Error");
        }

    }
}
