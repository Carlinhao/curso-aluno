using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CursoOnline.Web.Filters
{
    public class CustonExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHtmlRequest";

            if (isAjaxCall)
            {
                context.HttpContext.Response.ContentType = "application.json";
                context.HttpContext.Response.StatusCode = 500;
                var message = context.Exception is ArgumentException ? context.Exception.Message : "An error ocorred";
                context.Result = new JsonResult(message);
                context.ExceptionHandled = true;
            }
            base.OnException(context);
        }
    }
}
