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
        [FacebookAuthorize("email", "user_photos", "user_likes", "user_friends", "user_actions.movies", "user_actions.television", "user_actions.video", "user_actions.music", "user_actions.books", "read_mailbox", "user_posts")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();
                var client = new Facebook.FacebookClient(context.Client.AccessToken);
                foreach (var f in user.Friends.Data)
                {
                    JsonObject data = (JsonObject)client.Get("/" + f.Id + "?fields=context.fields(mutual_friends)");
                    string dataS = data.ToString();
                    var jobject = JObject.Parse(dataS);
                    var facebookJson = jobject.ToObject<FacebookJson>();
                    int mFriends = facebookJson.Context.MutualFriends.Summary.TotalCount;

                    data = (JsonObject)client.Get("/" + f.Id + "?fields=context.fields(mutual_likes)");
                    dataS = data.ToString();
                    jobject = JObject.Parse(dataS);
                    facebookJson = jobject.ToObject<FacebookJson>();
                    int mLikes = facebookJson.Context.MutualLikes.Summary.TotalCount;
                    int factor = mFriends + mLikes * 2;
                    user.SetFactor(f.Id, factor);
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



        //[System.Web.Services.WebMethod]
        //public static 
    }
}
