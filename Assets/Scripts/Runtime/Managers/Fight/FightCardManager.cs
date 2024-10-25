using System.Collections.Generic;
using Config;
using UnityEngine;
namespace Managers
{
    public class FightCardManager : TGameManager<FightCardManager>
    {
        public const int CMAX_SEND_CARD_COUNT = 5;

        public List<NormalCard> cardList;//卡堆集合
        public List<NormalCard> usedCardList;//弃牌堆
        public List<NormalCard> usingCardList;
        
        private List<NormalCard> _cardListWaitToSend;
        
        private TexasLogic _texasLogic;
        private PokerHand _pokerHand;

        protected override void OnAwake()
        {
            _texasLogic = new TexasLogic();
            
            cardList = new List<NormalCard>();
            usedCardList = new List<NormalCard>();
            usingCardList = new List<NormalCard>();
            //定义临时集合
            List<NormalCard> tempList = new List<NormalCard>();
            var allNormalCard = Resources.Load<NormalCardsConfig>("Configs/CardConfig/NormalCardsConfig");
            tempList.AddRange(allNormalCard.NormalCards);
            Shuffle(tempList);
            cardList.AddRange(tempList);
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
            return cardList.Count > 0;
        }
        
        //抽卡
        public NormalCard DrawCard()
        {
            var cardConfig = cardList[cardList.Count - 1];
            cardList.RemoveAt(cardList.Count - 1);
            return cardConfig;
        }
        
        public void DrawCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var card = DrawCard();
                usingCardList.Add(card);
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
            if (_cardListWaitToSend == null)
                _cardListWaitToSend = new List<NormalCard>();

            if (_cardListWaitToSend.Count == CMAX_SEND_CARD_COUNT)
            {
                //TODO 提示待打出牌已满
                return false;
            }
            
            _cardListWaitToSend.Add(cardConfig);
            var handStr = string.Empty;
            for (int i = 0; i < _cardListWaitToSend.Count; i++)
            {
                handStr += _cardListWaitToSend[i].cardId;
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
            _cardListWaitToSend.Remove(cardConfig);
            if (_cardListWaitToSend.Count == 0)
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
            usingCardList.Sort((card1, card2) =>
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
    }
}
