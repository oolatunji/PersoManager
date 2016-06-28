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
    public class CardProfileAPIController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SaveCardProfile([FromBody]CardProfile cardProfile)
        {
            try
            {
                string errMsg = string.Empty;

                bool result = CardProfilePL.Save(cardProfile, out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                    return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Card profile added successfully.") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
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
        public HttpResponseMessage UpdateCardProfile([FromBody]CardProfile cardProfile)
        {
            try
            {
                bool result = CardProfilePL.Update(cardProfile);
                return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Card profile updated successfully") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveCardProfiles()
        {
            try
            {
                IEnumerable<Object> cardProfiles = CardProfilePL.RetrieveCardProfiles();
                object returnedCardProfiles = new { data = cardProfiles };
                return Request.CreateResponse(HttpStatusCode.OK, returnedCardProfiles);
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
