using AV.Development.Core;
using AV.Development.Core.Interface;
using AV.Development.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Development.Utility;
using AV.Development.Core.Managers;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace AV.Development.Web.Controllers
{

    [RoutePrefix("api/Common")]
    public class CommonController : ApiController
    {

        ServiceResponse result;


        [AllowAnonymous]
        [HttpGet]
        [Route("GetCurrentTime/{id}")]
        public ServiceResponse GetCurrentTime(int id)
        {

            result = new ServiceResponse();
            try
            {
                result.StatusCode = (int)HttpStatusCode.OK;
                result.Response = DateTime.Now;
            }
            catch
            {
                result.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                result.Response = 0;
            }
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("TestMethod")]
        public ServiceResponse TestMethod()
        {

            result = new ServiceResponse();
            try
            {
                Guid systemSession = DevelopmentManagerFactory.GetSystemSession();
                IDevelopmentManager marcomManager = DevelopmentManagerFactory.GetDevelopmentManager(systemSession);
                result.StatusCode = (int)HttpStatusCode.OK;
                result.Response = marcomManager.CommonManager.TestMethod();
            }
            catch
            {
                result.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                result.Response = 0;
            }
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetEntities/{pageNo}/{pageSize}")]
        public ServiceResponse GetEntities(int pageNo, int pageSize)
        {
            result = new ServiceResponse();
            try
            {
                Guid systemSession = DevelopmentManagerFactory.GetSystemSession();
                IDevelopmentManager marcomManager = DevelopmentManagerFactory.GetDevelopmentManager(systemSession);
                result.StatusCode = (int)HttpStatusCode.OK;
                //result.Response = JsonConvert.SerializeObject(marcomManager.CommonManager.GetEntities(pageNo, pageSize)); //serialization sample
                result.Response = marcomManager.CommonManager.GetEntities(pageNo, pageSize);
            }
            catch
            {
                result.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                result.Response = 0;
            }
            return result;
        }


    }



}