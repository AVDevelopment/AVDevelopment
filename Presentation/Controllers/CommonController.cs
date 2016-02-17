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
using AV.Development.Core.Mongo;
using AV.Development.Core.Managers;
using System.Linq.Expressions;

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

            // Initialize our database provider.
            Setup.Initialize();

            List<messagescopy> dragons1 = new List<messagescopy>();
            dragons1 = DragonManager.GetPaginationAllEnron(1, 500);

            Expression<Func<messagescopy, bool>> filter = child => child.mailbox == "bass-e";
            var dt = DragonManager.FindSingleMessage(filter);
            var dt1 = DragonManager.FindgroupMessage(filter);

            //List<messagescopy> dragons12 = DragonManager.GetAllEnron().Where(a => a.mailbox == "bass-e").ToList(); ;
            ////get all dragons
            //dragons1 = DragonManager.GetAll();
            //// Search for dragons.
            //List<Dragon> dragons2 = DragonManager.Find("Evil Legendary", 1, 1);
            //// Search for dragons.
            //List<Dragon> dragons = DragonManager.Find("Evil Legendary");
            ////List<countryshortcodes> dragons1 = DragonManager.GetAllCountry().Where(a => a.name.ToLower().StartsWith("e")).ToList();


            UpdateSearcEngineRecursiveLoop(1, 10000);

            Setup.Close();


           

            result = new ServiceResponse();
            try
            {
                result.StatusCode = (int)HttpStatusCode.OK;
                //result.Response = DateTime.Now;
                result.Response = dragons1;
            }
            catch
            {
                result.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                result.Response = 0;
            }
            return result;
        }

        public int UpdateSearcEngineRecursiveLoop(int pageNumber, int PageSize)
        {
         
            List<messagescopy> dragons1 = new List<messagescopy>();
            dragons1 = DragonManager.GetPaginationAllEnron(pageNumber, PageSize);
           
            foreach (messagescopy mes in dragons1)
            {
                string messaBody = mes.body;
            }

            if (dragons1.Count == 0)
                return 0;
            else if (dragons1.Count >= 0)
                return UpdateSearcEngineRecursiveLoop(pageNumber + 1, PageSize);
            else return 0;

            
        }

    }



}