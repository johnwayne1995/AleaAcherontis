using System.Collections.Generic;

namespace Excel
{
    public enum PropertyType
    {
        
    }
    
    public enum Rarity
    {
        Normal = 1,              //N
        Rare,                    //R
        SuperRare,               //SR
        SpeciallySuperRare,      //SSR
        Max,
    }
    
    public enum Element
    {
        Fire = 1,    //火
        Wind,        //木
        Water,       //水
        Dark,        //暗
        Light        //光
    }

    public enum MonsterType
    {
        LittleMonster = 1,
        NormalMonster = 2,
        HardMonster = 3,
        NormalBoss = 4,
        HardBoss = 5,
        littleWeakBoss = 6,
        WeakBoss = 7,
        Elite = 8,
    }
    
    public enum MonsterClass
    {
        Monster = 1,
        Elite = 2,
        Boss = 3,
    }
    
    public enum Job
    {
        FIGHTER = 1,    //战士
        BARBARIAN = 2,  //野蛮人
        MARKSMAN = 3,   //神射手
        ROGUE = 4,      //盗贼
        WARPRIEST = 5,  //战斗祭祀
        WIZARD = 6      //法师
    }
    
    public enum MessageType
    {
        ErrorTips = 1,
        FeedbackTips = 2,
        GetItem = 3,
        SystemUnlock = 4,
        UpdateSkill = 5,
    }
    
    public enum UpgradeStarCostType
    {
        SameStarAndSpecifyID = 1,        //消耗 星级相同且特定ID的角色
        SameStarAndSpecifyElement = 2,   //消耗 星级相同且特定Element的角色
        SameStar = 3                     //消耗 星级相同的角色
    }
    
    public enum ItemType
    {
        None = 0,       //空
        Character,      //角色
        Suit,           //服装
        LimitedCard,    //限制卡
        Equipment,      //装备
        Bundle,         //宝箱
        MiscItems,      //其他道具
        DateCard,       //约会卡牌
        HeadPic,        //头像
        HeadFrame,      //头像框
        Crystal,        //角色信物
        CulCard = 14,        //艺能养成协力卡
        BGMPiece = 15,  //战歌碎片
        Omamori = 17,   //御守
        SelectBundle = 18,
        Gift = 19,      //礼物
    }
    
    public class ItemTypeEqualityComparer : IEqualityComparer<ItemType>
    {
        public bool Equals(ItemType x, ItemType y)
        {
            return x == y;
        }

        public int GetHashCode(ItemType obj)
        {
            return (int) obj;
        }
    }
    
    public enum ItemSubType
    {
        None = 0,       //空
        RenameCard,     //改名卡
        EnergyTablet,   //体力药
        Materials,       //养成材料
        ElementCore = 5,     //御守核心
        Omamori, //御守
        StarUpMaterials,
    }
    
    public class ItemSubTypeEqualityComparer : IEqualityComparer<ItemSubType>
    {
        public bool Equals(ItemSubType x, ItemSubType y)
        {
            return x == y;
        }

        public int GetHashCode(ItemSubType obj)
        {
            return (int) obj;
        }
    }
    
    public enum CumulativeType
    {
        Cumu = 0,       //可叠加
        NotCumu     //不可叠加
    }
    
    public enum Quality
    {
        None = 0,   //空
        White,      //白色
        Green,      //绿色
        Blue,       //蓝色
        Purple,     //紫色
        Orange,     //橙色
        Colorful,   //彩色
    }
    
    public enum ERewardType
    {
        None = 0,
        Item,
        Coin,
        Gem,
    }
    
    public enum BagType
    {
        Consume = 1,
        Materials,
        Prop,
    }

    public enum BagFilterType
    {
        BagMain,
        Compose,
        Decompose
    }
    
    public enum InventoryTabType
    {
        All = 0,
        Consume,
        Materials,
        Prop,
        CoreEquip,
        Fragment,
        SubEquip,
        Gift,
        None,
    }

    #region lovecard
    
    public enum InteractType
    {
        Topic = 1,          //话题卡
        Action,         //行为卡
        Vison,          //视觉卡
        Mechanism,      //机制卡
    }
    
    public enum LikeAttr
    {
        Passion = 1,    //激情
        Duty,           //责任
        Intimate        //亲密
    }

    #endregion

    #region love system
    
    public enum LoveCategory
    {
        PlayerAndCharacter = 1, //玩家与角色
        CharacterAndCharacter = 2 //角色与角色
    }
    
    public enum InformationType
    {
        HeightAndWeight = 1, //身高体重
        Interest = 2, //兴趣
        Like = 3, //爱好
        Hate = 4, //讨厌
        NickName = 5, //昵称
    }
    
    public enum EUnlockType
    {
        None = 0,
        Information, //角色资料
        Biography,   //角色个人传记
        Voice,       //角色CV语音
        Stroy,       //角色之间剧情
        Attr,        //基础属性
        Skill,       //战斗技能
        Other,       //其他功能技能

        Max
    }
    #endregion

    #region TourPerform
    public enum ERequiryInfoType
    {
        None = 0,
        Level,
        Quality,
        Sex,
        Attribute,

        Max
    }
    #endregion

    #region Dungeon
    
    public enum DungeonTypeEnum
    {
        Materials = 1,
        Story,
        Challenge,
        Activity,
        OfflineArena,
        TeachingDungeon
    }
    
    public enum DungeonSubTypeEnum
    {
        Exp = 1,
        Skill = 2,
        Equip = 3,
        Gold = 4,
        Adventure = 5,
        MainStory = 10,
        SubStory = 11,
        HideStory = 12,
        Challenge = 20,
        RoguelikeChallenge = 21,
        TowerDungeon = 22,
        FragmentBattle = 23,
        ActivityNormal = 30,
        ActivityElite = 31,
        ActivityBoss = 32,
        OfflineArena = 40,
        TeachingJunior = 61,
        TeachingSenior = 62,
    }

    #endregion
}