using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Modules;
using UnityEngine;

namespace UI
{
    public class FightUI : UIBase
    {
        private List<CardItem> cardItemList;
        
        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("battle", true);
            cardItemList = new List<CardItem>();
        }
        
        public void CreateCardItem(int count)
        {
            var fightCardMgr = GameManagerContainer.Instance.GetManager<FightCardManager>();

            if (count > fightCardMgr.cardList.Count)
            {
                count = fightCardMgr.cardList.Count;
            }

            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
                var item = obj.AddComponent<CardItem>();
                var cardConfig = fightCardMgr.DrawCard();
                item.Init(cardConfig);
                cardItemList.Add(item);
            }

            UpdateCardItemPos();
        }
        
        //更新卡牌位置
        public void UpdateCardItemPos()
        {
            float offset = 1000f / cardItemList.Count;
            Vector2 startPos = new Vector2(-cardItemList.Count / 2f * offset + offset * 0.5f, -500);
            for (int i = 0; i < cardItemList.Count; i++)
            {
                var card = cardItemList[i];
                card.DoInitMoveAni(startPos);
                startPos.x = startPos.x + offset;
            }
        }
    }
}
