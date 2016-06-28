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
                                    .Where(t => t.AccountNumber.Equals(customer.AccountNumber))
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
    }
}
