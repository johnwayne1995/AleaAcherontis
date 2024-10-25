using System.Collections.Generic;
using System.Linq;
namespace Managers
{
    public class TexasLogic
    {
        private readonly PokerHand _pokerHand;

        public TexasLogic()
        {
            _pokerHand = new PokerHand();
        }
        
        public PokerHand AnalyzeHandStr(string handStr)
        {
            _pokerHand.Cards.Clear();

            Rank curRank = Rank.Ace;
            char curRankChar = 'A';
            
            for (int i = 0; i < handStr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    //面数
                    curRankChar = handStr[i];
                    curRank = ConvertStrToRank(curRankChar);
                }
                else
                {
                    var suitValue = handStr[i];
                    var curSuit = ConvertStrToSuit(suitValue);
                    var card = new Card(curSuit, curRank, curRankChar.ToString() + suitValue);
                    _pokerHand.Cards.Add(card);
                }
            }
            
            return _pokerHand;
        }

        public static Rank ConvertStrToRank(char str)
        {
            switch (str)
            {
                case '2':
                    return Rank.Two;
                case 'A':
                    return Rank.Ace;
                case 'K':
                    return Rank.King;
                case 'Q':
                    return Rank.Queen;
                case 'J':
                    return Rank.Jack;
                case 'T':
                    return Rank.Ten;
                case '9':
                    return Rank.Nine;
                case '8':
                    return Rank.Eight;
                case '7':
                    return Rank.Seven;
                case '6':
                    return Rank.Six;
                case '5':
                    return Rank.Five;
                case '4':
                    return Rank.Four;
                case '3':
                    return Rank.Three;
            }
            
            return Rank.Three;
        }
        
        private Suit ConvertStrToSuit(char str)
        {
            switch (str)
            {
                case 's':
                    return Suit.Spades;
                case 'h':
                    return Suit.Hearts;
                case 'd':
                    return Suit.Diamonds;
                case 'c':
                    return Suit.Clubs;
            }
            
            return Suit.Spades;
        }
    }

    public class PokerHand
    {
        public List<Card> Cards { get; set; } = new List<Card>();
        public CaseEnum HandCase { get; set; } = CaseEnum.HighCard;
        public List<Card> HandDetails { get; set; } = new List<Card>();

        public void EvaluateHand()
        {
            // 排序手牌
            Cards = Cards.OrderBy(card => card.Rank).ToList();
            HandDetails.Clear();

            if (Cards.Count == 0)
            {
                HandCase = CaseEnum.None;
                return;
            }
            
            // 检查牌型
            if (IsRoyalFlush()) HandCase = CaseEnum.Flush;
            else if (IsStraightFlush()) HandCase = CaseEnum.StraightFlush;
            else if (IsFourOfAKind()) HandCase = CaseEnum.FourOfAKind;
            else if (IsFullHouse()) HandCase = CaseEnum.FullHouse;
            else if (IsFlush()) HandCase = CaseEnum.Flush;
            else if (IsStraight()) HandCase = CaseEnum.Straight;
            else if (IsThreeOfAKind()) HandCase = CaseEnum.ThreeOfAKind;
            else if (IsTwoPair()) HandCase = CaseEnum.TwoPair;
            else if (IsOnePair()) HandCase = CaseEnum.OnePair;
            else HandCase = CaseEnum.HighCard;
        }

        /// <summary>
        /// 同花
        /// </summary>
        /// <returns></returns>
        private bool IsRoyalFlush()
        {
            return IsStraightFlush() && Cards.First().Rank == Rank.Ten && Cards.Last().Rank == Rank.Ace;
        }

        /// <summary>
        /// 同花顺
        /// </summary>
        /// <returns></returns>
        private bool IsStraightFlush()
        {
            return IsFlush() && IsStraight();
        }

        /// <summary>
        /// 四条
        /// </summary>
        /// <returns></returns>
        private bool IsFourOfAKind()
        {
            var fourOfAKind = Cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 4);
            if (fourOfAKind != null)
            {
                HandDetails = fourOfAKind.ToList();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 葫芦
        /// </summary>
        /// <returns></returns>
        private bool IsFullHouse()
        {
            var threeOfAKind = Cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 3);
            var pair = Cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 2);
            if (threeOfAKind != null && pair != null)
            {
                HandDetails = threeOfAKind.Concat(pair).ToList();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 同花
        /// </summary>
        /// <returns></returns>
        private bool IsFlush()
        {
            var flush = Cards.GroupBy(card => card.Suit).FirstOrDefault(group => group.Count() == 5);
            if (flush != null)
            {
                HandDetails = flush.ToList();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 顺子
        /// </summary>
        /// <returns></returns>
        private bool IsStraight()
        {
            if (Cards.Select(card => (int)card.Rank).Distinct().Count() == 5 && Cards.Max(card => (int)card.Rank) - Cards.Min(card => (int)card.Rank) == 4)
            {
                HandDetails = Cards.ToList();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 三条
        /// </summary>
        /// <returns></returns>
        private bool IsThreeOfAKind()
        {
            var threeOfAKind = Cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 3);
            if (threeOfAKind != null)
            {
                HandDetails = threeOfAKind.ToList();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 两对
        /// </summary>
        /// <returns></returns>
        private bool IsTwoPair()
        {
            var pairs = Cards.GroupBy(card => card.Rank).Where(group => group.Count() == 2).ToList();
            if (pairs.Count == 2)
            {
                HandDetails = pairs.SelectMany(group => group).ToList();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 一对
        /// </summary>
        /// <returns></returns>
        private bool IsOnePair()
        {
            var pair = Cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 2);
            if (pair != null)
            {
                HandDetails = pair.ToList();
                return true;
            }
            return false;
        }
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public string CardId { get; set; }

        public Card(Suit suit, Rank rank, string cardId)
        {
            Suit = suit;
            Rank = rank;
            CardId = cardId;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
    
    public enum CaseEnum
    {
        None = 9,
        StraightFlush = 8, // 皇家同花顺&同花顺
        FourOfAKind   = 7, // 四条
        FullHouse     = 6, // 葫芦
        Flush         = 5, // 同花
        Straight      = 4, // 顺子
        ThreeOfAKind  = 3, // 三条
        TwoPair       = 2, // 两对
        OnePair       = 1, // 一对
        HighCard      = 0  // 高牌
    }

    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two = 13,
        Ace = 12,
        Three = 1,
        Four = 2,
        Five = 3,
        Six = 4,
        Seven = 5,
        Eight = 6,
        Nine = 7,
        Ten = 8,
        Jack = 9,
        Queen = 10,
        King = 11,
    }
}
