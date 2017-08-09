using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Cards
{
	class Hand
	{
		// CODEREVIEW: 1. This should be called MaxHandSize.
		// CODEREVIEW: 2. What if we want to have different max hand sizes for different instances of Hand objects?
		// CODEREVIEW:		In that case, because HandSize is static, we can't do it. This should perhaps be changed to
		// CODEREVIEW:		an instance field (or property) which is set upon construction of a Hand object.
		public static int HandSize { get; set; }
	    private List<PlayingCard> cards = new List<PlayingCard>();

		// CODEREVIEW: You could get rid of the need for the "None" player, by having this set by the constuctor of the Hand object.
		// CODEREVIEW: That way, it will be impossible to create a Hand without specifying the Player.
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
				// CODEREVIEW: Triple-slash comments should only be used when documenting things. Is this comment needed?
				// CODEREVIEW: Have a look at the other constructors of ArgumentException. Some of them could allow you to provide more information
			    // CODEREVIEW: which would be good. In particular, try one with a "paramName" parameter - then try to use the "nameof()" function for that...
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
        ///
		/// CODEREVIEW: This comment should just say that the cards are serialised according to their serialisation, or something.
        /// CODEREVIEW: It's not the job of the Hard class to know how the Card class serialises itself.
        /// Card, in turn, contains 'suit' and 'value' elements
        /// </summary>
        /// <returns>XML representation of the hand</returns>
	    public XElement Serialize()
        {
            return new XElement("hand",
				// CODEREVIEW: This is a bit dangerous. When you put a Player object into the XAttribute constructor, it will call "ToString" on it.
				// CODEREVIEW: This will return the code name of the enum. E.g. "North". This code name should be kept distinct from the serialised name.
				// CODEREVIEW: What if someone used resharper to casually rename the "North" hand to "Up"? Suddenly the serialisation changes too.
	            // CODEREVIEW: Instead, we need some separate storage for the serialised names for Players... Perhaps keep a static dictionary here of Player to string?
				new XAttribute("player", PlayerName),
                cards.Select(card =>
                    new XElement("card", card.Serialize())));
        }
	}
}