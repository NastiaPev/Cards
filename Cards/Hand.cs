using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Cards
{
    class PlayerHand : Hand
    {
        public Player PlayerName { get; set; }

        public PlayerHand(Player player, int handSize) : base(handSize) 
        {
            PlayerName = player;
        }

        /// <summary>
        /// Returns an XML representation of the hand, containing 'player' and 'card' elemnts.
        /// Cards, in turn, are serialized according to their serialization
        /// </summary>
        /// <returns>XML representation of the hand</returns>
        public override XElement Serialize()
        {
            XElement playerHandElement = base.Serialize();
            playerHandElement.Add(new XAttribute("player", PlayerName.Name()));
            return playerHandElement;
        }

    }

	class Hand
	{
		public int HandSize { get; set; }
	    private List<PlayingCard> cards = new List<PlayingCard>();

        /// <summary>
        /// </summary>
        /// <param name="handSize">Size of the hand to be constructed</param>
	    public Hand(int handSize)
	    {
	        HandSize = handSize;
	    }

	    /// <summary>
        /// Empties the hand and redeals from the provided pack
        /// </summary>
        /// <param name="pack"></param>
	    public void DealFromPack(Pack pack)
	    {
            cards.Clear();
            for (int j = 0; j < HandSize; j++)
	        {
	            PlayingCard card = pack.DealCardFromPack();
	            AddCardToHand(card);
	        }
	    }

        //TODO
        /// <summary>
        /// Adds a playing card to hand
        /// </summary>
        /// <param name="cardDealt">The card being added
        /// </param>
        public void AddCardToHand(PlayingCard cardDealt)
		{
		    if (cards.Count >= HandSize)
		    {
                throw new InvalidOperationException("Attemp to add card to a full hand. ");
		    }

		    cards.Add(cardDealt); 
		}

		public override string ToString()
		{
		    return cards.Aggregate("", (current, card) => current + $"{card}\n");
		}

	    public virtual XElement Serialize()
	    {
	        return new XElement("hand",
	            cards.Select(card => card.Serialize()));
	    }

    }
}