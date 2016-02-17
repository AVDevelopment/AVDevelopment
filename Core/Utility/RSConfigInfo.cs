using System.Configuration;
using System.ServiceModel;

namespace Development.Utility
{
    public static class RSConfigInfo {
       // string ReportServerUrl = ConfigurationSettings.AppSettings["ReportServerUrl"].ToString();
        public static string ServerBasedAddress = "";  //ConfigurationSettings.AppSettings["ReportServerUrl"].ToString();
        //public static string ServerBasedAddress = "";

        public const string AuthCookieName = ".ASPXAUTH";
        public const string StoreAuthCookieName = "DXRS.ASPXAUTH";

        public static  string ViewerUsername = "";
        public static string ViewerPassword = "";

        static readonly EndpointAddress serverFacade = new EndpointAddress(ServerBasedAddress + "ReportServerFacade.svc");
        static readonly EndpointAddress authenticationService = new EndpointAddress(ServerBasedAddress + "AuthenticationService.svc");

        public static EndpointAddress ServiceFacade { get { return serverFacade; } }
        public static EndpointAddress AuthenticationService { get { return authenticationService; } }
    }
}
