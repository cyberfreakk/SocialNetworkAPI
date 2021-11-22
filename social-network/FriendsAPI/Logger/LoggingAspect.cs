using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Microsoft.AspNetCore.Routing;


namespace FriendsService.Logger
{
    public class LoggingAspect : ActionFilterAttribute
    {
        string logFilePath;


        public LoggingAspect(IWebHostEnvironment environment)
        {
            logFilePath = environment.ContentRootPath + @"/LogFile.txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath, shared: false)
                .CreateLogger();

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Logging("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Logging("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Logging("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Logging("OnResultExecuted", filterContext.RouteData);
        }


        private void Logging(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Log.Information(message, "Keepnote Log");
            Log.CloseAndFlush();
        }


    }

}
