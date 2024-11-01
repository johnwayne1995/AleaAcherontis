using Config;
using DG.Tweening;
using Fsm;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinUI : UIBase
    {
        private Image _cardIcon;
        private RectTransform _cardPanel;
        private Transform _dialogPanel;
        private RectTransform _winPanel;
        private Text _dialogText;
        private Text _nameText;
        private Text _winText;

        private Button _restartBtn;
        private Vector3 _originPos = new Vector3();
        
        private void Awake()
        {
            _cardIcon = transform.Find("EquipCardItem/bg").GetComponent<Image>();
            _cardPanel = transform.Find("EquipCardItem").GetComponent<RectTransform>();
            _dialogPanel = transform.Find("EquipCardItem/dialogPanel");
            _dialogText = transform.Find("EquipCardItem/dialogPanel/dialogText").GetComponent<Text>();
            _nameText = transform.Find("EquipCardItem/nameText").GetComponent<Text>();
            _winText = transform.Find("winText/text").GetComponent<Text>();
            _winPanel = transform.Find("winText").GetComponent<RectTransform>();
            _restartBtn = transform.Find("restartBtn").GetComponent<Button>();
            _restartBtn.onClick.AddListener(OnStartGameBtnClick);
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("bgm1", true);
            
            var matchLevelManager = GameManagerContainer.Instance.GetManager<MatchLevelManager>();
            var table = matchLevelManager.GetMatchLevelTable();
            var curLevConfig = table[matchLevelManager.curRoom];
            var containReward = curLevConfig.IfGetReward;
            _winText.text = containReward ? "恭喜你击败boss\n获得手牌" : "恭喜你击败boss";
            _winPanel.anchoredPosition = containReward ? new Vector2(0, 309) : new Vector2(0, 0);
            if (containReward)
            {
                var equipManager = GameManagerContainer.Instance.GetManager<EquipManager>();
                var reward = Resources.Load<EquipCardConfig>("Configs/CardConfig/" + curLevConfig.RewardItem);
                equipManager.AddEquipReward(reward);
                _cardIcon.sprite = Resources.Load<Sprite>(reward.cardIconPath);
                _dialogText.text = reward.dialog;
                _nameText.text = reward.name;

                _restartBtn.gameObject.SetActive(false);
                _dialogPanel.gameObject.SetActive(false);
                
                var tweener = _cardPanel.DOAnchorPos(Vector2.zero, 0.8f);
                tweener.SetDelay(2f);
                tweener.onComplete += () =>
                {
                    _restartBtn.gameObject.SetActive(true);
                    _dialogPanel.gameObject.SetActive(true);
                };
            }
        }

        private void OnStartGameBtnClick()
        {
            //关闭login界面
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.MatchRoom);
            GameManagerContainer.Instance.GetManager<MatchLevelManager>().SkipCurRoom();
        }
    }
}
