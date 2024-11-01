using Config;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyCardItem : UIBase
    {
        private EnemyConfig _enemyConfig;
        public int maxHp;
        public int curHp;

        private Text _hpText;
        private Text _nameText;
        private Image _icon;

        private void Awake()
        {
            _hpText = transform.Find("bg/hpText").GetComponent<Text>();
            _nameText = transform.Find("bg/nameText").GetComponent<Text>();
            _icon = transform.Find("bg/icon").GetComponent<Image>();
        }

        public void Init(EnemyConfig cardConfig)
        {
            _enemyConfig = cardConfig;
            curHp = maxHp = _enemyConfig.maxHp;
            _icon.sprite = Resources.Load<Sprite>(cardConfig.iconPath);
            _nameText.text = _enemyConfig.enemyName;
            _hpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
        }
        
        public override void OnShow()
        {
            base.OnShow();
            
        }
        public bool Hit(int getCurHandsDamage)
        {
            curHp -= getCurHandsDamage;

            if (curHp <= 0)
            {
                curHp = 0;
                _hpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
                return true;
            }
            
            _hpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isEnemyRound">在敌人回合开始时触发</param>
        public void DoAction(bool isEnemyRound)
        {
            var fightManager = GameManagerContainer.Instance.GetManager<FightManager>();
            var curRound = fightManager.GetCurRound();
            var fightUI = UIModule.Instance.GetUI<FightUI>("FightUI");
            fightUI.HideTip();

            for (int i = _enemyConfig.enemyActions.Count - 1; i >= 0 ; i--)
            {
                var ac = _enemyConfig.enemyActions[i];
                bool activeAc = false;
                if (ac.OneTimeAc && curRound == ac.round && ac.isEnableOnPlayerTurn != isEnemyRound)
                {
                    activeAc = true;
                }
                else if (!ac.OneTimeAc && curRound % ac.perRound == 0 && ac.isEnableOnPlayerTurn != isEnemyRound)
                {
                    activeAc = true;
                }

                if (activeAc)
                {

                    switch (ac.enemyActionType)
                    {
                        case EnemyActionType.Damage:
                            fightManager.HitPlayer(ac.param);
                            break;
                        case EnemyActionType.Tip:
                            fightUI.ShowTip(ac.paramStr);
                            break;
                        case EnemyActionType.Dialog:
                            fightUI.ShowDialog(ac.paramStr);
                            break;
                    }
                    return;
                }
            }
        }
        
        public void Recycle()
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }
}
