using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;

namespace Cards
{
    /// <summary>
    /// An event for storing data created by user interaction
	/// CODEREVIEW: I'd consider renmaing this class slightly, as "Event" is very common. Perhaps "UserInteractionEvent"?
	/// CODEREVIEW: The comment above should say something like "holds data about a user interaction".
    /// </summary>
    abstract class EventInformation
    {
        // CODEREVIEW: This name is *slightly* misleading. When something accesses an EventInformation object, this property may no longer
		// CODEREVIEW: be the "current" date time. It should instead be called something like "OccurredWhen", or "EventOccuranceTime".
		public DateTime CurrentDateTime { get; }
		
		// CODEREVIEW: It's not completely clear to someone coming to the code without having seen it before what a "sender" is. This should
		// CODEREVIEW: be documented in the triple-slash XML documentation; this should match the constructor documentation ("Name of the object causing the event").
        public string SenderName { get; }

        /// <summary>
        /// An event for storing data created by user interaction
		/// CODEREVIEW: See comment on class name
        /// </summary>
        /// <param name="sender">Name of the object causing the event</param>
        /// <param name="time">Time of the event happening</param>
        protected EventInformation(string sender, DateTime time)
        {
            CurrentDateTime = time;
            SenderName = sender;
        }

        /// <summary>
        /// Converts event's information into XElemet type adding Button name and DateTime
		/// CODEREVIEW: Buttons have not been mentioned so far. They shouldn't be mentioned now. Make the comment complete and clear: "serialises the data to an XML representation containing 'time' and 'sender-name' elements".
        /// </summary>
		/// CODEREVIEW: Again, Button should not be mentioned at this point
        /// <returns>element containing Button name and DateTime</returns>
        public virtual XElement Serialize()
        {

            XElement currentDateTime = new XElement("time", CurrentDateTime);
            XElement senderName = new XElement("sender-name", SenderName);
            return new XElement("information", currentDateTime, senderName);
        }
    

	// CODEREVIEW: Are these comments meant to be here? If not, they should go :)
    // read-only
        /*
         * Better to keep the data in it's original format and manipulate it after 
         * classes, properties, methods anything public
          */

    }
}
