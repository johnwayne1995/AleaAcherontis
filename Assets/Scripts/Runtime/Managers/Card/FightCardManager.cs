using System.Collections.Generic;
using Config;
using Modules;
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
        public List<CardBase> CardList;
        /// <summary>
        /// 弃牌堆
        /// </summary>
        public List<CardBase> UsedCardList;
        /// <summary>
        /// 手牌
        /// </summary>
        public List<CardBase> UsingCardList;
        /// <summary>
        /// 待打出的牌
        /// </summary>
        public List<CardBase> CardListWaitToSend;

        private Dictionary<string, int> _cacheCardDamage = new Dictionary<string, int>();
        private CardCaseConfig _cardCaseConfig;
        
        private TexasLogic _texasLogic;
        private PokerHand _pokerHand;
        private int _baseMagnification = 1;

        protected override void OnAwake()
        {
            _texasLogic = new TexasLogic();
            
            CardList = new List<CardBase>();
            UsedCardList = new List<CardBase>();
            UsingCardList = new List<CardBase>();
            
            //定义临时集合
            
            _cardCaseConfig = Resources.Load<CardCaseConfig>("Configs/CardConfig/CardsCaseConfig");
        }

        protected override void OnEnterGame()
        {
            base.OnEnterGame();
            if (CardListWaitToSend != null)
                CardListWaitToSend.Clear();
            
            if (_cacheCardDamage != null)
                _cacheCardDamage.Clear();
            _pokerHand = null;
        }

        public void LoadCardGroup(PokerCardsConfig cardsConfig)
        {
            List<CardBase> tempList = new List<CardBase>();
            for (int i = 0; i < cardsConfig.normalCards.Count; i++)
            {
                var card = cardsConfig.normalCards[i];
                _cacheCardDamage.Add(card.id, card.basePoint);
            }
            
            tempList.AddRange(cardsConfig.normalCards);
            Shuffle(tempList);
            CardList.AddRange(tempList);
        }
        
        /// <summary>
        /// 洗牌
        /// </summary>
        private void Shuffle(List<CardBase> allCards)
        {
            DoShuffle(allCards, 52);
        }
        
        public void DoShuffle(List<CardBase> card, int n)
        {
            System.Random rand = new System.Random();
            for (int i = 0; i < n; i++)
            {
                int r = i + rand.Next(52 - i);
                (card[r], card[i]) = (card[i], card[r]);
            }
        }
        
        //是否有卡
        public bool HasCard(int needCount)
        {
            return CardList.Count >= needCount;
        }
        
        //抽卡
        public CardBase DrawCard()
        {
            var cardConfig = CardList[CardList.Count - 1];
            CardList.RemoveAt(CardList.Count - 1);
            return cardConfig;
        }
        
        public void DrawCards(int count)
        {
            if (!HasCard(count))
            {
                CardList.AddRange(UsedCardList);
                UsedCardList.Clear();
            }
            for (int i = 0; i < count; i++)
            {
                var card = DrawCard();
                UsingCardList.Add(card);
            }
            SortUsingCards(true);
        }
        
        /// <summary>
        /// 将指定牌设置为待打出
        /// </summary>
        /// <param name="cardConfig"></param>
        /// <returns></returns>
        public bool SetCardToWaitSend(CardBase cardConfig)
        {
            if (CardListWaitToSend == null)
                CardListWaitToSend = new List<CardBase>();

            if (CardListWaitToSend.Count == CMAX_SEND_CARD_COUNT)
            {
                //TODO 提示待打出牌已满
                return false;
            }
            
            CardListWaitToSend.Add(cardConfig);
            var handStr = string.Empty;
            for (int i = 0; i < CardListWaitToSend.Count; i++)
            {
                if (CardListWaitToSend[i] is PokerCard pokerCard)
                {
                    handStr += pokerCard.id;
                }
            }
            
            _pokerHand = _texasLogic.AnalyzeHandStr(handStr);
            _pokerHand.EvaluateHand();
            return true;
        }
        
        /// <summary>
        /// 将指定牌设置为手牌
        /// </summary>
        /// <param name="cardConfig"></param>
        public void SetCardToHand(CardBase cardConfig)
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

        public void SortUsingCards(bool sortByFace)
        {
            if (sortByFace)
            {
                UsingCardList.Sort((card1, card2) =>
                {
                    if (card1 is PokerCard p1 && card2 is PokerCard p2)
                    {
                        var card1Rank = TexasLogic.ConvertStrToRank(p1.id[0]);
                        var card2Rank = TexasLogic.ConvertStrToRank(p2.id[0]);
                        if (card1Rank > card2Rank)
                        {
                            return -1;
                        }
                    }
                
                    return 0;
                });
            }
            else
            {
                UsingCardList.Sort((card1, card2) =>
                {
                    if (card1 is PokerCard p1 && card2 is PokerCard p2)
                    {
                        var suit1 = TexasLogic.ConvertStrToSuit(p1.id[1]);
                        var suit2 = TexasLogic.ConvertStrToSuit(p2.id[1]);
                        if (suit1 > suit2)
                        {
                            return -1;
                        }
                    }
                
                    return 0;
                });
            }
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

        public void UseCard(CardBase normalCard)
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
            for (int i = 0; i < _cardCaseConfig.CardCases.Count; i++)
            {
                var item = _cardCaseConfig.CardCases[i];
                if (item.caseEnum == _pokerHand.HandCase)
                {
                    baseDmg = item.damageValue;
                    _baseMagnification = item.magnification;
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

            //todo计算 装备牌加的倍率
            var lastMag = _baseMagnification;
            var equipManager = GameManagerContainer.Instance.GetManager<EquipManager>();
            for (int i = 0; i < equipManager.CanEquipSlotCount; i++)
            {
                var equip = equipManager.GetEquipCardConfigByPos(i);
                if (equip == null)
                {
                    continue;
                }                
                switch (equip.devilCardInfluenceType)
                {
                    case DevilCardInfluenceType.Add:
                        lastMag += equip.paramValue;
                        break;
                    case DevilCardInfluenceType.Multiplication:
                        lastMag *= equip.paramValue;
                        break;
                }
            }

            return baseDmg * lastMag;
        }

        /// <summary>
        /// 弃牌
        /// </summary>
        /// <param name="getCardConfig"></param>
        public void FoldCard(PokerCard getCardConfig)
        {
            UsedCardList.Add(getCardConfig);
            UsingCardList.Remove(getCardConfig);
            CardListWaitToSend.Clear();
        }
    }
}
