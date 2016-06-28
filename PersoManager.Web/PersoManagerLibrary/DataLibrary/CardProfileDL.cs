using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class CardProfileDL
    {
        public CardProfileDL()
        {

        }

        public static bool Save(CardProfile cardProfile)
        {
            try
            {                                
                using (var context = new PersoDBEntities())
                {
                    context.CardProfiles.Add(cardProfile);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CardProfileExists(CardProfile cardProfile)
        {
            try
            {
                var existingCardProfile = new CardProfile();
                using (var context = new PersoDBEntities())
                {
                    existingCardProfile = context.CardProfiles
                                    .Where(t => t.Name.Equals(cardProfile.Name))
                                    .FirstOrDefault();
                }

                if (existingCardProfile == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CardProfile> RetrieveCardProfiles()
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var cardProfiles = context.CardProfiles.ToList();

                    return cardProfiles;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CardProfile RetrieveCardProfileByID(long id)
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var cardProfiles = context.CardProfiles
                                            .Where(f => f.ID == id);

                    return cardProfiles.Any() ? cardProfiles.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Update(CardProfile cardProfile)
        {
            try
            {
                var existingCardProfile = new CardProfile();
                using (var context = new PersoDBEntities())
                {
                    existingCardProfile = context.CardProfiles
                                    .Where(t => t.ID == cardProfile.ID)
                                    .FirstOrDefault();
                }

                if (existingCardProfile != null)
                {
                    existingCardProfile.Name = cardProfile.Name;
                    existingCardProfile.CardType = cardProfile.CardType;
                    existingCardProfile.CardBin = cardProfile.CardBin;
                    existingCardProfile.CEDuration = cardProfile.CEDuration;

                    using (var context = new PersoDBEntities())
                    {
                        context.Entry(existingCardProfile).State = EntityState.Modified;

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
