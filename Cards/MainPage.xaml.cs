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

namespace Cards
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const int NumHands = 4;
        private Pack pack = null;
        private Hand[] hands = new Hand[NumHands];






        public MainPage()
        {
            this.InitializeComponent();

            pack = new Pack();

            Hand.SetHandSize(13);





        }

        private void DealClick(object sender, RoutedEventArgs e)
        {


            try
            {
                this.Tag = "Deal";
                for (int i = 0; i < NumHands; i++)
                {
                    hands[i] = new Hand();

                    for (int j = 0; j < Hand.GetHandSize(); j++)
                    {
                        PlayingCard card = pack.DealCardFromPack();
                        hands[i].AddCardToHand(card);
                    }
                }

                north.Text = hands[0].Serialize();
                south.Text = hands[1].Serialize();
                east.Text = hands[2].Serialize();
                west.Text = hands[3].Serialize();

                //Add hands to the event in the same order as events are printed as north to west values are hard-coded in the constructor

                pack = new Pack(pack.GetNumSuits(), pack.GetNumCards());

                EventWithDeal eventWithDeal = new EventWithDeal(((Button) sender).Tag.ToString(), DateTime.Now, hands[0],
                    hands[1], hands[2], hands[3]);
                EventFileWriter.TheWriter.WriteIntoFile(eventWithDeal);

            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message, "Error");
                msg.ShowAsync();
            }
            
            

            
        }


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

                EventWithBox eventWithBox = new EventWithBox(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(eventWithBox);
            }
            catch (FormatException fEx)
            {
                Suits.Text = "4";
                SuitMistake.Text = "Please enter a valid number";
                pack.SetNumSuits(4);

                AdjustHandSize(4 * pack.GetNumCards() / 4);
            }
           

        }

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

                EventWithBox eventWithBox = new EventWithBox(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(eventWithBox);

            }
            catch (FormatException fEx)
            {
                Cards.Text = "13";
                CardMistake.Text = "Please enter a valid number";
                pack.SetNumCards(13);

                AdjustHandSize(13 * pack.GetNumSuits() / 4);

            }
            

        }

        private int AdjustHandSize(int size)
        {

            if (size >= 0 && size <= pack.GetNumSuits() * pack.GetNumCards() / 4)
            {
                HandSize.Text = size.ToString();
                Hand.SetHandSize(size);
                HandSizeMistake.Text = "";
                return size;
            }

            size = pack.GetNumSuits() * pack.GetNumCards() / 4;
            HandSize.Text = $"{size}";
            Hand.SetHandSize(size);
            HandSizeMistake.Text = "Please adjust hand size";
            return size;
        }

        private void HandSizeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Tag = "HandSize";
                int i = Int32.Parse(HandSize.Text);
                i = AdjustHandSize(i);

                EventWithBox eventWithBox = new EventWithBox(((Button)sender).Tag.ToString(), DateTime.Now, i);
                EventFileWriter.TheWriter.WriteIntoFile(eventWithBox);
            }
            catch (FormatException fEx)
            {
                AdjustHandSize(pack.GetNumSuits() * pack.GetNumCards() / 4);
                HandSizeMistake.Text = "Please enter something making sense";
            }
            

        }


        //public void Documentation()
        //{
        //    //write a new line to the file to record each time the OK button is clicked
        //    //each line should contain the time of the event

        //    string testString = "face";
        //    string fileLocation = @"C:\myfile.txt";
        //    using (FileStream stream = new FileStream(fileLocation, FileMode.OpenOrCreate))
        //    using (StreamWriter writer = new StreamWriter(stream))
        //    {
        //        writer.WriteLine(testString);
        //    }


        //}


    }
}
