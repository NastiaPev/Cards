
using System.Collections.Generic;
using Cards;

namespace Cards
{
    enum Suit { Clubs, Diamonds, Hearts, Spades }
}

static class CardSuitSerialiser
{

    private static Dictionary<Suit, string> SuitNames = new Dictionary<Suit, string> {
        {Suit.Clubs, "clubs"},
        {Suit.Diamonds, "diamonds"},
        {Suit.Hearts, "hearts"},
        {Suit.Spades, "spades"}
    };

    /// <summary>
    /// An XML-suitable name for the suit
    /// </summary>
    /// <param name="suit"></param>
    /// <returns></returns>
    public static string Name(this Suit suit)
    {
        return SuitNames[suit];
    }
}