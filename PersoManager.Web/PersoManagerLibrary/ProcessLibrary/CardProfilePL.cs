using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class CardProfilePL
    {
        public CardProfilePL()
        {

        }

        public static bool Save(CardProfile cardProfile, out string message)
        {
            try
            {
                if (CardProfileDL.CardProfileExists(cardProfile))
                {
                    message = string.Format("Card profile with name: {0} exists already", cardProfile.Name);
                    return false;
                }
                else
                {
                    message = string.Empty;
                    return CardProfileDL.Save(cardProfile);
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
                return CardProfileDL.Update(cardProfile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Object> RetrieveCardProfiles()
        {
            try
            {
                var returnedCardProfiles = new List<object>();

                var cardProfiles = CardProfileDL.RetrieveCardProfiles();

                foreach (CardProfile cardProfile in cardProfiles)
                {
                    Object cardProfileObj = new
                    {
                        ID = cardProfile.ID,
                        Name = cardProfile.Name,
                        CardType = cardProfile.CardType,
                        CardBin = cardProfile.CardBin,
                        CEDuration = cardProfile.CEDuration,
                    };

                    returnedCardProfiles.Add(cardProfileObj);
                }

                return returnedCardProfiles; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
