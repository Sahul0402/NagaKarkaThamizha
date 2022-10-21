using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Common.Utility
{
    public class ExceptionUtility
    {
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public void WriteErroLog(ExceptionUtility exc)
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
            //using (StreamWriter sw = new StreamWriter(logFilePath))
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine("Log Entry:  {0} ", DateTime.Now);
                if (exc != null)
                {
                    sw.WriteLine("Exception Message: " + exc.ExceptionMessage);
                    sw.WriteLine("Stack Trace: " + exc.StackTrace);
                    sw.WriteLine("Controller Name: " + exc.Controller);
                    sw.WriteLine("Action Name" + exc.Action);
                }

                sw.Close();
                sw.Dispose();
            }
        }

        // Log an Exception
        public static void LogException(Exception exc, ExceptionContext filterContext)
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
            //using (StreamWriter sw = new StreamWriter(logFilePath))
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine("Log Entry:  {0} ", DateTime.Now);
                if (exc.InnerException != null)
                {
                    sw.WriteLine("Inner Exception Type: ");
                    sw.WriteLine(exc.InnerException.GetType().ToString());
                    sw.WriteLine("Inner Exception: ");
                    sw.WriteLine(exc.InnerException.Message);
                    sw.WriteLine("Inner Source: ");
                    sw.WriteLine(exc.InnerException.Source);
                    if (exc.InnerException.StackTrace != null)
                    {
                        sw.WriteLine("Inner Stack Trace: ");
                        sw.WriteLine(exc.InnerException.StackTrace);
                    }
                }
                else if (exc != null)
                {
                    sw.WriteLine("Exception Type: " + exc.GetType().ToString());
                    sw.WriteLine("Exception: " + exc.Message);
                    //if (exc.StackTrace != null)
                    //{
                    //    sw.WriteLine("Stack Trace: ");
                    //    sw.WriteLine(exc.StackTrace);
                    //    sw.WriteLine();
                    //}
                    sw.WriteLine("Controller:" + ((System.Web.Mvc.HandleErrorInfo)((System.Web.Mvc.ViewResultBase)filterContext.Result).Model).ControllerName);
                    sw.WriteLine("Actoin:" + ((System.Web.Mvc.HandleErrorInfo)((System.Web.Mvc.ViewResultBase)filterContext.Result).Model).ActionName);
                    sw.WriteLine("Exception:" + ((System.Web.Mvc.HandleErrorInfo)((System.Web.Mvc.ViewResultBase)filterContext.Result).Model).Exception);
                    sw.WriteLine();
                }
                //SendMail(exc.ToString(), "support@karkathamizha.com", "KarkaThamizhaAdmin Error");
                sw.Flush();
                sw.Close();
            }
        }

        public static bool SendMail(string message, string mailTo, string subject)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    using (SmtpClient client = new SmtpClient())
                    {
                        mail.To.Add(mailTo);
                        mail.Subject = subject;
                        mail.Body = message;
                        mail.IsBodyHtml = true;

                        client.Host = "relay-hosting.secureserver.net";
                        client.Port = 25;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("info@karkathamizha.com", "M0h@m3d0701");
                        client.Send(mail);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
