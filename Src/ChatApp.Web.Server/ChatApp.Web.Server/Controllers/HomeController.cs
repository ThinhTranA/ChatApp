using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChatApp.Web.Server.Controllers
{
    public class HomeController : Controller
    {
        #region Protected Members
        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext mContext;
        #endregion
        public HomeController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Index()
        {
            var mContext = IoC.ApplicationDbContext;

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
    }
}
