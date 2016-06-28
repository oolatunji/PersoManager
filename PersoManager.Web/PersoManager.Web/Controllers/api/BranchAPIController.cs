using PersoManagerLibrary;
using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersoManager.Web.Controllers.api
{
    public class BranchAPIController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SaveBranch([FromBody]Branch branch)
        {
            try
            {
                string errMsg = string.Empty;
                bool result = BranchPL.Save(branch, out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                    return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Branch added successfully.") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, errMsg);
                    return response;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateBranch([FromBody]Branch branch)
        {
            try
            {
                bool result = BranchPL.Update(branch);
                return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Branch updated successfully") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveBranches()
        {
            try
            {
                IEnumerable<Object> branches = BranchPL.RetrieveBranches();
                object returnedBranches = new { data = branches };
                return Request.CreateResponse(HttpStatusCode.OK, returnedBranches);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }
    }
}
