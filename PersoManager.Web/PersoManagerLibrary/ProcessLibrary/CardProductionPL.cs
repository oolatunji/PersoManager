using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class CardProductionPL
    {
        public Card ProduceCard(long cardProfileID, long requestingBranch)
        {
            try
            {
                var card = new Card();

                DateTime currentDate = DateTime.Now;
                Random randomizer = new Random();
                var cardProfile = CardProfileDL.RetrieveCardProfileByID(cardProfileID);
                string CompleteCardProfileBIN = cardProfile.CardBin;
                string cardType = cardProfile.CardType;
                var panlength = 0;

                if(cardType == StatusUtil.CardType.VerveCard.ToString())
                {
                    panlength = 19;
                }
                else if (cardType == StatusUtil.CardType.MasterCard.ToString() || cardType == StatusUtil.CardType.VISA.ToString())
                {
                    panlength = 16;
                }

                int randomNumberLength = (panlength - 1) - CompleteCardProfileBIN.Length;
                string randNumber = string.Empty;
                if (randomNumberLength <= 9)
                {
                    randNumber += randomizer.Next(Convert.ToInt32(Math.Pow(10, (double)randomNumberLength))).ToString();
                }
                else
                {
                    randNumber += randomizer.Next(Convert.ToInt32(Math.Pow(10, (double)randomNumberLength - 9))).ToString();
                    randNumber += randomizer.Next(Convert.ToInt32(Math.Pow(10, 9))).ToString();
                }

                //Generate new Card PAN
                string newCardPAN = string.Format("{0}{1}", CompleteCardProfileBIN, randNumber.PadLeft(randomNumberLength, '0'));
                //Get Luhn Digit
                string pan = string.Format("{0}{1}", newCardPAN, GetLuhnAlgorithmNumber(newCardPAN));
                //Get Luhn Digit

                card.CardPan = Crypter.Encrypt(System.Configuration.ConfigurationManager.AppSettings.Get("ekey"), pan);
                card.HashedCardPan = PasswordHash.MD5Hash(pan);
                card.CardProfileID = cardProfile.ID;
                card.RequestingBranch = requestingBranch;
                card.Date = System.DateTime.Now;
                card.CardExpiryDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(Convert.ToInt32(cardProfile.CEDuration));
                
                return card;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private int GetLuhnAlgorithmNumber(string data)
        {
            int sum = 0;
            bool odd = true;
            for (int i = data.Length - 1; i >= 0; i--)
            {
                if (odd == true)
                {
                    int tSum = Convert.ToInt32(data[i].ToString()) * 2;
                    if (tSum >= 10)
                    {
                        string tData = tSum.ToString();
                        tSum = Convert.ToInt32(tData[0].ToString()) + Convert.ToInt32(tData[1].ToString());
                    }
                    sum += tSum;
                }
                else
                    sum += Convert.ToInt32(data[i].ToString());
                odd = !odd;
            }
            return (sum % 10 == 0) ? 0 : 10 - (sum % 10);
        }

        protected int GetLuhnNumber(string EighteenDigitNumber)
        {
            int result = 0;
            result = (10 - (AddOddPositionDigits(EighteenDigitNumber) + CalculateEvenPositionDigits(EighteenDigitNumber)) % 10) % 10;
            return result;
        }

        protected int AddOddPositionDigits(string EighteenDigitNumber)
        {
            int sum = 0;
            for (int n = 1; n < EighteenDigitNumber.Length; n = n + 2)
            {
                //Add odd position digits
                sum += Convert.ToInt32(EighteenDigitNumber.Substring(n - 1, 1)); //bcos of zero based index  
            }
            return sum;
        }

        protected int CalculateEvenPositionDigits(string EighteenDigitNumber)
        {
            int sum = 0;
            string strConat = "";
            const int MulBy = 2;
            for (int n = 0; n < EighteenDigitNumber.Length; n = n + 2)
            {
                //multiple even position digits by 2
                long result = Convert.ToInt32(EighteenDigitNumber.Substring(n + 1, 1)) * MulBy;
                //Concatenate Them
                strConat = string.Concat(strConat, result.ToString());
            }

            foreach (char CH in strConat)
            {
                //Add all the digits in the string
                sum += Convert.ToInt32(CH.ToString());
            }
            return sum;
        }
    }
}
