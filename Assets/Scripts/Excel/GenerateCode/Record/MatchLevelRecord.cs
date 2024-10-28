﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Excel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    
    // Level.xlsx
    [System.SerializableAttribute()]
    public class MatchLevelRecord
    {
        
        // 关卡ID
        [Excel.RecordKeyField()]
        [UnityEngine.SerializeField()]
        private int _RoomID;
        
        // 关卡层数
        [UnityEngine.SerializeField()]
        private int _RoomLayer;
        
        // 关卡类型
        //普通 = 0
        //NPC = 1
        //BOSS = 2
        [UnityEngine.SerializeField()]
        private int _EnemyType;
        
        // 能否跳过
        [UnityEngine.SerializeField()]
        private bool _CanSkip;
        
        // 是否肉鸽
        [UnityEngine.SerializeField()]
        private bool _IsRandom;
        
        // 非肉鸽的固定约束
        [UnityEngine.SerializeField()]
        private List<int> _Restrict;
        
        // 敌人id
        [UnityEngine.SerializeField()]
        private string _EnemyConfig;
        
        // 肉鸽房间的刷新权重
        //（格式：房间类型1,权重|房间类型2,权重|...）
        //0=许愿池，花费指定金币，三选一一个效果。花费的数额会越来越高
        //1=商人，会有三个物品刷新在商店，消耗金币可刷新一次
        //2=宝箱，白送一个物品，有X概率被宝箱怪咬一口，扣除X%百分比血量
        //3=祝福女神，三选一一个只能在本层使用的祝福
        // [UnityEngine.SerializeField()]
        // private List<ParamPair> _RoomWeight;
        
        /// <summary>
        /// 关卡ID
        /// </summary>
        public int RoomID
        {
            get
            {
                return this._RoomID;
            }
        }
        
        /// <summary>
        /// 关卡层数
        /// </summary>
        public int RoomLayer
        {
            get
            {
                return this._RoomLayer;
            }
        }
        
        /// <summary>
        /// 关卡类型
        ///普通 = 0
        ///NPC = 1
        ///BOSS = 2
        /// </summary>
        public int EnemyType
        {
            get
            {
                return this._EnemyType;
            }
        }
        
        /// <summary>
        /// 能否跳过
        /// </summary>
        public bool CanSkip
        {
            get
            {
                return this._CanSkip;
            }
        }
        
        /// <summary>
        /// 是否肉鸽
        /// </summary>
        public bool IsRandom
        {
            get
            {
                return this._IsRandom;
            }
        }
        
        /// <summary>
        /// 非肉鸽的固定约束
        /// </summary>
        public List<int> Restrict
        {
            get
            {
                return this._Restrict;
            }
        }
        
        /// <summary>
        /// 敌人id
        /// </summary>
        public string EnemyConfig
        {
            get
            {
                return this._EnemyConfig;
            }
        }
        
        /// <summary>
        /// 肉鸽房间的刷新权重
        ///（格式：房间类型1,权重|房间类型2,权重|...）
        ///0=许愿池，花费指定金币，三选一一个效果。花费的数额会越来越高
        ///1=商人，会有三个物品刷新在商店，消耗金币可刷新一次
        ///2=宝箱，白送一个物品，有X概率被宝箱怪咬一口，扣除X%百分比血量
        ///3=祝福女神，三选一一个只能在本层使用的祝福
        /// </summary>
        /// <remarks>
        /// Author: tianchanglin
        /// 决定了刷新不同类型房间的概率
        /// </remarks>
        // public List<ParamPair> RoomWeight
        // {
        //     get
        //     {
        //         return this._RoomWeight;
        //     }
        // }
    }
}