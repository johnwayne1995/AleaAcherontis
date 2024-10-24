using System.Collections.Generic;
using Config;
using UnityEngine;
namespace Managers
{
    public class FightCardManager : TGameManager<FightCardManager>
    {
        public List<NormalCard> cardList;//卡堆集合
        public List<NormalCard> usedCardList;//弃牌堆

        protected override void OnAwake()
        {
            cardList = new List<NormalCard>();
            usedCardList = new List<NormalCard>();
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
    }
}
