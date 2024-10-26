using Config;
using Fsm;
using Modules;
using UI;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : TGameManager<EnemyManager>
    {
        private EnemyCardItem _curEnemy;

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        protected override void OnEnterGame()
        {
            base.OnEnterGame();
            if (_curEnemy != null)
            {
                _curEnemy.Recycle();
            }
        }

        public void LoadEnemy()
        {
            var enemyConfig = Resources.Load<EnemyConfig>("Configs/EnemyConfigs/EnemyCardsConfig");
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            _curEnemy = fightUi.CreateNewEnemy(enemyConfig);
        }
        
        public void SelfHit(int getCurHandsDamage)
        {
            var isDone = _curEnemy.Hit(getCurHandsDamage);
            if (isDone)
            {
                var fightManager = GameManagerContainer.Instance.GetManager<FightManager>();
                fightManager.ChangeState(EFIGHT_STAGE.Win);
            }
            else
            {
                STimer.Wait(1.5f, () =>
                {
                    var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
                    fightUi.RemoveAllSendCard();
                });
            }
        }
        
        public void DoAction()
        {
            _curEnemy.DoAction();
        }
    }
}
