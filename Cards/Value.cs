
using System.Collections.Generic;
using Cards;

namespace Cards
{
    enum Value { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
}
static class CardValueSerialiser
{

    private static Dictionary<Value, string> CardValueNames = new Dictionary<Value, string> {
        {Value.Two, "two" },
        {Value.Three, "three" },
        {Value.Four, "four" },
        {Value.Five, "five" },
        {Value.Six, "six" },
        {Value.Seven, "seven" },
        {Value.Eight, "eight" },
        {Value.Nine, "nine" },
        {Value.Ten, "ten" },
        {Value.Jack, "jack" },
        {Value.Queen, "queen" },
        {Value.King, "king" },
        {Value.Ace, "ace" }
    };

    /// <summary>
    /// An XML-suitable name for the value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Name(this Value value)
    {
        return CardValueNames[value];
    }
}