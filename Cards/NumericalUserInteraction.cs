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
		/// CODEREVIEW: If we ever change the serialisation of the UserInteraction class, we'd need to remember to go to all of the subclasses
		/// CODEREVIEW: like this one, and change the documentation where we've mentioned 'time' and 'sender-name'. Instead, say something like
		/// CODEREVIEW: "Serializes the user interaction into an XML representation containing 'time' along with the standard UserInteraction serialisation".
		/// Serializes event's information into XML representation containing 'time', 'sender-name' and 'number-recorded' elements
		/// </summary>
		/// <returns>
		/// CODEREVIEW: old comment
		/// XElement containing Button name, DateTime of the button press and number in the box associated with it</returns>
		public override XElement Serialize()
        {
            XElement output = base.Serialize();
			XElement numberInTheBox = new XElement("number-recorded", NumberRecorded);
            output.Add(numberInTheBox);
            return output;
        }

        public int NumberRecorded { get; }

		/// <summary>
		/// CODEREVIEW: There is a slight discrepency between "boxNumber" and "NumberRecorded". Try to stick with one naming convension.
		/// Represents button clicks, which have number boxes assosiated with them. Records the number in the box.
		/// </summary>
		/// <param name="sender">Name of the object causing the event</param>
		/// <param name="time">Time of the event happening</param>
		/// <param name="boxNumber">The number associated with teh event</param>
		public NumericalUserInteraction(string sender, DateTime time, int boxNumber) : base(sender, time)
        {
            NumberRecorded = boxNumber;
        }
    }
}
