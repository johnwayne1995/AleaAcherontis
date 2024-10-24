using Managers;
using Modules;
namespace UI
{
    public class StartUI : UIBase
    {
        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("bgm1", true);
        }
    }
}
