using DefaultNamespace;
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

        public void LoadEnemy()
        {
            var enemyConfig = Resources.Load<EnemyConfig>("Configs/EnemyConfigs/EnemyCardsConfig");
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            _curEnemy = fightUi.CreateNewEnemy(enemyConfig);
        }
        
        public void SelfHit(int getCurHandsDamage)
        {
            _curEnemy.Hit(getCurHandsDamage);
        }
        
        public void DoAction()
        {
            _curEnemy.DoAction();
        }
    }
}
