using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Web.Server.Controllers
{
    public class HomeController : Controller
    {
        #region Protected Members
        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext mContext;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles, etc...
        /// </summary>
        protected UserManager<ApplicationUser> mUserManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> mSignInManager;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        public HomeController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        public IActionResult Index()
        {
           // var mContext = IoC.ApplicationDbContext;

            //Make sure we have the database
            mContext.Database.EnsureCreated();
                
            //Don't need to dispose context as the ServiceProvider will do this.
            if(!mContext.Settings.Any())
            {
                mContext.Settings.Add(new SettingsDataModel
                {
                    Name = "BackgroundColor",
                    Value = "Red"
                });

                var settingsLocally = mContext.Settings.Local.Count();
                var settingsDatabase = mContext.Settings.Count();

                var firstLocal = mContext.Settings.Local.FirstOrDefault();
                var firstDatabase = mContext.Settings.FirstOrDefault();

                mContext.SaveChanges();

                settingsLocally = mContext.Settings.Local.Count();
                settingsDatabase = mContext.Settings.Count();

                firstLocal = mContext.Settings.Local.FirstOrDefault();
                firstDatabase = mContext.Settings.FirstOrDefault();

            }

            return View();
            
        }

        public IActionResult Error()
        {
            return View();
        }

        [Route("create")]
        public async
         Task<IActionResult> CreateUserAsync()
        {

            var result = await mUserManager.CreateAsync(new ApplicationUser
            {
                UserName = "angelsix",
                Email = "contact@angelsix.com"
            }, "password");

            if(result.Succeeded)
                return Content("User was created", "text/html");

            return Content("User creation failed", "text/html");
        }
    }
}
