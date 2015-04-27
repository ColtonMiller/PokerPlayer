using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    //make list of rank
    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    //make suits
    public enum Suit
    {
        Club = 1,
        Diamond,
        Heart,
        Spade
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
    class PokerPlayer
    {
        //make a deck
        public Deck pokerDeck = new Deck();
        
        //make empty list for hand
        List<Card> hand1 = new List<Card> { }; 

        /// <summary>
        /// takes in 5 cards 
        /// </summary>
        /// <returns></returns>
        public void DrawHand( List<Card> hand)
        {
            //return the list of cards
            this.hand1 = hand;
        }
        // Enum of different hand types
        public enum HandType
        {
            HighCard = 1,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush

        }
        // Rank of hand that player holds
        public HandType HandRank
        {
            get
            {
                if (this.HasRoyalFlush())
                {
                    return HandType.RoyalFlush;
                }
                else if (this.HasStraightFlush())
                {
                    return HandType.StraightFlush;
                }
                else if (this.HasFourOfAKind())
                {
                    return HandType.FourOfAKind;
                }
                else if (this.HasFullHouse())
                {
                    return HandType.FullHouse;
                }
                else if (this.HasFlush())
                {
                    return HandType.Flush;
                }
                else if (this.HasStraight())
                {
                    return HandType.Straight;
                }
                else if (this.HasThreeOfAKind())
                {
                    return HandType.ThreeOfAKind;
                }
                else if (this.HasTwoPair())
                {
                    return HandType.TwoPair;
                }
                else if (this.HasPair())
                {
                    return HandType.OnePair;
                }
                else
                {
                    return HandType.HighCard;
                }
            }
        }
        // Constructor that isn't used
        public PokerPlayer() { }
        public bool HasPair()
        {


            return hand1.GroupBy(x => x.CardRank).Where(x => x.Count() == 2).Count() == 1;
        }
        public bool HighCard()
        {
            return hand1.Distinct().Count() == 5;
        }
        public bool HasTwoPair()
        {
            return hand1.GroupBy(x => x.CardRank).Where(x => x.Count() == 2).Count() == 2;
        }
        public bool HasThreeOfAKind()
        {
            //group by rank and where count is 3 and count is one return true
            return hand1.GroupBy(x => x.CardRank).Where(x => x.Count() == 3).Count() == 1;
        }
        public bool HasStraight()
        {
            //create temp list to hold hand1
            List<Card> tempStraight = new List<Card>{};
            tempStraight = (List<Card>)hand1.OrderBy(x => (int)x.CardRank).ToList();
            //loop through hand1 rank
            for (int i = 0; i < tempStraight.Count() - 1; i++)
            {
                //take value of card rank and next card rank
                int currentRank = (int)tempStraight[i].CardRank;
                int nextRank = (int)tempStraight[i + 1].CardRank;
                //if next rank equals current rank plus 1 return true'
                if (nextRank == currentRank + 1)
                {
                    if (i == 3)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }
        public bool HasFlush()
        {
            return hand1.GroupBy(x => x.CardSuit).Where(x => x.Count() == 5).Count() == 1;
        }
        public bool HasFullHouse()
        {
            return hand1.GroupBy(x => x.CardSuit).Where(x => x.Count() == 2 && x.Count() == 3).Count() == 2;
        }
        public bool HasFourOfAKind()
        {
            return hand1.GroupBy(x => x.CardRank).Where(x => x.Count() == 4).Count() == 1;
        }
        public bool HasStraightFlush()
        {
            List<Card> tempHand = new List<Card> { };
            tempHand = (List<Card>)hand1.GroupBy(x => x.CardSuit).First().OrderBy(y => (int)y.CardRank).ToList();
            
            //loop through tempHand rank
            for (int i = 0; i < (tempHand.Count() - 1); i++)
            {
                //take value of card rank and next card rank
                int currentRank = (int)tempHand[i].CardRank;
                int nextRank = (int)tempHand[i + 1].CardRank;
                //if next rank equals current rank plus 1 return true
                if (nextRank == currentRank + 1)
                {
                    if (i == 3)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }
        public bool HasRoyalFlush()
        {
            List<Card> tempHand = new List<Card> { };
            tempHand = (List<Card>)hand1.GroupBy(x => x.CardSuit).First().OrderBy(y => (int)y.CardRank).ToList();
            //loop through tempHand rank
            for (int i = 0; i < hand1.Count(); i++)
            {
                //take value of card rank 
                int currentRank = (int)tempHand[i].CardRank;
                //currentRank equal i + 10
                if (currentRank == i+10)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
    //Guides to pasting your Deck and Card class

    //  *****Deck Class Start*****
    class Deck
    {
        //create a random
        Random rng = new Random();
        //set properties
        public List<Card> DeckOfCards { get; set; }
        public List<Card> DiscardedCards { get; set; }
        /// <summary>
        /// Shuffles deck
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();
            List<Card> tempShuffleList = new List<Card> { };
            while (DeckOfCards.Count() > 0)
            {
                Card currentCard = DeckOfCards[rng.Next(0, DeckOfCards.Count())];
                tempShuffleList.Add(currentCard);
                DeckOfCards.Remove(currentCard);
            }
            DeckOfCards = tempShuffleList;
        }
        /// <summary>
        /// deals number of cards
        /// </summary>
        /// <param name="numberOfCards">number of cards</param>
        /// <returns></returns>
        public List<Card> Deal(int numberOfCards)
        {
            List<Card> tempDeal = new List<Card> { };
            for (int i = 0; i < numberOfCards; i++)
            {
                tempDeal.Add(DeckOfCards[i]);
                DeckOfCards.Remove(DeckOfCards[i]);

            }
            return tempDeal;
        }
        /// <summary>
        /// discard one card
        /// </summary>
        /// <param name="card">a card</param>
        /// <returns></returns>
        public List<Card> Discard(Card card)
        {
            DiscardedCards.Add(card);
            return DiscardedCards;
        }
        /// <summary>
        /// discards multiple cards
        /// </summary>
        /// <param name="cards">liist of cards</param>
        /// <returns></returns>
        public List<Card> Discard(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                DiscardedCards.Add(card);
            }
            return DiscardedCards;
        }

        public Deck()
        {
            this.DeckOfCards = new List<Card> { };
            //set discarded to empty
            this.DiscardedCards = new List<Card> { };
            //set standard 
            for (int i = 2; i < 15; i++)
            {
                for (int x = 1; x < 5; x++)
                {
                    Card newCard = new Card(i, x);
                    DeckOfCards.Add(newCard);

                }
            }
        }
    }
    //  *****Deck Class End*******

    //  *****Card Class Start*****
    class Card
    {
        //make properties
        public Rank CardRank { get; set; }
        public Suit CardSuit { get; set; }





       
        public Card()
        {

        }
        public Card(int rank, int suit)
        {
            this.CardRank = (Rank)rank;
            this.CardSuit = (Suit)suit;
        }
    }
    //  *****Card Class End*******
}
