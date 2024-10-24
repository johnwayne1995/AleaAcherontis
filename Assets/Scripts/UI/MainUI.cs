using Managers;
using Modules;
namespace UI
{
    public class MainUI : UIBase
    {
        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("battle", true);
        }
    }
}
