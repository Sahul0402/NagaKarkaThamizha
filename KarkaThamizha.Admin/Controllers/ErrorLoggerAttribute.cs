using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Admin.Controllers
{
    public class ErrorLoggerAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            LogError(filterContext);
            base.OnException(filterContext);
        }

        public void LogError1(ExceptionContext filterContext)
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString());
            string date = dt.ToString("dd-MM-yyyy");

            // Include logic for logging exceptions
            // Get the absolute path to the log file
            string logFilePath = "App_Data\\";
            //logFile = HttpContext.Current.Server.MapPath(logFile);
            logFilePath = AppDomain.CurrentDomain.BaseDirectory + logFilePath + date + ".txt";
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
            // Open the log file for append and write the log

            StackFrame CallStack = new StackFrame(1, true);

            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine("----------")
                .AppendLine(DateTime.Now.ToString())
                .AppendFormat("Source:\t{0}", filterContext.Exception.Source)
                .AppendLine()
                .AppendFormat("Target:\t{0}", filterContext.Exception.TargetSite)
                .AppendLine()
                .AppendFormat("Type:\t{0}", filterContext.Exception.GetType().Name)
                .AppendLine()
                .AppendFormat("Message:\t{0}", filterContext.Exception.Message)
                .AppendLine()
                .AppendFormat("Stack:\t{0}", filterContext.Exception.StackTrace)
                .AppendLine()
                .AppendFormat("Line:\t{0}", CallStack.GetFileLineNumber())
                .AppendLine();

            //string filePath = filterContext.HttpContext.Server.MapPath("~/App_Data/Error.log");

            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.Write(builder.ToString());
                writer.Flush();
                writer.Dispose();
            }
        }

        public void LogError(ExceptionContext filterContext)
        {   
            // Open the log file for append and write the log

            StackFrame CallStack = new StackFrame(1, true);

            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine("----------")
                .AppendLine(DateTime.Now.ToString())
                .AppendFormat("Source:\t{0}", filterContext.Exception.Source)
                .AppendLine()
                .AppendFormat("Target:\t{0}", filterContext.Exception.TargetSite)
                .AppendLine()
                .AppendFormat("Type:\t{0}", filterContext.Exception.GetType().Name)
                .AppendLine()
                .AppendFormat("Message:\t{0}", filterContext.Exception.Message)
                .AppendLine()
                .AppendFormat("Stack:\t{0}", filterContext.Exception.StackTrace)
                .AppendLine()
                .AppendFormat("Line:\t{0}", CallStack.GetFileLineNumber())
                .AppendLine();

            WriteLogError(builder.ToString());
        }

        public static void WriteLogError(string error)
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString());
            string date = dt.ToString("dd-MM-yyyy");

            // Include logic for logging exceptions
            // Get the absolute path to the log file
            string logFilePath = "App_Data\\";
            //logFile = HttpContext.Current.Server.MapPath(logFile);
            logFilePath = AppDomain.CurrentDomain.BaseDirectory + logFilePath + date + ".txt";
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.Write(error);
                writer.Flush();
                writer.Dispose();
            }

        }
    }
}