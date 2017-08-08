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
	///
	/// CODEREVIEW: 1. Having a naming convention of XxxEvent for subclasses would be ideal.
	/// CODEREVIEW: 2. This event class only contains an extra integer. Perhaps a less specific name like "NumericalClickEvent" would be better?
    /// </summary>
    class EventWithBox : EventInformation
    {
		// CODEREVIEW: These two properties are defined on the base class so do not need to be defined here too.
        public DateTime CurrentDateTime { get; }
        public string SenderName { get; }

        /// <summary>
        /// CODEREVIEW: You don't really need to specify that the serialisation is overriden. Just describe it:
		/// CODEREVIEW: e.g. "serialises the data to an XML representation containing 'time', 'sender-name' and 'number' elements"
		///
		/// Overrides standard serialization method.
        /// Converts event's information into XElemet type adding Button name, DateTime of the button press and number in the box
        /// </summary>
        /// <returns>XElement containing Button name, DateTime of the button press and number in the box associated with it</returns>
        public override XElement Serialize()
        {
            XElement output = base.Serialize();
            // CODEREVIEW: If you go with the simpler name, this could just be called "clicked-number".
			XElement numberInTheBox = new XElement("number-in-the-box", NumberInTheBox);
            output.Add(numberInTheBox);
            return output;
        }

		// CODEREVIEW: as above, consider renaming this to be less specific
        public int NumberInTheBox { get; }

        /// <summary>
        /// Represents button clicks, which have number boxes assosiated with them. Records the number in the box.
        /// </summary>
        /// <param name="sender">Name of the object causing the event</param>
        /// <param name="time">Time of the event happening</param>
        /// <param name="boxNumber">The number in the box</param>
        public EventWithBox(string sender, DateTime time, int boxNumber) : base(sender, time)
        {
			// CODEREVIEW: These two properties do not need to be set here, they will be set in the base constructor.
            CurrentDateTime = time;
            SenderName = sender;
			
            NumberInTheBox = boxNumber;
        }
    }
}
