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
        private Pack pack = null;
        private List<Hand> hands = new List<Hand>();


        public MainPage()
        {
            this.InitializeComponent();
            pack = new Pack();
            Hand.HandSize = 13;
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
                hands = new List<Hand>();
                this.Tag = "Deal";
                for (int i = 0; i < NumHands; i++)
                {
                    hands.Add(new Hand());
                    for (int j = 0; j < Hand.HandSize; j++)
                    {
                        PlayingCard card = pack.DealCardFromPack();
                        hands[i].AddCardToHand(card);
                    }
                }

                north.Text = hands[0].ToString();
                hands[0].PlayerName = Player.North;

                south.Text = hands[1].ToString();
                hands[1].PlayerName = Player.South;

                east.Text = hands[2].ToString();
                hands[2].PlayerName = Player.East;

                west.Text = hands[3].ToString();
                hands[3].PlayerName = Player.West;

                pack = new Pack(pack.GetNumSuits(), pack.GetNumCards());
                CardDealUserInteraction cardDealUserInteraction = new CardDealUserInteraction(((Button) sender).Tag.ToString(), DateTime.Now, hands);
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

                if (i > 0 && i < 5)
                {
                    pack.SetNumSuits(i);
                    SuitMistake.Text = "";
                    AdjustHandSize(i * pack.GetNumCards() / 4);
                }
                else
                {
                    Suits.Text = "4";
                    SuitMistake.Text = "Please enter a valid number";
                    pack.SetNumSuits(4);
                    i = 4;
                    AdjustHandSize(4 * pack.GetNumCards() / 4);
                }

                NumericalUserInteraction numericalUserInteraction = new NumericalUserInteraction(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(numericalUserInteraction);
            }
            catch (FormatException fEx)
            {
                Suits.Text = "4";
                SuitMistake.Text = "Please enter a valid number";
                pack.SetNumSuits(4);
                AdjustHandSize(4 * pack.GetNumCards() / 4);
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

                if (i > 0 && i < 14)
                {
                    pack.SetNumCards(i);
                    AdjustHandSize(i * pack.GetNumSuits() / 4);
                }
                else
                {
                    Cards.Text = "13";
                    CardMistake.Text = "Please enter a valid number";
                    i = 13;
                    pack.SetNumCards(13);
                    AdjustHandSize(13 * pack.GetNumSuits() / 4);
                }

                NumericalUserInteraction numericalUserInteraction = new NumericalUserInteraction(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(numericalUserInteraction);

            }
            catch (FormatException fEx)
            {
                Cards.Text = "13";
                CardMistake.Text = "Please enter a valid number";
                pack.SetNumCards(13);
                AdjustHandSize(13 * pack.GetNumSuits() / 4);

            }
            

        }
        /// <summary>
        /// Cheks whether it is possible to adjust the hand size to the imputed number. If it is, adjusts
        /// the size of the hadn. Otherwise adjusts the size of the hand to maximum possible given the size of the deck 
        /// and paassively-aggressively swears to the user. 
        /// </summary>
        /// <param name="size">the size hand is being adjusted to</param>
        /// <returns>new hand size</returns>
        private int AdjustHandSize(int size)
        {

            if (size >= 0 && size <= pack.GetNumSuits() * pack.GetNumCards() / 4)
            {
                HandSize.Text = size.ToString();
                Hand.HandSize = size;
                HandSizeMistake.Text = "";
                return size;
            }

            size = pack.GetNumSuits() * pack.GetNumCards() / 4;
            HandSize.Text = $"{size}";
            Hand.HandSize = size;
            HandSizeMistake.Text = "Please adjust hand size correctly";
            return size;
        }

        /// <summary>
        /// An event happening when the button adjusting the hand size is clicked
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
                AdjustHandSize(pack.GetNumSuits() * pack.GetNumCards() / 4);
                HandSizeMistake.Text = "Please enter something making sense";
            }
            

        }

    }
}
