using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    public class EnemyConfig : ScriptableObject
    {
        public int maxHp;
        public string enemyName;
        public string iconPath;
        
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

        [LabelText("在玩家回合触发")]
        public bool isEnableOnPlayerTurn = false;
        
        /// <summary>
        /// 每几回合执行一次
        /// </summary>
        [ShowIf("@OneTimeAc == false")]
        public int perRound;
        public EnemyActionType enemyActionType = EnemyActionType.Damage;
        
        [ShowIf("@enemyActionType == EnemyActionType.Damage")]
        public int param;
        
        [ShowIf("@enemyActionType != EnemyActionType.Damage")]
        public string paramStr;
    }

    public enum EnemyActionType
    {
        Damage,
        //ForceAddCard,
        Tip,
        Dialog,
    }
}
