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
    class EventWithDeal : EventInformation
    {
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
            XElement handNorth = new XElement("hand-North", HandNorth.Serialize());
            XElement handSouth = new XElement("hand-South", HandSouth.Serialize());
            XElement handEast = new XElement("hand-East", HandEast.Serialize());
            XElement handWest = new XElement("hand-West", HandWest.Serialize());
            output.Add(handNorth, handSouth, handEast, handWest);
            return output;
        }
    }
}
