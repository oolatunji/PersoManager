using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class CardDL
    {
        public static bool Save(PersoDBEntities context, Card card, out long savedCardID)
        {
            try
            {
                context.Cards.Add(card);
                context.SaveChanges();
                savedCardID = card.ID;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Card> RetrieveCards()
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var cards = context.Cards.ToList();

                    return cards;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
