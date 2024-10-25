using System.Collections.Generic;
using Config;
using UnityEngine;
namespace Managers
{
    public class FightCardManager : TGameManager<FightCardManager>
    {
        /// <summary>
        /// 最大出牌数
        /// </summary>
        public const int CMAX_SEND_CARD_COUNT = 5;
        /// <summary>
        /// 最大持有手牌数
        /// </summary>
        public const int CMAX_SAVE_CARD_COUNT = 8;

        /// <summary>
        /// 抽卡堆
        /// </summary>
        public List<NormalCard> CardList;
        /// <summary>
        /// 弃牌堆
        /// </summary>
        public List<NormalCard> UsedCardList;
        /// <summary>
        /// 手牌
        /// </summary>
        public List<NormalCard> UsingCardList;
        /// <summary>
        /// 待打出的牌
        /// </summary>
        public List<NormalCard> CardListWaitToSend;

        private Dictionary<string, int> _cacheCardDamage = new Dictionary<string, int>();
        private CardCaseConfig _cardCaseConfig;
        
        private TexasLogic _texasLogic;
        private PokerHand _pokerHand;

        protected override void OnAwake()
        {
            _texasLogic = new TexasLogic();
            
            CardList = new List<NormalCard>();
            UsedCardList = new List<NormalCard>();
            UsingCardList = new List<NormalCard>();
            //定义临时集合
            List<NormalCard> tempList = new List<NormalCard>();
            
            _cardCaseConfig = Resources.Load<CardCaseConfig>("Configs/CardConfig/CardsCaseConfig");
            
            var allNormalCard = Resources.Load<NormalCardsConfig>("Configs/CardConfig/NormalCardsConfig");

            for (int i = 0; i < allNormalCard.NormalCards.Count; i++)
            {
                var card = allNormalCard.NormalCards[i];
                _cacheCardDamage.Add(card.cardId, card.cardPoint);
            }
            
            tempList.AddRange(allNormalCard.NormalCards);
            Shuffle(tempList);
            CardList.AddRange(tempList);
        }

        protected override void OnEnterGame()
        {
            base.OnEnterGame();
            if (CardListWaitToSend != null)
            {
                CardListWaitToSend.Clear();
            }
            _pokerHand = null;
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        private void Shuffle(List<NormalCard> allCards)
        {
            DoShuffle(allCards, 52);
        }
        
        public void DoShuffle(List<NormalCard> card, int n)
        {
            System.Random rand = new System.Random();
            for (int i = 0; i < n; i++)
            {
                int r = i + rand.Next(52 - i);
                (card[r], card[i]) = (card[i], card[r]);
            }
        }
        
        //是否有卡
        public bool HasCard()
        {
            return CardList.Count > 0;
        }
        
        //抽卡
        public NormalCard DrawCard()
        {
            var cardConfig = CardList[CardList.Count - 1];
            CardList.RemoveAt(CardList.Count - 1);
            return cardConfig;
        }
        
        public void DrawCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var card = DrawCard();
                UsingCardList.Add(card);
            }
            SortUsingCards();
        }
        
        /// <summary>
        /// 将指定牌设置为待打出
        /// </summary>
        /// <param name="cardConfig"></param>
        /// <returns></returns>
        public bool SetCardToWaitSend(NormalCard cardConfig)
        {
            if (CardListWaitToSend == null)
                CardListWaitToSend = new List<NormalCard>();

            if (CardListWaitToSend.Count == CMAX_SEND_CARD_COUNT)
            {
                //TODO 提示待打出牌已满
                return false;
            }
            
            CardListWaitToSend.Add(cardConfig);
            var handStr = string.Empty;
            for (int i = 0; i < CardListWaitToSend.Count; i++)
            {
                handStr += CardListWaitToSend[i].cardId;
            }
            
            _pokerHand = _texasLogic.AnalyzeHandStr(handStr);
            _pokerHand.EvaluateHand();
            return true;
        }
        
        /// <summary>
        /// 将指定牌设置为手牌
        /// </summary>
        /// <param name="cardConfig"></param>
        public void SetCardToHand(NormalCard cardConfig)
        {
            CardListWaitToSend.Remove(cardConfig);
            if (CardListWaitToSend.Count == 0)
            {
                _pokerHand = null;
            }
        }

        public CaseEnum GetCurHandCardCase()
        {
            if (_pokerHand == null)
                return CaseEnum.None;
            return _pokerHand.HandCase;
        }

        public void SortUsingCards()
        {
            UsingCardList.Sort((card1, card2) =>
            {
                var card1Rank = TexasLogic.ConvertStrToRank(card1.cardId[0]);
                var card2Rank = TexasLogic.ConvertStrToRank(card2.cardId[0]);
                if (card1Rank > card2Rank)
                {
                    return -1;
                }
                return 0;
            });
        }

        public CardCase GetCardCaseConfigByCaseType(CaseEnum caseEnum)
        {
            for (int i = 0; i < _cardCaseConfig.CardCases.Count; i++)
            {
                var cardCase = _cardCaseConfig.CardCases[i];
                if (cardCase.caseEnum == caseEnum)
                {
                    return cardCase;
                }
            }

            return null;
        }

        public void UseCard(NormalCard normalCard)
        {
            UsedCardList.Add(normalCard);
            UsingCardList.Remove(normalCard);
        }
        
        public void ClearWaitToSendList()
        {
            CardListWaitToSend.Clear();
        }

        public int GetCurHandsDamage()
        {
            int baseDmg = 0;
            int magnification = 1;
            for (int i = 0; i < _cardCaseConfig.CardCases.Count; i++)
            {
                var item = _cardCaseConfig.CardCases[i];
                if (item.caseEnum == _pokerHand.HandCase)
                {
                    baseDmg = item.damageValue;
                    magnification = item.magnification;
                    break;
                }
            }

            for (int i = 0; i < _pokerHand.HandDetails.Count; i++)
            {
                if (_cacheCardDamage.TryGetValue(_pokerHand.HandDetails[i].CardId, out var dmg))
                {
                    baseDmg += dmg;
                }
            }

            return baseDmg * magnification;
        }
    }
}
