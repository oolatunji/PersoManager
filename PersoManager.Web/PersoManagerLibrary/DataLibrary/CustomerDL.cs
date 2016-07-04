using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class CustomerDL
    {
        public CustomerDL()
        {

        }

        public static bool Save(Customer customer)
        {
            try
            {                                
                using (var context = new PersoDBEntities())
                {                    
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CustomerExists(Customer customer)
        {
            try
            {
                var existingCustomer = new Customer();
                using (var context = new PersoDBEntities())
                {
                    existingCustomer = context.Customers
                                    .Where(t => t.Surname.Equals(customer.Surname) && t.Othernames.Equals(customer.Othernames))
                                    .FirstOrDefault();
                }

                if (existingCustomer == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Customer> RetrieveCustomers()
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var customers = context.Customers
                                            .ToList();

                    return customers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Customer> RetrieveCustomersByID(long[] id)
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var customers = context.Customers
                                            .Where(c => id.Contains(c.ID))
                                            .Include("Card")
                                            .Include("Card.CardProfile")
                                            .ToList();

                    return customers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Customer RetrieveCustomer(string accountnumber)
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var customers = context.Customers
                                            .Where(c => c.AccountNumber == accountnumber);

                    return customers.Any() ? customers.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UpdateStatus(long[] customerIDs, bool status)
        {
            try
            {
                var existingCustomer = new List<Customer>();
                using (var context = new PersoDBEntities())
                {
                    existingCustomer = context.Customers
                                    .Where(t => customerIDs.Contains(t.ID))
                                    .ToList();
                }

                if (existingCustomer.Any())
                {
                    existingCustomer.ForEach(customer =>
                        {
                            customer.Downloaded = status;

                            using (var context = new PersoDBEntities())
                            {
                                context.Entry(customer).State = EntityState.Modified;

                                context.SaveChanges();
                            }

                        });
                    
                    return true;
                }
                else
                {
                    return false;
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
                var existingCustomer = new Customer();
                using (var context = new PersoDBEntities())
                {
                    existingCustomer = context.Customers
                                    .Where(t => t.ID == customer.ID)
                                    .FirstOrDefault();
                }

                if (existingCustomer != null)
                {
                    existingCustomer.Surname = customer.Surname;
                    existingCustomer.Othernames = customer.Othernames;

                    using (var context = new PersoDBEntities())
                    {
                        context.Entry(existingCustomer).State = EntityState.Modified;

                        context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IssueCustomerCard(Customer customer, out string errMsg)
        {
            try
            {
                var existingCustomer = new Customer();
                using (var context = new PersoDBEntities())
                {
                    existingCustomer = context.Customers
                                    .Where(t => t.AccountNumber == customer.AccountNumber)
                                    .FirstOrDefault();


                    if (existingCustomer != null && existingCustomer.ID != 0)
                    {
                        if (existingCustomer.Card == null)
                        {
                            using (var transaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    long cardid = 0;

                                    if (CardDL.Save(context, customer.Card, out cardid))
                                    {
                                        existingCustomer.CustomerBranch = customer.CustomerBranch;
                                        existingCustomer.Downloaded = customer.Downloaded;
                                        existingCustomer.CustomerCard = cardid;


                                        context.Entry(existingCustomer).State = EntityState.Modified;
                                        context.SaveChanges();

                                        transaction.Commit();

                                        errMsg = string.Empty;
                                        return true;
                                    }
                                    else
                                    {
                                        errMsg = "Request Failed";
                                        transaction.Rollback();
                                        return false;
                                    }
                                }
                                catch(Exception e)
                                {
                                    transaction.Rollback();
                                    throw e;
                                }
                            }
                        }
                        else
                        {
                            errMsg = string.Format("Customer with account number: {0} has a card already", customer.AccountNumber);
                            return true;
                        }
                    }
                    else
                    {
                        errMsg = string.Format("No customer found with account number: {0}", customer.AccountNumber);
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Customer> RetrieveCustomerPersoData()
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var customers = context.Customers
                                            .Where(c => c.CustomerCard != null)                    
                                            .Include("Card")
                                            .Include("Card.CardProfile")
                                            
                                            .ToList();

                    return customers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
