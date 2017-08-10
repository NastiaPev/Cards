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
    /// Represents button clicks, which have a number assosiated with them. Records the number.
    /// </summary>
    class NumericalUserInteraction : UserInteraction
    {

		/// <summary>
		/// Serializes event's information into XML representation containing standard UserInteraction serialization and 'number-recorded' element
		/// </summary>
		/// <returns>
		/// XElement representing User Interaction with number assosiated with it </returns>
		public override XElement Serialize()
        {
            XElement output = base.Serialize();
			XElement numberInTheBox = new XElement("number-recorded", NumberRecorded);
            output.Add(numberInTheBox);
            return output;
        }

        public int NumberRecorded { get; }

		/// <summary>
		/// CODEREVIEW: There is a slight discrepency between "boxNumber" and "NumberRecorded". Try to stick with one naming conve
		/// Represents button clicks, which have number assosiated with them. Records the number.
		/// </summary>
		/// <param name="sender">Name of the object causing the event</param>
		/// <param name="time">Time of the event happening</param>
		/// <param name="boxNumber">The number associated with the event</param>
		public NumericalUserInteraction(string sender, DateTime time, int boxNumber) : base(sender, time)
        {
            NumberRecorded = boxNumber;
        }
    }
}
