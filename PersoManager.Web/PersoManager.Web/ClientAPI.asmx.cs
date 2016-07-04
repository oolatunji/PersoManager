using PersoManagerLibrary;
using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Services;

namespace PersoManager.Web
{
    /// <summary>
    /// Summary description for ClientAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ClientAPI : System.Web.Services.WebService
    {

        [WebMethod]
        public UserModel GetUser()
        {
            return UserPL.RetrieveUser();
        }

        [WebMethod]
        public CardProfileModel[] GetCardProfiles()
        {
            var cardProfiles = CardProfilePL.GetCardProfiles();
            return cardProfiles.ToArray();
        }

        [WebMethod]
        public string RegisterCustomer(ClientCustomerModel customermodel)
        {
            string persoData = "";

            string errMsg = string.Empty;

            var customer = new Customer();
            customer.AccountNumber = customermodel.AccountNumber;
            customer.Date = System.DateTime.Now;
            customer.CustomerBranch = customermodel.CustomerBranch;
            customer.Downloaded = customermodel.Downloaded;
            customer.Card = new CardProductionPL().ProduceCard(customermodel.CardProfileID, Convert.ToInt64(customer.CustomerBranch));

            bool result = CustomerPL.IssueCustomerCard(customer, out errMsg);
            if(result)
            {
                if (string.IsNullOrEmpty(errMsg))
                {
                    var theCustomer = CustomerDL.RetrieveCustomer(customer.AccountNumber);

                    customer.Surname = theCustomer.Surname;
                    customer.Othernames = theCustomer.Othernames;

                    var customers = new List<Customer>();
                    customers.Add(customer);

                    persoData = CustomerPL.FormatCustomerPersoDataForDownload(customers);
                    return persoData;
                }
                else
                {
                    throw new Exception(errMsg);
                }
            }
            else 
            {
                if (!string.IsNullOrEmpty(errMsg))
                    throw new Exception(errMsg);
                else
                    throw new Exception("Customer Registration Failed.");
            }
        }
    }
}
