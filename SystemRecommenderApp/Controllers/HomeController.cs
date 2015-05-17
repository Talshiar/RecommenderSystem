using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Facebook;
using Microsoft.AspNet.Facebook.Client;
using SystemRecommenderApp.Models;

namespace SystemRecommenderApp.Controllers
{
    public class HomeController : Controller
    {
        [FacebookAuthorize("email", "user_photos", "user_likes")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();
                /*FacebookGroupConnection<MyAppUser.Like> likeList = new FacebookGroupConnection<MyAppUser.Like>();
                likeList = user.Likes;
                Facebook.FacebookClient b;
                foreach (List<MyAppUser.Like> l in likeList)
                {
                    
                }*/
                /*var fb = new Facebook.FacebookClient("access_token");
                dynamic me = fb.Get("me");
                var id = me.id;
                var name = me.name;*/
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
