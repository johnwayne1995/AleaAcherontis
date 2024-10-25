using System.Collections.Generic;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FightUI : UIBase
    {
        private List<CardItem> cardItemList;

        private FightCardManager _fightCardManager;
        private Button _sortBtn;
        private Button _sendBtn;
        private Text _caseText;

        private void Awake()
        {
            _fightCardManager = GameManagerContainer.Instance.GetManager<FightCardManager>();
            _caseText = transform.Find("leftMiddlePanel/caseTip/caseText").GetComponent<Text>();
            _sortBtn = transform.Find("sortBtn").GetComponent<Button>();
            _sendBtn = transform.Find("sendBtn").GetComponent<Button>();
            _sortBtn.onClick.AddListener(SortBtnClick);
            _sendBtn.onClick.AddListener(SendBtnClick);
            UpdateCardCaseTip();
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("battle", true);
            cardItemList = new List<CardItem>();
        }
        
        public void CreateCardItem(int count)
        {
            if (count > _fightCardManager.cardList.Count)
            {
                count = _fightCardManager.cardList.Count;
            }

            _fightCardManager.DrawCards(count);
            SortBtnClick();
        }

        private void ClearAllCardItem()
        {
            for (int i = 0; i < cardItemList.Count; i++)
            {
                cardItemList[i].OnRecycle();
            }
            
            cardItemList.Clear();
        }
        
        public void UpdateCardCaseTip()
        {
            var cardCase = _fightCardManager.GetCurHandCardCase();
            switch (cardCase)
            {
                case CaseEnum.StraightFlush:
                    _caseText.text = "同花顺";
                    break;
                case CaseEnum.FourOfAKind:
                    _caseText.text = "四条";
                    break;
                case CaseEnum.FullHouse:
                    _caseText.text = "葫芦";
                    break;
                case CaseEnum.Flush:
                    _caseText.text = "同花";
                    break;
                case CaseEnum.Straight:
                    _caseText.text = "顺子";
                    break;
                case CaseEnum.ThreeOfAKind:
                    _caseText.text = "三条";
                    break;
                case CaseEnum.TwoPair:
                    _caseText.text = "两对";
                    break;
                case CaseEnum.OnePair:
                    _caseText.text = "一对";
                    break;
                case CaseEnum.HighCard:
                    _caseText.text = "高牌";
                    break;
                default:
                    _caseText.text = "";
                    break;
            }
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

        private void SortBtnClick()
        {
            ClearAllCardItem();
            _fightCardManager.SortUsingCards();
            for (int i = 0; i < _fightCardManager.usingCardList.Count; i++)
            {
                GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
                var item = obj.AddComponent<CardItem>();
                item.Init(_fightCardManager.usingCardList[i], UpdateCardCaseTip);
                cardItemList.Add(item);
            }

            UpdateCardItemPos();
        }
        
        private void SendBtnClick()
        {
            
        }
    }
}
