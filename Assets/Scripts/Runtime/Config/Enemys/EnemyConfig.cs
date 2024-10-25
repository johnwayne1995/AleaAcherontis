using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
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
        /// <summary>
        /// 每几回合执行一次
        /// </summary>
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
