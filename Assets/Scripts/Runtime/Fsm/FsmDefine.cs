namespace Fsm
{
    public enum EGAME_STAGE
    {
        Unknown = -1, 
        Start = 0,   
        MatchRoom,
        GameMain,       
    }
    
    public enum EFIGHT_STAGE
    {
        None,
        LoadCard,
        Player,//玩家回合
        PlayerTurnSettlement,
        Enemy,//敌人回合
        EnemyTurnSettlement,
        Win,
        Fail 
    }
}
