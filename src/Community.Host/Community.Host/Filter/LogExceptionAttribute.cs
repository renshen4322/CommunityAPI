using Community.Common;
using Community.Common.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Community.Utils.Common;


namespace Community.Host.Filter
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class LogExceptionAttribute : ExceptionFilterAttribute
    {

        //重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
            string errLog = "";
            Exception exception = actionExecutedContext.Exception;

            while (exception != null)
            {
                errLog += exception.Message + "\r\n";
                exception = exception.InnerException;
               
            }
            errLog += actionExecutedContext.Exception.StackTrace;
            LogHelper.Error(errLog);

            //2.返回调用方具体的异常信息

            if (actionExecutedContext.Exception is NotFoundException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "error"
                };
            }
            else if (actionExecutedContext.Exception is ArgumentException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "error"
                };
            }
            else if (actionExecutedContext.Exception is UnAuthorizedException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "error"
                };
            }
            else if (actionExecutedContext.Exception is ForbiddenException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "error"
                };
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "error"
                };
            }
            else if (actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "error"
                };
            }
            else
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "error"
                };
            }

            base.OnException(actionExecutedContext);
        }
    }
}