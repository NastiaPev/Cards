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
	/// CODEREVIEW: CardDealEvent?
    /// </summary>
    class EventWithDeal : EventInformation
    {
		// CODEREVIEW: These two properties are defined on the base class so do not need to be defined here too.
        public DateTime CurrentDateTime { get; }
        public string SenderName { get; }

        public Hand HandNorth   { get; }
        public Hand HandSouth   { get; }
        public Hand HandEast    { get; }
        public Hand HandWest    { get; }

        /// <summary>
        /// Represents Deal button click. Records each of four hands. The constructor relies on hardcoded
        /// Hand's consequence North, South, East, West
        /// </summary>
        /// <param name="sender">Name of the object causing the event</param>
        /// <param name="time">Time of the event happening</param>
        /// <param name="handNorth">Hand labelled North</param>
        /// <param name="handSouth">Hand labelled South</param>
        /// <param name="handEast" >Hand labelled East</param>
        /// <param name="handWest" >Hand labelled West</param>
        public EventWithDeal(string sender, DateTime time, Hand handNorth, Hand handSouth, Hand handEast, Hand handWest) : base(sender, time)
        {
			// CODEREVIEW: These two properties are set in the base constructor so do not need to be set here too.
            CurrentDateTime = time;
            SenderName = sender;
			
            HandNorth = handNorth;
            HandSouth = handSouth;
            HandEast = handEast;
            HandWest = handWest;
        }

        /// <summary>
        /// Overrides standard serialization method.
        /// Converts event's information into XElemet type adding Button name, DateTime of the button press and four hands associated with it
        /// </summary>
        /// <returns>Xelement containing Button name, DateTime of the button press and four hands associated with it</returns>
        public override XElement Serialize()
        {
            XElement output = base.Serialize();
			// CODEREVIEW: By writing out the human-readable descriptions of the hands, it becomes harder for someone to programatically
			// CODEREVEIW: parse the hand from the serialisation. Instead, it could be beneficial for the Serialise() method on a Hand object
			// CODEREVIEW: to build up an XElement which contains child elements for each card, in order. E.g.:
			/*
			 * <hand>
			 *	<card suit="hearts" value="10"/>
			 *	<card suit="spades" value="queen"/>
			 *	...
			 *</hand>
			 * 
			 */
			 // CODEREVIEW: These could then be used in this serialisation, perhaps like:
	        /*
			* <north>
			*	<hand>
			*		<card suit="hearts" value="10"/>
			*		<card suit="spades" value="queen"/>
			*		...
			*	</hand>
			* </north>
			* <south>
			*	...
			* </south>
			*
			* etc.
			* 
			*/

            XElement handNorth = new XElement("hand-North", HandNorth.Serialize());
            XElement handSouth = new XElement("hand-South", HandSouth.Serialize());
            XElement handEast = new XElement("hand-East", HandEast.Serialize());
            XElement handWest = new XElement("hand-West", HandWest.Serialize());
            output.Add(handNorth, handSouth, handEast, handWest);
            return output;
        }
    }
}
