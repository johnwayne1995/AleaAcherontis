using Fsm;
using GameStages;
using UnityEngine;
namespace Modules
{
    public class GameStageModule : AbstractModule<GameStageModule>
    {
        private GameStageController _fsmController;
        public override void Start()
        {
            _fsmController = new GameStageController();
            _fsmController.InitState();
            _fsmController.AddState(EGAME_STAGE.Start, new GameStage_Start(EGAME_STAGE.Start, _fsmController));
            _fsmController.AddState(EGAME_STAGE.MatchRoom, new GameStage_MatchRoom(EGAME_STAGE.MatchRoom, _fsmController));
            _fsmController.AddState(EGAME_STAGE.GameMain, new GameStage_GameMain(EGAME_STAGE.GameMain, _fsmController));
            _fsmController.SetDefault(EGAME_STAGE.Start);
        }
        
        public override void Dispose()
        {
            if (_fsmController != null)
            {
                _fsmController.ClearAllStates();
                _fsmController = null;
            }
        }
        
        public void SwitchStage(EGAME_STAGE state , object e = null)
        {
            if(_fsmController == null)
                return;
            
            _fsmController.SwitchState(state,e);
        }
        
        public override void Update()
        {
            if(_fsmController == null)
                return;
            
            _fsmController.UpdateState(Time.deltaTime);
        }

        /// <summary>
        /// 获取游戏当前状态
        /// </summary>
        /// <returns></returns>
        public EGAME_STAGE GetCurStage()
        {
            if (_fsmController == null)
                return EGAME_STAGE.Unknown;
            
            return _fsmController.CurrentStateType;
        }
    }
}
