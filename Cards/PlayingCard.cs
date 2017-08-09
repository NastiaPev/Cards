using System.Xml.Linq;

namespace Cards
{
	class PlayingCard
	{
        private readonly Suit suit;
        private readonly Value value;

		public PlayingCard(Suit s, Value v)
		{
			this.suit = s;
			this.value = v;
		}

        public override string ToString()
		{
            string result = $"{this.value} of {this.suit}";
            return result;
		}


	    /// <summary>
	    /// Returns an XML representation of the card, containing 'suit' and 'value' elements
	    /// </summary>
	    /// <returns>XML representation of the card</returns>
	    public XElement Serialize()
	    {
	        return new XElement("card",
		        // CODEREVIEW: See the comment on "Hand.cs" for why this is dangerous.
				new XElement("suit", suit),
	            new XElement("value", value));
	    }
    }
}