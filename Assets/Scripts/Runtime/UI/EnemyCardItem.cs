using DefaultNamespace;
using UnityEngine.UI;

namespace UI
{
    public class EnemyCardItem : UIBase
    {
        private EnemyConfig _enemyConfig;
        public int maxHp;
        public int curHp;

        private Text _hpText;
        
        private void Awake()
        {
            _hpText = transform.Find("bg/hpText").GetComponent<Text>();
        }

        public void Init(EnemyConfig cardConfig)
        {
            _enemyConfig = cardConfig;
            curHp = maxHp = _enemyConfig.maxHp;

            _hpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
        }
        
        public override void OnShow()
        {
            base.OnShow();
            
        }
        public void Hit(int getCurHandsDamage)
        {
            curHp -= getCurHandsDamage;
            _hpText.text = $"{curHp.ToString()}/{maxHp.ToString()}";
        }
    }
}
