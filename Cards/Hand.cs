using System;

namespace Cards
{
	class Hand
	{
        private static int handSize;
        private PlayingCard[] cards = new PlayingCard[handSize];
        private int playingCardCount = 0;

		public void AddCardToHand(PlayingCard cardDealt)
		{
		    if (this.playingCardCount >= handSize)
		    {
                throw new ArgumentException("Too many cards");
		    }

		    this.cards[this.playingCardCount] = cardDealt;
		    this.playingCardCount++;
		}

		// CODEREVIEW: I'd call this method something other than serialize. Perhaps override ToString(). Serialise tends to refer
		// CODEREVIEW:  to translating data between formats. This method, though, outputs a human-readable display text.
		
		// CODEREVIEW: A proper serialisation could be useful though...
		public string Serialize()
		{
			string result = "";
			foreach (PlayingCard card in this.cards)
			{
                result += $"{card.ToString()}\n";
            }

			return result;
		}

	    public static void SetHandSize(int i)
	    {
	        handSize = i;

	    }

	    public static int GetHandSize() => handSize;

	}
}