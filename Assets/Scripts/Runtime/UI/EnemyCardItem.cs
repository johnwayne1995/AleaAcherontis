using DefaultNamespace;
using Fsm;
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
        
        private void Awake()
        {
            _hpText = transform.Find("bg/hpText").GetComponent<Text>();
        }

        public void Init(EnemyConfig cardConfig)
        {
            _enemyConfig = cardConfig;
            curHp = maxHp = _enemyConfig.maxHp;

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
                return true;
            }
            
            _hpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
            return false;
        }

        public void DoAction()
        {
            var fightManager = GameManagerContainer.Instance.GetManager<FightManager>();
            var curRound = fightManager.GetCurRound();
            for (int i = _enemyConfig.enemyActions.Count - 1; i >= 0 ; i--)
            {
                var ac = _enemyConfig.enemyActions[i];
                if (curRound % ac.perRound == 0)
                {
                    switch (ac.enemyActionType)
                    {
                        case EnemyActionType.Damage:
                            fightManager.HitPlayer(ac.param);
                            break;
                        case EnemyActionType.ForceAddCard:
                            
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
