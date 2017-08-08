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
    /// </summary>
    abstract class EventInformation
    {
        public DateTime CurrentDateTime { get; }
        public string SenderName { get; }

        /// <summary>
        /// An event for storing data created by user interaction
        /// </summary>
        /// <param name="sender">Name of the object causing the event</param>
        /// <param name="time">Time of the event happening</param>
        protected EventInformation(string sender, DateTime time)
        {
            CurrentDateTime = time;
            SenderName = sender;
        }

        public virtual XElement Serialize()
        {

            XElement currentDateTime = new XElement("time", CurrentDateTime);
            XElement senderName = new XElement("sender-name", SenderName);
            return new XElement("information", currentDateTime, senderName);
        }
    






    // read-only
        /*
         * Better to keep the data in it's original format and manipulate it after 
         * classes, properties, methods anything public
          */





    }
}
