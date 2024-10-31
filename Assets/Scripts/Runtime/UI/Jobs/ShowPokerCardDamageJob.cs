// ************************************************************************** //
// File: ShowPokerCardPointJob.cs
// Author: longpeng.lin
// Data: 2024/10/31 16:59
// Description:
// 
// Notice:
// ************************************************************************** //

using Managers;
namespace UI.Jobs
{
    public class ShowPokerCardDamageJob : DependentJob
    {
        public PokerCardItem CardItem;

        public void InitParam(PokerCardItem item)
        {
            CardItem = item;
        }

        protected override void OnExecuteJob()
        {
            base.OnExecuteJob();
            CardItem.ShowDamage(OnShowDamageOver);
        }

        private void OnShowDamageOver()
        {
            MarkJobSuccess();
        }
    }
}
