using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.CrossCutting.Logging;

namespace My.CoachManager.Web.Attributes
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly Stopwatch _watch = new Stopwatch();

        #endregion

        #region Constructors and Destructors

        public LogActionFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods and Operators

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Stop Chronometer
            _watch.Stop();

            if (context != null)
            {
                if (context.Exception != null)
                {
                    _logger.Error(context.Exception);
                }
                else
                {
                    _logger.Info(string.Format(LoggingTraceResources.EndWebApiCallTrace, context.HttpContext.Request.Method, context.HttpContext.Request.GetDisplayUrl(), _watch.ElapsedMilliseconds));
                }
            }

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Start Chronometer
            _watch.Start();

            _logger.Info(string.Format(LoggingTraceResources.StartWebApiCallTrace, context.HttpContext.Request.Method, context.HttpContext.Request.GetDisplayUrl()));
        }

        #endregion
    }
}