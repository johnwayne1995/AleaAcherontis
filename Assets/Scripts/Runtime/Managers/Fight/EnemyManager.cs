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

        public void LoadEnemy(EnemyConfig enemyConfig)
        {
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            _curEnemy = fightUi.CreateNewEnemy(enemyConfig);
        }
        
        /// <summary>
        /// 受击
        /// </summary>
        /// <param name="getCurHandsDamage"></param>
        public void BeHit(int getCurHandsDamage)
        {
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            var isDone = _curEnemy.Hit(getCurHandsDamage);
            if (isDone)
            {
                STimer.Wait(0.5f, () =>
                {
                    var fightManager = GameManagerContainer.Instance.GetManager<FightManager>();
                    fightManager.ChangeState(EFIGHT_STAGE.Win);
                });
            }
            else
            {
                STimer.Wait(0.5f, () =>
                {
                    fightUi.RemoveAllSendCard();
                });
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isEnemyRound">在敌人回合开始时触发</param>
        public void DoAction(bool isEnemyRound = true)
        {
            _curEnemy.DoAction(isEnemyRound);
        }
    }
}
