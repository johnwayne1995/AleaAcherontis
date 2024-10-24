﻿namespace Fsm
{
    public enum EGAME_STAGE
    {
        Unknown = -1, 
        Start = 0,               
        GameMain,       
    }
    
    public enum EFIGHT_STAGE
    {
        None,
        LoadCard,
        Player,//玩家回合
        Enemy,//敌人回合
        Win,
        Fail 
    }
}
