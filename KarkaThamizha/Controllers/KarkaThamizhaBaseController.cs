using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

/// <summary>
/// The Controller namespace.
/// </summary>
namespace KarkaThamizha.Controller
{
    [HandleError()]
    public class KarkaThamizhaBaseController : System.Web.Mvc.Controller, IExceptionFilter
    {
        public readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            //this.LogInfo(string.Format("Transaction before exception rollback : {0}", _isTransactionCompleted));

            //#region TransactionScope_Implementation
            //if (_scope != null && !_isTransactionCompleted)
            //{
            //    Rollback();
            //    _scope.Dispose();
            //}
            //#endregion

            //// do we have an exception object
            //if (filterContext.Exception != null)
            //{
            //    // check if this exception was already logged
            //    if (ErrorHandlingHelper.LastException != filterContext.Exception)
            //    {
            //        // we don't have it logged yet
            //        var errorReference = this.LogException(filterContext.Exception, suppressRethrow: true);
            //        ErrorHandlingHelper.ErrorReference = errorReference;
            //        ErrorHandlingHelper.LastException = filterContext.Exception;
            //    }

            //    // mark exception as handled
            //    filterContext.ExceptionHandled = true;

            //    // if this is an Ajax request, then return Ajax
            //    if (ErrorHandlingHelper.IsAjaxRequest(filterContext.HttpContext.Request))
            //    {
            //        var result = new JsonResult()
            //        {
            //            Data = ErrorHandlingHelper.GetErrorObject(),
            //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //        };

            //        filterContext.HttpContext.Response.Clear();
            //        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //        filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            //        filterContext.Result = result;
            //    }
            //    else
            //    {
            //        //TODO: instead of redirect, render the view
            //        // for now, redirect to error page
            //        filterContext.Result = Redirect(ErrorHandlingHelper.GetErrorPageRedirectUrl());
            //    }
            //}
        }

        protected KarkaThamizhaBaseController()
        {
            #region 
            #endregion
        }

        protected virtual void AddErrorMessage(string message)
        {
        }

        protected virtual void AddErrorMessages(string[] messages)
        {
            foreach (var s in messages)
            {
                AddErrorMessage(s);
            }
        }

        protected virtual void AddInformationalMessage(string message)
        {
        }

        protected virtual void AddInformationalMessage(string[] messages)
        {
            foreach (var s in messages)
            {
                AddInformationalMessage(s);
            }
        }

        private Stopwatch _stopWatch;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST.
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            string _value = GetValue(myIP + "TestWeb");

            if (Session["ActionStartTimeAndAction"] != null)
            {
                Debug.WriteLine(Session["ActionStartTimeAndAction"]);
            }
            else
            {
                Session["ActionStartTimeAndAction"] = DateTime.Now.ToString();//$"{filterContext.ActionDescriptor.ActionName}";
            }
            string info = string.Empty;

            info = string.Format("OnActionExecuting - Controller [{0}] Action [{1}] HttpMethod [{2}]",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName, filterContext.HttpContext.Request.HttpMethod);
            string ipaddr = GetIp();


            var properties = new List<KeyValuePair<string, object>>();

            string ip = Request.UserHostAddress;

            properties.Add(new KeyValuePair<string, object>("ipaddr", ip));
            properties.Add(new KeyValuePair<string, object>("username", "naga"));

            logger.WithProperties(properties.AsEnumerable()).Info(info);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();

            base.OnActionExecuting(filterContext);

        }

        private string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string info = string.Empty;

            info = string.Format("OnActionExecuted - Controller [{0}] Action [{1}] HttpMethod [{2}]",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName, filterContext.HttpContext.Request.HttpMethod);

            var properties = new List<KeyValuePair<string, object>>();

            string ip = Request.UserHostAddress;

            properties.Add(new KeyValuePair<string, object>("ipaddr", ip));
            properties.Add(new KeyValuePair<string, object>("username", "naga"));

            logger.WithProperties(properties.AsEnumerable()).Info(info);
            base.OnActionExecuted(filterContext);

            if (_stopWatch != null)
            {
                _stopWatch.Stop();
            }
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        //Add Value in cokies    
        public void SetValue(string key, string value)
        {
            Response.Cookies[key].Value = value;
            Response.Cookies[key].Expires = DateTime.Now.AddDays(1); // ad    
        }

        //Get value from cokkie    
        public string GetValue(string _str)
        {
            if (Request.Cookies[_str] != null)
                return Request.Cookies[_str].Value;
            else
                return "";
        }
    }
}