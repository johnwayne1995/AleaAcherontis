using System;
using System.Collections.Generic;
using Config;
using Fsm;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FightUI : UIBase
    {
        private List<PokerCardItem> _cardItemList;
        private List<PokerCardItem> _sendCardList;

        private FightCardManager _fightCardManager;
        private FightManager _fightManager;

        private Button _sortBtn;
        private Button _sendBtn;
        private Text _caseText;
        private Text _caseDamageText;
        private Text _magnificationText;
        private Text _playerHpText;
        private Text _roundCountText;

        private void Awake()
        {
            _fightCardManager = GameManagerContainer.Instance.GetManager<FightCardManager>();
            _fightManager = GameManagerContainer.Instance.GetManager<FightManager>();

            _caseText = transform.Find("leftMiddlePanel/caseTip/caseText").GetComponent<Text>();
            _caseDamageText = transform.Find("leftMiddlePanel/damagePanel/caseDamageText").GetComponent<Text>();
            _magnificationText = transform.Find("leftMiddlePanel/damagePanel/magnificationText").GetComponent<Text>();
            _playerHpText = transform.Find("playerHp").GetComponent<Text>();
            _roundCountText = transform.Find("roundCount").GetComponent<Text>();

            _sortBtn = transform.Find("sortBtn").GetComponent<Button>();
            _sendBtn = transform.Find("sendBtn").GetComponent<Button>();
            _sortBtn.onClick.AddListener(SortBtnClick);
            _sendBtn.onClick.AddListener(SendBtnClick);
            OnWaitSendListChanged();

            _cardItemList = new List<PokerCardItem>();
            _sendCardList = new List<PokerCardItem>();
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("battle", true);
        }

        public void CreateCardItem()
        {
            var curHandCardCount = _fightCardManager.UsingCardList.Count;
            if (curHandCardCount > FightCardManager.CMAX_SAVE_CARD_COUNT)
            {
                //手牌超出，需弃牌
                
                return;
            }

            int needDrawCount = 0;
            if (curHandCardCount == 0)
            {
                needDrawCount = FightCardManager.CMAX_SAVE_CARD_COUNT;
            }
            else
            {
                needDrawCount = FightCardManager.CMAX_SAVE_CARD_COUNT - curHandCardCount;
            }
            
            if (needDrawCount > _fightCardManager.CardList.Count)
            {
                needDrawCount = _fightCardManager.CardList.Count;
            }

            _fightCardManager.DrawCards(needDrawCount);
            SortBtnClick();
        }

        private void ClearAllCardItem()
        {
            for (int i = 0; i < _cardItemList.Count; i++)
            {
                _cardItemList[i].OnRecycle();
            }

            _cardItemList.Clear();
        }

        public void OnWaitSendListChanged()
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
                    _caseText.text = String.Empty;
                    _caseDamageText.text = String.Empty;
                    _magnificationText.text = String.Empty;
                    return;
            }
            var cardCaseConfig = _fightCardManager.GetCardCaseConfigByCaseType(cardCase);
            _caseDamageText.text = cardCaseConfig.damageValue.ToString();
            _magnificationText.text = cardCaseConfig.magnification.ToString();
        }

        //更新卡牌位置
        public void UpdateCardItemPos()
        {
            float offset = 1000f / _cardItemList.Count;
            Vector2 startPos = new Vector2(-_cardItemList.Count / 2f * offset + offset * 0.5f, -500);
            for (int i = 0; i < _cardItemList.Count; i++)
            {
                var card = _cardItemList[i];
                card.DoInitMoveAni(startPos);
                startPos.x = startPos.x + offset;
            }
        }

        private void SortBtnClick()
        {
            ClearAllCardItem();
            _fightCardManager.SortUsingCards();
            for (int i = 0; i < _fightCardManager.UsingCardList.Count; i++)
            {
                var cardConfig = _fightCardManager.UsingCardList[i];
                if (cardConfig is PokerCard pokerCard)
                {
                    GameObject obj = Instantiate(Resources.Load("UI/PokerCardItem"), transform) as GameObject;
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
                    var item = obj.AddComponent<PokerCardItem>();
                    item.InitCardItem(pokerCard, OnWaitSendListChanged);
                    _cardItemList.Add(item);
                }
                
            }

            UpdateCardItemPos();
        }

        private void SendBtnClick()
        {
            if (_fightCardManager.CardListWaitToSend.Count == 0)
            {
                return;
            }

            _sendCardList.Clear();
            for (int i = 0; i < _cardItemList.Count; i++)
            {
                var card = _cardItemList[i];
                if (card.isSelected)
                {
                    _sendCardList.Add(card);
                }
            }

            for (int i = _sendCardList.Count - 1; i >= 0; i--)
            {
                _cardItemList.Remove(_sendCardList[i]);
            }
            UpdateCardItemPos();

            float offset = 600f / _sendCardList.Count;
            Vector2 enPos = new Vector2(-_sendCardList.Count / 2f * offset + offset * 0.5f, 0);
            for (int i = 0; i < _sendCardList.Count; i++)
            {
                var card = _sendCardList[i];
                card.DoInitMoveAni(enPos);
                enPos.x = enPos.x + offset;
            }
            
            _fightManager.ChangeState(EFIGHT_STAGE.PlayerTurnSettlement);
        }
        
        public void RemoveAllSendCard()
        {
            for (int i = 0; i < _sendCardList.Count; i++)
            {
                var item = _sendCardList[i];
                _fightCardManager.UseCard(item.GetCardConfig());
                item.DoInitMoveAni(new Vector2(1000, -700), 0.25f);
                item.DoScaleAni(0, 0.25f);
                Destroy(item.gameObject, 1);
            }

            _fightCardManager.ClearWaitToSendList();
            _sendCardList.Clear();
            if(_fightManager.GetCurState() == EFIGHT_STAGE.PlayerTurnSettlement)
                _fightManager.ChangeState(EFIGHT_STAGE.Enemy);
        }

        public EnemyCardItem CreateNewEnemy(EnemyConfig enemyConfig)
        {
            GameObject obj = GameObject.Instantiate(Resources.Load("UI/EnemyCardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
            var item = obj.AddComponent<EnemyCardItem>();
            item.Init(enemyConfig);
            return item;
        }
        
        public void FlushHp(int curHp, int maxHp)
        {
            _playerHpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
        }
        
        public void FlushRoundCount(int curRound, int maxRound)
        {
            _roundCountText.text = $"{curRound.ToString()}/{maxRound.ToString()}";
        }

        public void InitEquipInfo()
        {
            
        }
    }
}
