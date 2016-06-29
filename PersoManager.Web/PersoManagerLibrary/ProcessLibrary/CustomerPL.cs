using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace PersoManagerLibrary
{
    public class CustomerPL
    {
        public CustomerPL()
        {

        }

        public static bool Save(Customer customer, out string message)
        {
            try
            {
                if (CustomerDL.CustomerExists(customer))
                {
                    message = string.Format("Customer with account number: {0} exists already", customer.AccountNumber);
                    return false;
                }
                else
                {
                    message = string.Empty;
                    return CustomerDL.Save(customer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Update(Customer customer)
        {
            try
            {
                return CustomerDL.Update(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Object> RetrieveCustomers()
        {
            try
            {
                List<Object> returnedCustomers = new List<object>();

                List<Customer> customers = CustomerDL.RetrieveCustomers();

                foreach (Customer customer in customers)
                {
                    Object customerObj = new
                    {
                        ID = customer.ID,
                        Surname = customer.Surname,
                        Othernames = customer.Othernames,
                        AccountNumber = customer.AccountNumber,
                        CardPan = Crypter.Decrypt(System.Configuration.ConfigurationManager.AppSettings.Get("ekey"), customer.Card.CardPan),
                        CardType = customer.Card.CardProfile.CardType
                    };

                    returnedCustomers.Add(customerObj);
                }

                return returnedCustomers; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Object> RetrieveCustomerPersoData()
        {
            try
            {
                List<Object> returnedCustomers = new List<object>();

                List<Customer> customers = CustomerDL.RetrieveCustomers();

                foreach (Customer customer in customers)
                {
                    Object customerObj = new
                    {
                        ID = customer.ID,
                        Surname = customer.Surname,
                        Othernames = customer.Othernames,
                        CardPan = Crypter.Decrypt(System.Configuration.ConfigurationManager.AppSettings.Get("ekey"), customer.Card.CardPan),
                        CardExpiryDate = String.Format("{0:MM/yy}", Convert.ToDateTime(customer.Card.CardExpiryDate)),
                        Downloaded = customer.Downloaded
                    };

                    returnedCustomers.Add(customerObj);
                }

                return returnedCustomers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ProcessCustomerPersoFile(long[] id)
        {
            try
            {
                List<Customer> customers = CustomerDL.RetrieveCustomersByID(id);

                var customerPersoData = FormatCustomerPersoDataForDownload(customers);

                DownloadCustomerPersoData(customerPersoData, id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void DownloadCustomerPersoData(string persoData, long[] id)
        {
            //System.Web.HttpContext.Current.Response.Clear();
            //System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=log.txt");
            //System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", persoData.Length.ToString());
            //System.Web.HttpContext.Current.Response.ContentType = "text/plain";
            //System.Web.HttpContext.Current.Response.Write(persoData);
            //System.Web.HttpContext.Current.Response.Flush();
            FileWriter.Write(persoData.ToString());
            CustomerDL.UpdateStatus(id, true);
        }

        private static string FormatCustomerPersoDataForDownload(List<Customer> customers)
        {
            var customerData = new StringBuilder();

            foreach (Customer customer in customers)
            {
                var _pan = Crypter.Decrypt(System.Configuration.ConfigurationManager.AppSettings.Get("ekey"), customer.Card.CardPan);

                var customerName = FormatCustomerName(customer.Surname, customer.Othernames).ToUpper();

                customerData.Append("000001");
                customerData.Append("#");
                customerData.Append(PasswordHash.ReFormatPan(_pan));
                customerData.Append("#");
                customerData.Append("05/15");
                customerData.Append("#");
                customerData.Append(String.Format("{0:MM/yy}", Convert.ToDateTime(customer.Card.CardExpiryDate)));
                customerData.Append("#");
                customerData.Append(customerName.PadRight(25,' '));
                customerData.Append("#");
                customerData.Append("Wisecard Technology");
                customerData.Append("      ");
                customerData.Append("#");
                customerData.Append(string.Format("478[%B{0}^{1}^{2}201163540000000000478000000? ;{3}={4}2011635447800000?]~389@@", _pan, customerName.PadRight(26, ' '), String.Format("{0:yyMM}", Convert.ToDateTime(customer.Card.CardExpiryDate)), _pan, String.Format("{0:yyMM}", Convert.ToDateTime(customer.Card.CardExpiryDate))));
                customerData.Append("#");
                customerData.Append("100002 7700690073006500630061007200");
                customerData.Append("#");
                customerData.Append("05/15 12345 EMV Golden                  ");
                customerData.Append("#");
                customerData.Append("support@wisecardtech.com           ");
                customerData.Append("#");
                customerData.Append("                                   ");
                customerData.Append("#");
                customerData.Append("                                   ");
                customerData.Append("#");
                customerData.Append("                                   ");
                customerData.Append("#");
                customerData.Append("                              ");
                customerData.Append("#");
                customerData.Append("EUR        5,000");
                customerData.Append("#");
                customerData.Append("EUR        5,000");
                customerData.Append("#");
                customerData.Append(customerName.PadRight(40, ' '));
                customerData.Append("#");
                customerData.Append("EMV Golden                    ");
                customerData.Append("#");
                customerData.Append("EUR        5,000");
                customerData.Append("#");
                customerData.Append("Delivery Branch");
                customerData.Append("     ");
                customerData.Append("#");
                customerData.Append("EUR");
                customerData.Append("#");
                customerData.Append("100");
                customerData.Append("#");
                customerData.Append(" 25");
                customerData.Append("#");
                customerData.Append("100002@@@@@@;5F2D=--");

                if (customers.Count > 1)
                {
                    customerData.AppendLine();
                }
            }

            return customerData.ToString();
        }

        private static string FormatCustomerName(string lastname, string othernames)
        {
            var formattedCustomerName = string.Empty;

            var customername = string.Format("{0} {1}", othernames, lastname);

            if(customername.Length > 25)
            {
                if(othernames.Contains(' '))
                {
                    var names = othernames.Split(' ');
                    var firstName = names[0].Trim();
                    var middleName = names[1].Trim();

                    customername = string.Format("{0} {1} {2}", firstName.Substring(0, 1), middleName.Substring(0, 1), lastname);
                }
                else
                {
                    customername = string.Format("{0} {1}", othernames.Substring(0, 1), lastname);
                }
            }

            formattedCustomerName = customername;

            return formattedCustomerName;
        }

        public static bool ResetDownload(long[] id)
        {
            try
            {
                return CustomerDL.UpdateStatus(id, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
