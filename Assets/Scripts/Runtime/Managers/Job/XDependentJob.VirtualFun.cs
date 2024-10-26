namespace Managers
{
    public partial class DependentJob
    {
        /// <summary>
        /// 执行工作
        /// </summary>
        protected virtual void OnExecuteJob()
        {
		
        }
        
        /// <summary>
        /// 完成工作
        /// </summary>
        protected virtual void OnJobComplete()
        {
            
        }
        
        /// <summary>
        /// 打断工作
        /// </summary>
        protected virtual void OnBreakJob()
        {
            
        }

        /// <summary>
        /// 工作失败后的结果
        /// </summary>
        protected virtual void OnJobFailed()
        {
            
        }
        
        /// <summary>
        /// 清除工作
        /// </summary>
        protected virtual void OnCleanJob()
        {
            
        }

        /// <summary>
        /// 重置工作
        /// </summary>
        protected virtual void OnResetJob()
        {
            
        }
        
        /// <summary>
        /// 更新工作
        /// </summary>
        /// <param name="deltaTime"></param>
        protected virtual void OnUpdateJob(float deltaTime)
        {
            
        }
    }
}
