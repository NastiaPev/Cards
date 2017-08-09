using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cards
{
    /// <summary>
    ///  Represents Deal button click. Records each hand.
    /// </summary>
    class CardDealUserInteraction : UserInteraction
    {
		public List<Hand> Hands{ get; }
        

        /// <summary>
        /// Represents Deal button click. Records each of four hands. 
        /// </summary>
        /// <param name="sender">Name of the object causing the event</param>
        /// <param name="time">Time of the event happening</param>
        /// <param name="hands">List of hands the cards were dealt to</param>
        public CardDealUserInteraction(string sender, DateTime time, List<Hand> hands) : base(sender, time)
        {
            Hands = hands;
        }

		/// <summary>
		/// CODEREVIEW: If we ever change the serialisation of the UserInteraction class, we'd need to remember to go to all of the subclasses
		/// CODEREVIEW: like this one, and change the documentation where we've mentioned 'time' and 'sender-name'. Instead, say something like
		/// CODEREVIEW: "Serializes the card deal user interaction into an XML representation containing four instances of the standard serialisation of a Hand,
		/// CODEREVIEW: along with the standard UserInteraction serialisation".
		/// Converts event's information into XElemet type adding 'time', 'sender-name' and four 'hand' elements
		/// </summary>
		/// <returns>XElement 'time', 'sender-name' and four 'hand' elements</returns>
		public override XElement Serialize()
        {
            XElement output = base.Serialize();
            foreach (Hand hand in Hands)
            {
                output.Add(hand.Serialize());
            }
            
            return output;
        }
    }
}
