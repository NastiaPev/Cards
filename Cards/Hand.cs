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