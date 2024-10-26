using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    public class EnemyConfig: ScriptableObject
    {
        public int maxHp;
        public string enemyName;
        
        public List<EnemyAction> enemyActions = new List<EnemyAction>();
    }

    [Serializable]
    public class EnemyAction
    {
        [LabelText("一次性事件")]
        public bool OneTimeAc;
        
        /// <summary>
        /// 每几回合执行一次
        /// </summary>
        [ShowIf("@OneTimeAc == true")]
        public int round;
        
        /// <summary>
        /// 每几回合执行一次
        /// </summary>
        [ShowIf("@OneTimeAc == false")]
        public int perRound;
        public EnemyActionType enemyActionType = EnemyActionType.Damage;
        public int param;
    }

    public enum EnemyActionType
    {
        Damage,
        ForceAddCard,
    }
}
