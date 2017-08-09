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
    /// Holds data about a user interaction
    /// </summary>
    abstract class UserInteraction
    {

		public DateTime InteractionOccuranceDateTime { get; }

        /// <summary>
        /// Name of the object causing the event
        /// </summary>
        public string SenderName { get; }

        /// <summary>
        /// Holds data about a user interaction
        /// </summary>
        /// <param name="sender">Name of the object causing the event</param>
        /// <param name="time">Time of the event happening</param>
        protected UserInteraction(string sender, DateTime time)
        {
            InteractionOccuranceDateTime = time;
            SenderName = sender;
        }

        /// <summary>
        /// Serialises the data about the user interactionto an XML representation containing 'time' and 'sender-name' elements"
        /// </summary>
        /// <returns>XElement containing name of the object, user interacted with, and DateTime of the interection occuring</returns>
        public virtual XElement Serialize()
        {

            XElement currentDateTime = new XElement("time", InteractionOccuranceDateTime);
            XElement senderName = new XElement("sender-name", SenderName);
            return new XElement("information", currentDateTime, senderName);
        }
    

	// CODEREVIEW: Are these comments meant to be here? If not, they should go :)
    //response - they are kept here as a gentle reminder for me
    // read-only
        /*
         * Better to keep the data in it's original format and manipulate it after 
         * classes, properties, methods anything public
          */

    }
}
