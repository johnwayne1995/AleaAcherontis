namespace Managers
{
    public class EquipManager : TGameManager<EquipManager>
    {
        public int MaxEquipSlotCount
        {
            get;
            private set;
        }
        
        public int CanEquipSlotCount
        {
            get;
            private set;
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            MaxEquipSlotCount = 2;
            CanEquipSlotCount = 1;
        }

        protected override void OnEnterGame()
        {
            base.OnEnterGame();
        }
    }
}
