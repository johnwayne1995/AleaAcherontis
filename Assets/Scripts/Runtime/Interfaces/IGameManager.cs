namespace Interfaces
{
    public interface IGameManager
    {
        /// <summary>
        /// 唤醒
        /// </summary>
        void Awake();

        /// <summary>
        /// 进入游戏
        /// </summary>
        void EnterGame();

        /// <summary>
        /// 进入游戏场景
        /// </summary>
        void EnterScene();

        /// <summary>
        /// 退出游戏场景
        /// </summary>
        void ExitScene();

        void Update();

        void LateUpdate();

        void FixedUpdate();
        
        /// <summary>
        /// 暂停游戏
        /// </summary>
        void PauseGame();

        /// <summary>
        /// 继续游戏
        /// </summary>
        void ContinueGame();
        
        /// <summary>
        /// 销毁
        /// </summary>
        void Destroy();
    }
}
