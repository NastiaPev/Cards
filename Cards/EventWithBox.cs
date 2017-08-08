using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Activation;

namespace Cards
{
    /// <summary>
    /// Represents button clicks, which have number boxes assosiated with them. Records the number in the box.
    /// </summary>
    class EventWithBox : EventInformation
    {
        public DateTime CurrentDateTime { get; }
        public string SenderName { get; }

        /// <summary>
        /// Overrides standard serialization method.
        /// Converts event's information into XElemet type adding Button name, DateTime of the button press and number in the box
        /// </summary>
        /// <returns>XElement containing Button name, DateTime of the button press and number in the box associated with it</returns>
        public override XElement Serialize()
        {
            XElement output = base.Serialize();
            XElement numberInTheBox = new XElement("number-in-the-box", NumberInTheBox);
            output.Add(numberInTheBox);
            return output;
        }

        public int NumberInTheBox { get; }

        /// <summary>
        /// Represents button clicks, which have number boxes assosiated with them. Records the number in the box.
        /// </summary>
        /// <param name="sender">Name of the object causing the event</param>
        /// <param name="time">Time of the event happening</param>
        /// <param name="boxNumber">The number in the box</param>
        public EventWithBox(string sender, DateTime time, int boxNumber) : base(sender, time)
        {
            CurrentDateTime = time;
            SenderName = sender;
            NumberInTheBox = boxNumber;
        }
    }
}
