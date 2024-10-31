using Managers;
namespace UI.Jobs
{
    public class ShowEquipCardEffectJob : DependentJob
    {
        public EquipCardItem CardItem;

        public void InitParam(EquipCardItem item)
        {
            CardItem = item;
        }

        protected override void OnExecuteJob()
        {
            base.OnExecuteJob();
            CardItem.ShowEffect(OnShowDamageOver);
        }

        private void OnShowDamageOver()
        {
            MarkJobSuccess();
        }
    }
}
