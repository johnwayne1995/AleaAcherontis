﻿using Config;
using FightStages;
using Fsm;
using Fsm.FightStages;
using Modules;
using UI;
using UnityEngine;

namespace Managers
{
    public class FightManager : TGameManager<FightManager>
    {
        private FightStageController _fsmController;
        public int MaxHp;//最大血量
        public int CurHp;//当前血量
        
        /// <summary>
        /// 回合数
        /// </summary>
        private int _maxRoundCount = 3;
        
        /// <summary>
        /// 回合数
        /// </summary>
        private int _roundCount = 0;
        
        public void StartFight()
        {
            _fsmController = new FightStageController();
            _fsmController.InitState();
            _fsmController.AddState(EFIGHT_STAGE.PlayerTurnSettlement, new FightStage_PlayerTurnSettlement(EFIGHT_STAGE.PlayerTurnSettlement, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.LoadCard, new FightStage_LoadCard(EFIGHT_STAGE.LoadCard, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Player, new FightStage_PlayerTurn(EFIGHT_STAGE.Player, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Enemy, new FightStage_EnemyTurn(EFIGHT_STAGE.Enemy, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.EnemyTurnSettlement, new FightStage_EnemyTurnSettlement(EFIGHT_STAGE.EnemyTurnSettlement, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Win, new FightStage_Win(EFIGHT_STAGE.Win, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Fail, new FightStage_Fail(EFIGHT_STAGE.Fail, _fsmController));
            _fsmController.SetDefault(EFIGHT_STAGE.Player);
            _roundCount = 0;
            CurHp = MaxHp = 1000;
            
        }

        /// <summary>
        /// 关卡接口-加载牌组
        /// </summary>
        /// <param name="cardCaseConfig">牌组参数</param>
        public void SetCardGroup(PokerCardsConfig cardCaseConfig)
        {
            cardCaseConfig = Resources.Load<PokerCardsConfig>("Configs/CardConfig/PokerCardsConfig");
            var fightCardMgr = GameManagerContainer.Instance.GetManager<FightCardManager>();
            fightCardMgr.LoadCardGroup(cardCaseConfig);

        }
        
        /// <summary>
        /// 关卡接口 创建敌人
        /// </summary>
        /// <param name="enemyConfig">敌人参数</param>
        public void SetEnemy(string path)
        {
            EnemyConfig enemyConfig;
            if(path == default)
                enemyConfig = Resources.Load<EnemyConfig>("Configs/EnemyConfigs/EnemyCardsConfig");
            else
                enemyConfig = Resources.Load<EnemyConfig>("Configs/EnemyConfigs/"+path);
            var enemyManager = GameManagerContainer.Instance.GetManager<EnemyManager>();
            enemyManager.LoadEnemy(enemyConfig);
        }
        
        //切换战斗类型
        public void ChangeState(EFIGHT_STAGE type)
        {
            if(_fsmController == null)
                return;
            
            _fsmController.SwitchState(type);
        }
        
        public EFIGHT_STAGE GetCurState()
        {
            if(_fsmController == null)
                return EFIGHT_STAGE.None;

            return _fsmController.CurrentStateType;
        }

        protected override void OnUpdate()
        {
            if(_fsmController == null)
                return;
            
            _fsmController.UpdateState(Time.deltaTime);
        }
        
        public void EnterNewRound()
        {
            _roundCount++;
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            fightUi.FlushRoundCount(_roundCount, _maxRoundCount);
        }

        public int GetCurRound()
        {
            return _roundCount;
        }
        
        public void HitPlayer(int damage)
        {
            CurHp -= damage;

            if (CurHp <= 0)
            {
                CurHp = 0;
            }
            
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            fightUi.FlushHp(CurHp, MaxHp);
        }
        
        /// <summary>
        /// 出牌数是否用完
        /// </summary>
        /// <returns></returns>
        public bool RoundOver()
        {
            if (_roundCount + 1 > _maxRoundCount)
            {
                return true;
            }

            return false;
        }
    }
}