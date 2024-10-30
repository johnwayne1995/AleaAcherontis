using System;
using System.Collections.Generic;
using Config;
using DG.Tweening;
using Fsm;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FightUI : UIBase
    {
        /// <summary>
        /// 所有手牌
        /// </summary>
        private List<PokerCardItem> _cardItemList;

        /// <summary>
        /// 等待打出的牌
        /// </summary>
        private List<PokerCardItem> _sendCardList;

        /// <summary>  
        /// 卡牌起始位置  
        /// </summary>  
        public Vector3 rootPos = new Vector3(0, -2400, 0);
        /// <summary>  
        /// 扇形半径  
        /// </summary>  
        public float size = 2500f;
        /// <summary>  
        /// 卡牌出现最大位置  
        /// </summary>  
        private float minPos = 1.415f;
        /// <summary>  
        /// 卡牌出现最小位置  
        /// </summary>  
        private float maxPos = 1.73f;
        /// <summary>  
        /// 手牌位置  
        /// </summary>  
        private List<float> _rotPos;

        private PokerCardPool _pokerCardPool;
        private FightCardManager _fightCardManager;
        private FightManager _fightManager;
        private EquipManager _equipManager;

        private Transform _equipParent;
        private Transform _tipParent;
        private Transform _enemyParent;
        private CanvasGroup _dialogCanvas;

        private Button _faceSortBtn;
        private Button _suitSortBtn;

        private Button _foldBtn;
        private Button _sendBtn;
        private Text _caseText;
        private Text _caseDamageText;
        private Text _magnificationText;
        private Text _playerHpText;
        private Text _tipText;
        private Text _dialogText;

        private Text _sendLastCountText;
        private Text _foldLastCountText;
        
        private Transform _cardParent;

        private void Awake()
        {
            _fightCardManager = GameManagerContainer.Instance.GetManager<FightCardManager>();
            _fightManager = GameManagerContainer.Instance.GetManager<FightManager>();
            _equipManager = GameManagerContainer.Instance.GetManager<EquipManager>();

            rootPos.x = (float)Screen.width / 2;
            
            _tipParent = transform.Find("tipPanel/tip");
            _dialogCanvas = transform.Find("tipPanel/dialog").GetComponent<CanvasGroup>();

            _enemyParent = transform.Find("enemyParent");
            _equipParent = transform.Find("rightMiddlePanel/cardGrid");
            _caseText = transform.Find("leftMiddlePanel/caseTip/caseText").GetComponent<Text>();
            _caseDamageText = transform.Find("leftMiddlePanel/damagePanel/caseDamageText").GetComponent<Text>();
            _magnificationText = transform.Find("leftMiddlePanel/damagePanel/magnificationText").GetComponent<Text>();
            _playerHpText = transform.Find("playerHp").GetComponent<Text>();
            _tipText = transform.Find("tipPanel/tip/tipText").GetComponent<Text>();
            _dialogText = transform.Find("tipPanel/dialog/dialogText").GetComponent<Text>();

            _faceSortBtn = transform.Find("faceSortBtn").GetComponent<Button>();
            _suitSortBtn = transform.Find("suitSortBtn").GetComponent<Button>();

            _foldBtn = transform.Find("foldBtn").GetComponent<Button>();
            _sendBtn = transform.Find("sendBtn").GetComponent<Button>();
            
            _sendLastCountText = transform.Find("sendBtn/lastCount").GetComponent<Text>();
            _foldLastCountText = transform.Find("foldBtn/lastCount").GetComponent<Text>();

            _faceSortBtn.onClick.AddListener(FaceSortBtnClick);
            _suitSortBtn.onClick.AddListener(SuitSortBtnClick);

            _sendBtn.onClick.AddListener(SendBtnClick);
            _foldBtn.onClick.AddListener(FoldBtnClick);
            OnWaitSendListChanged();

            if (_cardParent == null)
            {
                _cardParent = new GameObject("Pokers").transform;
                _cardParent.SetParent(transform);
                _cardParent.transform.localPosition = Vector3.zero;
            }
            _tipParent.gameObject.SetActive(false);
            _dialogCanvas.alpha = 0;
            
            _pokerCardPool = new PokerCardPool(_cardParent);
            _cardItemList = new List<PokerCardItem>();
            _sendCardList = new List<PokerCardItem>();
            _rotPos = InitRotPos(FightCardManager.CMAX_SAVE_CARD_COUNT);
        }

        private void Update()
        {
            RefreshCardPos();
        }

        /// <summary>  
        /// 手牌状态刷新  
        /// </summary>  
        private void RefreshCardPos()
        {
            if (_cardItemList == null)
            {
                return;
            }

            for (int i = 0; i < _cardItemList.Count; i++)
            {
                var card = _cardItemList[i];
                if (card != null)
                {
                    card.RefreshData(rootPos, _rotPos[i], size);
                }
            }
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("battle", true);
            InitEquipInfo();
        }

        public override void OnHide()
        {
            base.OnHide();
            DestroyImmediate(_cardParent);
            _pokerCardPool.ReleaseAll();
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
            FaceSortBtnClick();
        }

        private void ClearAllCardItem()
        {
            for (int i = 0; i < _cardItemList.Count; i++)
            {
                _pokerCardPool.Free(_cardItemList[i]);
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

        private void FaceSortBtnClick()
        {
            SortCardItem(true);
        }
        
        private void SuitSortBtnClick()
        {
            SortCardItem(false);
        }

        public void SortCardItem(bool sortByFace)
        {
            _fightCardManager.SortUsingCards(sortByFace);
            ClearAllCardItem();
            for (int i = 0; i < _fightCardManager.UsingCardList.Count; i++)
            {
                var cardConfig = _fightCardManager.UsingCardList[i];
                if (cardConfig is PokerCard pokerCard)
                {
                    var item = _pokerCardPool.Alloc();
                    item.transform.SetAsLastSibling();
                    item.InitCardItem(pokerCard, OnWaitSendListChanged);
                    item.RefreshData(rootPos, 0, 0);
                    _cardItemList.Add(item);
                }

            }
        }

        /// <summary>  
        /// 初始化位置  
        /// </summary>  
        /// <param name="count"></param>    
        /// <returns></returns>    
        private List<float> InitRotPos(int count)
        {
            List<float> rotPosList = new List<float>();
            float interval = (maxPos - minPos) / count;
            for (int i = 0; i < count; i++)
            {
                float nowPos = maxPos - interval * i;
                rotPosList.Add(nowPos);
            }
            return rotPosList;
        }

        private void FoldBtnClick()
        {
            if (_fightCardManager.CardListWaitToSend.Count == 0 || !_fightManager.CanFold())
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

            for (int i = 0; i < _sendCardList.Count; i++)
            {
                var card = _sendCardList[i];
                var tweener = card.DoInitMoveAni(new Vector2(1000, -700), 0.25f);
                card.DoScaleAni(0, 0.25f);
                tweener.onComplete = () =>
                {
                    _pokerCardPool.Free(card);
                };
                _fightCardManager.FoldCard(card.GetCardConfig());
            }

            _fightCardManager.ClearWaitToSendList();
            _fightManager.UseFold();
            _sendCardList.Clear();
            CreateCardItem();
            FlushFoldCount();
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
                var tweener = item.DoInitMoveAni(new Vector2(1000, -700), 0.25f);
                item.DoScaleAni(0, 0.25f);
                tweener.onComplete = () =>
                {
                    _pokerCardPool.Free(item);
                };
            }

            _fightCardManager.ClearWaitToSendList();
            _sendCardList.Clear();
            if (_fightManager.GetCurState() == EFIGHT_STAGE.PlayerTurnSettlement)
                _fightManager.ChangeState(EFIGHT_STAGE.Enemy);
        }

        public EnemyCardItem CreateNewEnemy(EnemyConfig enemyConfig)
        {
            GameObject obj = GameObject.Instantiate(Resources.Load("UI/EnemyCardItem"), _enemyParent) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
            var item = obj.AddComponent<EnemyCardItem>();
            obj.transform.SetAsFirstSibling();
            item.Init(enemyConfig);
            
            FlushFoldCount();
            FlushRoundCount();
            return item;
        }

        public void FlushHp(int curHp, int maxHp)
        {
            _playerHpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
        }

        public void FlushRoundCount()
        {
            _sendLastCountText.text = $"剩余{(_fightManager.GetMaxRound() - _fightManager.GetCurRound()).ToString()}次";
        }
        
        public void FlushFoldCount()
        {
            _foldLastCountText.text = $"剩余{(_fightManager.GetMaxFoldCount() - _fightManager.GetCurFoldCount()).ToString()}次";
        }

        public void InitEquipInfo()
        {
            for (int i = 0; i < _equipManager.MaxEquipSlotCount; i++)
            {
                bool isLock = i >= _equipManager.CanEquipSlotCount;
                var cardConfig = _equipManager.GetEquipCardConfigByPos(i);
                GameObject obj = GameObject.Instantiate(Resources.Load("UI/EquipCardItem"), _equipParent) as GameObject;
                obj.transform.SetAsFirstSibling();
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                var item = obj.AddComponent<EquipCardItem>();
                item.Init(isLock, cardConfig);
            }
        }

        public void ShowTip(string tipStr)
        {
            _tipParent.gameObject.SetActive(true);
            _tipText.text = tipStr;
        }
        
        public void ShowDialog(string tipStr)
        {
            _dialogText.text = tipStr;
            _dialogCanvas.DOFade(1, 0.5f);
            var tweenerHide = _dialogCanvas.DOFade(0, 0.5f);
            tweenerHide.SetDelay(1.5f);
        }
        
        public void HideTip()
        {
            _tipParent.gameObject.SetActive(false);
        }
    }
}
