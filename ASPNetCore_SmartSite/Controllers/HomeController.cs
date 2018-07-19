using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNetCore_SmartSite.Models;

namespace ASPNetCore_SmartSite.Controllers
{
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult QueryAction(string query)
        {

            Microsoft.Bot.Builder.Luis.Models.LuisResult test = SmartEventHandler.Run(query);

            if(test.Intents.Count > 0)
            {
                ISmartEvent sEvent = new SmartEvent();
                var navigationEntity = test.Entities.FirstOrDefault()?.Entity;

                if(navigationEntity == null)
                    return new JavaScriptResult("return;");

                switch (test.Intents.First().Intent)
                {
                    case "NavigateWebPage":
                    {
                        sEvent.CallbackFunction = "navigationService.Navigate";

                        if (string.IsNullOrEmpty(navigationEntity) == false)
                        {
                            switch (navigationEntity.ToLower())
                            {
                                default:
                                case "home":
                                    sEvent.CallbackFunctionParameters = new string[] { "/home" };
                                    break;

                                case "contact":
                                    sEvent.CallbackFunctionParameters = new string[] { "/home/contact" };
                                    break;

                                case "about":
                                    sEvent.CallbackFunctionParameters = new string[] { "/home/about" };
                                    break;
                            }
                        }
                        break;
                    }

                    case "ContactUsBy":
                    {
                        sEvent.CallbackFunction = "navigationService.NavigateContactMethod";

                        if (string.IsNullOrEmpty(navigationEntity) == false)
                            sEvent.CallbackFunctionParameters = new string[] { navigationEntity };

                        break;
                    }

                }

                string parms = string.Empty;

                for (int i = 0; i < sEvent.CallbackFunctionParameters.Count(); i++)
                {
                    parms += (i == 0 ? string.Empty : ", ") + $@"'{sEvent.CallbackFunctionParameters[i]}'";
                }


                return new JavaScriptResult($"{sEvent.CallbackFunction}({parms})");
            }
            else
            {
                return new JavaScriptResult("return;");
            }
        }
    }
}
