using PersoManager.Web.Models;
using PersoManagerLibrary;
using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersoManager.Web.Controllers.api
{
    public class CustomerAPIController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SaveCustomer([FromBody]CustomerModel customermodel)
        {
            try
            {
                string errMsg = string.Empty;
                Random rand = new Random();
                string accountnumber = rand.Next(20000, 39999).ToString();
                rand = new Random();
                accountnumber += rand.Next(10000, 99999).ToString();

                var customer = new Customer();
                customer.Surname = customermodel.Surname;
                customer.Othernames = customermodel.Othernames;
                customer.AccountNumber = accountnumber;
                customer.Date = System.DateTime.Now;
                
                bool result = CustomerPL.Save(customer, out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                    return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Customer registered successfully.") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
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
        public HttpResponseMessage UpdateCustomer([FromBody]Customer customer)
        {
            try
            {
                bool result = CustomerPL.Update(customer);
                return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Customer updated successfully") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveCustomers()
        {
            try
            {
                IEnumerable<Object> customers = CustomerPL.RetrieveCustomers();
                object returnedCustomers = new { data = customers };
                return Request.CreateResponse(HttpStatusCode.OK, returnedCustomers);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage IssueCustomerCard([FromBody]CustomerModel customermodel)
        {
            try
            {
                string errMsg = string.Empty;
                var customer = new Customer();
                customer.AccountNumber = customermodel.AccountNumber;
                customer.CustomerBranch = customermodel.CustomerBranch;
                customer.Downloaded = false;
                customer.Card = new CardProductionPL().ProduceCard(customermodel.CardProfileID, Convert.ToInt64(customer.CustomerBranch)); 

                bool result = CustomerPL.IssueCustomerCard(customer, out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                    return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Card Request was successful.") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
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

        [HttpGet]
        public HttpResponseMessage RetrieveCustomerPersoData()
        {
            try
            {
                IEnumerable<Object> customers = CustomerPL.RetrieveCustomerPersoData();
                object returnedCustomers = new { data = customers };
                return Request.CreateResponse(HttpStatusCode.OK, returnedCustomers);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage DownloadPersoFile([FromBody]CustomerModel customer)
        {
            try
            {
                CustomerPL.ProcessCustomerPersoFile(customer.CustomerIDs);

                var persofilepath = @System.Configuration.ConfigurationManager.AppSettings.Get("PersoFilePath");

                return Request.CreateResponse(HttpStatusCode.OK, string.Format("Perso file downloaded successfully. File path: {0}", persofilepath));
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage ResetDownload([FromBody]CustomerModel customer)
        {
            try
            {
                bool customerData = CustomerPL.ResetDownload(customer.CustomerIDs);

                return Request.CreateResponse(HttpStatusCode.OK, "Reset download was successful");
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }
    }
}
