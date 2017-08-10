using System;
using System.Numerics;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Pickers.Provider;

namespace Cards
{	
	class Pack
	{
	    private const int maxCardsInSuit = 13;
	    private const int maxSuits = 4;

        private int numSuits = 4;
        private int cardsPerSuit = 13;
        private PlayingCard[,] cardPack;
        private Random randomCardSelector = new Random();

        public Pack()
        {
            
            this.cardPack = new PlayingCard[numSuits,cardsPerSuit];

            for (Suit i = Suit.Clubs; (int) i < numSuits; i++)
            {
                for (Value j = Value.Ace; (int) j >= maxCardsInSuit-cardsPerSuit; j--)
                {
                    this.cardPack[(int)i,(int)j] = new PlayingCard(i,j); 
                }
            }
        }

	    public Pack(int suits, int cards):this()
	    {
            SetNumCards(cards);
            SetNumSuits(suits);
	    }

	    public void SetNumSuits(int num)
	    {
            if (num > 0 && num <= maxSuits)
            {
                numSuits = num;
            }
	        
	    }

	    public void SetNumCards(int num)
	    {
	        if (num > 0 && num <= maxCardsInSuit)
	        {
	            cardsPerSuit = num;
	        }

	    }

	    public int GetNumSuits() => this.numSuits;

	    public int GetNumCards() => this.cardsPerSuit;


        public PlayingCard DealCardFromPack()
        {
            Suit suit = (Suit) randomCardSelector.Next(0,this.numSuits);

            while (this.IsSuitEmpty(suit))
            {
                suit = (Suit)randomCardSelector.Next(0, this.numSuits);
            }

			Value value = (Value) randomCardSelector.Next(maxCardsInSuit - cardsPerSuit, maxCardsInSuit); 

            while (this.IsCardAlreadyDealt(suit, value))
            {
                value = (Value)randomCardSelector.Next(maxCardsInSuit - cardsPerSuit, maxCardsInSuit);
            }

            PlayingCard card = this.cardPack[(int) suit, (int) value];
            this.cardPack[(int) suit, (int) value] = null;
            return card;

            
        }

        private bool IsSuitEmpty(Suit suit)
        {
            bool flag = true;

            for (Value j = Value.Ace; (int) j >= maxCardsInSuit - cardsPerSuit; j--)
            {
                if (!IsCardAlreadyDealt(suit, j))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        private bool IsCardAlreadyDealt(Suit suit, Value value)
        {
            bool flag = this.cardPack[(int) suit, (int) value] == null;
            return flag;
        }
    }
}