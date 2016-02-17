using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using AV.Development.Core.Interface;
using AV.Development.Dal;
using AV.Development.Core;
using AV.Development.Core.User.Interface;
using Development.Utility;
using System.Globalization;
using AV.Development.Core.Common;


namespace AV.Development.Web
{
    public class Global : System.Web.HttpApplication
    {
        
        private Dictionary<string, string> ApplicationSettings = new Dictionary<string, string>();


        void Application_Start(object sender, EventArgs e)
        {

           
            LogHandler.LogInfo("global asax file hit happen at" + DateTime.Now, LogHandler.LogType.General);

            //intialize all schedulers
            InitializeSchedulers();

            //register MVC Web API Attribute routing
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);

            //intialize all Development tenants
            DevelopmentManagerFactory.InitializeSystem();

           LogHandler.LogInfo("global asax file hit finished  at" + DateTime.Now, LogHandler.LogType.General);

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        void Session_Start(object sender, EventArgs e)
        {

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        //Function to loop through each tenant and initalize schedulers for mail, task, preview etc
        void InitializeSchedulers()
        {

           //initialize any mail scheduler
           
        }


    }
}
