using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Cards
{
	class Hand
	{
	    public static int HandSize { get; set; }
	    private List<PlayingCard> cards = new List<PlayingCard>();
	    public Player PlayerName { get; set; } = Player.None;

        /// <summary>
        /// Adds a playing card to hand
        /// </summary>
        /// <param name="cardDealt">The card being added
        /// </param>
	    public void AddCardToHand(PlayingCard cardDealt)
		{
		    if (cards.Count >= HandSize)
		    {
                throw new ArgumentException("Too many cards"); /// refresh the hand
		    }

		    cards.Add(cardDealt); 
		}

		public override string ToString()
		{
		    return cards.Aggregate("", (current, card) => current + $"{card}\n");
		}

        /// <summary>
        /// Returns an XML representation of the hand, containing 'player' and 'card' elemnts.
        /// Card, in turn, contains 'suit' and 'value' elements
        /// </summary>
        /// <returns>XML representation of the hand</returns>
	    public XElement Serialize()
        {
            return new XElement("hand",
                new XAttribute("player", PlayerName),
                cards.Select(card =>
                    new XElement("card", card.Serialize())));
        }
	}
}