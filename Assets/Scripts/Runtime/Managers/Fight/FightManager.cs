using FightStages;
using Fsm;
using Fsm.FightStages;
using UnityEngine;

namespace Managers
{
    public class FightManager : TGameManager<FightManager>
    {
        private FightStageController _fsmController;
        public int MaxHp;//最大血量
        public int CurHp;//当前血量
        
        public void StartFight()
        {
            _fsmController = new FightStageController();
            _fsmController.InitState();
            _fsmController.AddState(EFIGHT_STAGE.LoadCard, new FightStage_LoadCard(EFIGHT_STAGE.LoadCard, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Player, new FightStage_PlayerTurn(EFIGHT_STAGE.Player, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Enemy, new FightStage_EnemyTurn(EFIGHT_STAGE.Enemy, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Win, new FightStage_Win(EFIGHT_STAGE.Win, _fsmController));
            _fsmController.AddState(EFIGHT_STAGE.Fail, new FightStage_Fail(EFIGHT_STAGE.Fail, _fsmController));
            _fsmController.SetDefault(EFIGHT_STAGE.LoadCard);
        }
        
        //切换战斗类型
        public void ChangeType(EFIGHT_STAGE type)
        {
            if(_fsmController == null)
                return;
            
            _fsmController.SwitchState(type);
        }

        protected override void OnUpdate()
        {
            if(_fsmController == null)
                return;
            
            _fsmController.UpdateState(Time.deltaTime);
        }
    }
}
