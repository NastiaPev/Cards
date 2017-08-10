using System;
using System.Collections.Generic;
using System.IO;
                                                                                                                                                                            using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.ApplicationInsights.Extensibility.Implementation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

//If anuthorized exception occures - go to file explorer @Cards\Cards\bin\x86\Debug\AppX (@ is local folder of the project).
//Right click go to Properties -> Security and grant full accesss to accounts represesnting the application

namespace Cards
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const int NumHands = 4;
        private const int maxCardsInSuit = 13;
        private const int maxSuits = 4;

        private Pack pack = null;
        private List<Hand> hands = new List<Hand>();
        private int handSize = 13;


        public MainPage()
        {
            this.InitializeComponent();
            pack = new Pack();
        }

        /// <summary>
        /// An event happening when the "Deal" button is clicked. Deals cards to four hands.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DealClick(object sender, RoutedEventArgs e)
        {
         

            try
            {
                hands = new List<Hand>(); //discarding old hands which are left from previous deal
                this.Tag = "Deal";

                Player[] allPlayers = {Player.North, Player.South, Player.East, Player.West};
                foreach (Player player in allPlayers)
                {
                    PlayerHand hand = new PlayerHand(player, handSize);
                    hand.DealFromPack(pack);
                    hands.Add(hand);
                }

                north.Text = hands[0].ToString();
                south.Text = hands[1].ToString();
                east.Text = hands[2].ToString();
                west.Text = hands[3].ToString();

                pack = new Pack(pack.GetNumSuits(), pack.GetNumCards());
                CardDealUserInteraction cardDealUserInteraction = new CardDealUserInteraction(((Button) sender).Tag.ToString(), DateTime.Now, hands); //Ask Andy - I hope I used inheritance right
                EventFileWriter.TheWriter.WriteIntoFile(cardDealUserInteraction);

            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message, "Error");
                msg.ShowAsync();
            }
        }

        /// <summary>
        /// Adjusts the number of suits in the deck.
        /// </summary>
        /// <param name="sender">Button causing the event</param>
        /// <param name="e"></param>
        private void SuitClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Tag = "Suit";
                int i = Int32.Parse(Suits.Text);

                if (i > 0 && i <= maxSuits)
                {
                    pack.SetNumSuits(i);
                    SuitMistake.Text = "";
                    AdjustHandSize(i * pack.GetNumCards() / NumHands);
                }
                else
                {
                    Suits.Text = $"{maxSuits}";
                    SuitMistake.Text = "Please enter a valid number";
                    pack.SetNumSuits(maxSuits);
                    i = maxSuits;
                    AdjustHandSize(maxSuits * pack.GetNumCards() / NumHands);
                }

                NumericalUserInteraction numericalUserInteraction = new NumericalUserInteraction(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(numericalUserInteraction);
            }
            catch (FormatException fEx)
            {
                Suits.Text = $"{maxSuits}";
                SuitMistake.Text = "Please enter a valid number";
                pack.SetNumSuits(maxSuits);
                AdjustHandSize(maxSuits * pack.GetNumCards() / NumHands);
            }
        }

        /// <summary>
        /// Adjusts the number of cards in each suit of the deck
        /// </summary>
        /// <param name="sender">Button causing the event</param>
        /// <param name="e"></param>
        private void CardsClick(object sender, RoutedEventArgs e)
        {

            try
            {
                this.Tag = "CardsInSuit";
                int i = Int32.Parse(Cards.Text);

                if (i > 0 && i <= maxCardsInSuit)
                {
                    pack.SetNumCards(i);
                    AdjustHandSize(i * pack.GetNumSuits() / NumHands);
                }
                else
                {
                    Cards.Text = $"{maxCardsInSuit}";
                    CardMistake.Text = "Please enter a valid number";
                    i = maxCardsInSuit;
                    pack.SetNumCards(maxCardsInSuit);
                    AdjustHandSize(maxCardsInSuit * pack.GetNumSuits() / NumHands);
                }

                NumericalUserInteraction numericalUserInteraction = new NumericalUserInteraction(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(numericalUserInteraction);

            }
            catch (FormatException fEx)
            {
                Cards.Text = $"{maxCardsInSuit}";
                CardMistake.Text = "Please enter a valid number";
                pack.SetNumCards(maxCardsInSuit);
                AdjustHandSize(maxCardsInSuit * pack.GetNumSuits() / NumHands);

            }
            

        }
        /// <summary>
        /// Cheks whether it is possible to adjust the hand sizeUserInput to the imputed number. If it is, adjusts
        /// the sizeUserInput of the hadn. Otherwise adjusts the sizeUserInput of the hand to maximum possible given the sizeUserInput of the deck 
        /// and paassively-aggressively swears to the user. 
        /// </summary>
        /// <param name="sizeUserInput">the sizeUserInput hand is being adjusted to</param>
        /// <returns>new hand sizeUserInput</returns>
        private int AdjustHandSize(int sizeUserInput)
        {

            if (sizeUserInput >= 0 && sizeUserInput <= pack.GetNumSuits() * pack.GetNumCards() / NumHands)
            {
                HandSize.Text = sizeUserInput.ToString();
                handSize = sizeUserInput;
                HandSizeMistake.Text = "";
                return handSize;
            }

            handSize = pack.GetNumSuits() * pack.GetNumCards() / NumHands;
            HandSize.Text = $"{handSize}";
            HandSizeMistake.Text = "Please adjust hand sizeUserInput correctly";
            return handSize;
        }

        /// <summary>
        /// An event happening when the button adjusting the hand sizeUserInput is clicked
        /// </summary>
        /// <param name="sender">Button causing the event</param>
        /// <param name="e"></param>
        private void HandSizeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Tag = "HandSize";
                int i = Int32.Parse(HandSize.Text);
                i = AdjustHandSize(i);

                NumericalUserInteraction numericalUserInteraction = new NumericalUserInteraction(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(numericalUserInteraction);
            }
            catch (FormatException fEx)
            {
                AdjustHandSize(pack.GetNumSuits() * pack.GetNumCards() / NumHands);
                HandSizeMistake.Text = "Please enter something making sense";
            }
            

        }

    }
}
