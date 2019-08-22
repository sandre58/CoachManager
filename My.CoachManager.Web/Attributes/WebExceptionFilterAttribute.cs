using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using My.CoachManager.CrossCutting.Core.Exceptions;

namespace My.CoachManager.Web.Attributes
{
    public class WebExceptionFilterAttribute : ExceptionFilterAttribute
    {
        #region Public Methods and Operators

        public override void OnException(ExceptionContext context)
        {

            context.Result = new BadRequestObjectResult(new ApiException(context.Exception.Message, context.Exception));
        }

        #endregion
    }
}