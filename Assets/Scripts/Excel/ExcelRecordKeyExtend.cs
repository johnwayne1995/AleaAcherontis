using UnityEngine;
using System.Collections;

namespace Excel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    // public partial class AIBotNameKey : ExcelRecordCompositeKey
    // {
    //  public override int GetHashCode()
    //  {
    //      if (0 == _hashCode)
    //      {
    //          _hashCode = this.Language.GetHashCode() + this.id;
    //      }
    //
    //      return _hashCode;
    //  }
    // }
    //
    // public partial class BlockHeightKey : ExcelRecordCompositeKey
    // {
    //  public override int GetHashCode()
    //  {
    //      if (0 == _hashCode)
    //      {
    //          _hashCode = this.MapID * 100 + this.GridIndexX * 10 + this.GridIndexY;
    //      }
    //
    //      return _hashCode;
    //  }
    // }
    //
    // public partial class MerchantTrustLevelModeKey : ExcelRecordCompositeKey
    // {
    //     public override int GetHashCode()
    //     {
    //         if (0 == _hashCode)
    //         {
    //             _hashCode = this.TrustLevelMode * 100 + this.TrustLevel;
    //         }
    //
    //         return _hashCode;
    //     }
    // }
    //
    // public partial class ConditionLocalizationKey : ExcelRecordCompositeKey
    // {
    //     public override int GetHashCode()
    //     {
    //         if (0 == _hashCode)
    //         {
    //             _hashCode = this.Language.GetHashCode() + this.ConditionID;
    //         }
    //
    //         return _hashCode;
    //     }
    // }
    //
    // public partial class TaskLocalizationKey : ExcelRecordCompositeKey
    // {
    //     public override int GetHashCode()
    //     {
    //         if (0 == _hashCode)
    //         {
    //             _hashCode = this.Language.GetHashCode() + this.TaskID;
    //         }
    //
    //         return _hashCode;
    //     }
    // }
    //
    // public partial class IconInMapConfigKey : ExcelRecordCompositeKey
    // {
    //     public override int GetHashCode()
    //     {
    //         if (0 == _hashCode)
    //         {
    //             _hashCode = this.ConfigId.GetHashCode() * 10000 + this.ModeID * 1000 + this.MapID;
    //         }
    //
    //         return _hashCode;
    //     }
    // }
    //
    // public partial class IconDisplayKey : ExcelRecordCompositeKey
    // {
    //     public override int GetHashCode()
    //     {
    //         if (0 == _hashCode)
    //         {
    //             _hashCode = this.TemplateType * 1000 + this.InteractType * 100 + this.TemplateSubType;
    //         }
    //
    //         return _hashCode;
    //     }
    // }
    //
    // public partial class TypeEffectKey : ExcelRecordCompositeKey
    // {
    //     public override int GetHashCode()
    //     {
    //         if (0 == _hashCode)
    //         {
    //             _hashCode = this.TargetType * 1000 + this.Quality;
    //         }
    //
    //         return _hashCode;
    //     }
    // }

    //     public partial class GuideStep2Key : ExcelRecordCompositeKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 //_hashCode = this.group * 1000 + this.step;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class GuideStepKey : ExcelRecordCompositeKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 //_hashCode = this.group * 1000 + this.step;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     // Character.xlsx
    //     public partial class CharacterStarAttributeKey : ExcelRecordCompositeKey,
    //         System.IEquatable<CharacterStarAttributeKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = CharacterID * 1000 + Star;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     // Character.xlsx
    //     public partial class CharacterStarConsumeKey : ExcelRecordCompositeKey, System.IEquatable<CharacterStarConsumeKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = CharacterID * 1000 + Star;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     // Dungeon.xlsl
    //     public partial class DungeonGradeKey : ExcelRecordCompositeKey, System.IEquatable<DungeonGradeKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = ID * 100 + GradeType;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class LoveCardLevelKey : ExcelRecordCompositeKey, System.IEquatable<LoveCardLevelKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = Id * 100 + LoveCardLevel;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class DatingSceneInterestKey : ExcelRecordCompositeKey, System.IEquatable<DatingSceneInterestKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = RoleId * 1000 + SceneId;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     // LoveSystem.xlsl
    //     public partial class ElementLevelKey : ExcelRecordCompositeKey, System.IEquatable<ElementLevelKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = MainRoleId * 100 + ObjectRoleId * 20 + ElementLevel * 10 + (int) LikeAttrType;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class LoveLevelKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             unchecked
    //             {
    //                 if (0 == _hashCode)
    //                 {
    //                     _hashCode = MainRoleId;
    //                     _hashCode = (_hashCode * 397) ^ ObjectRoleId;
    //                     _hashCode = (_hashCode * 397) ^ LoveLevel;
    //                 }
    //
    //                 return _hashCode;
    //             }
    //         }
    //     }
    //
    //     public partial class LoveSetKey : ExcelRecordCompositeKey, System.IEquatable<LoveSetKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             unchecked
    //             {
    //                 if (0 == _hashCode)
    //                 {
    //                     _hashCode = MainRoleId;
    //                     _hashCode = (_hashCode * 397) ^ ObjectRoleId;
    //                 }
    //
    //                 return _hashCode;
    //             }
    //         }
    //     }
    //
    //     public partial class LoveNameDesKey : ExcelRecordCompositeKey, System.IEquatable<LoveNameDesKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             unchecked
    //             {
    //                 if (0 == _hashCode)
    //                 {
    //                     _hashCode = MainRoleId;
    //                     _hashCode = (_hashCode * 397) ^ ObjectRoleId;
    //                     _hashCode = (_hashCode * 397) ^ TitleId;
    //                 }
    //
    //                 return _hashCode;
    //             }
    //         }
    //     }
    //
    //     public partial class LoveUnlockAttrKey : ExcelRecordCompositeKey, System.IEquatable<LoveUnlockAttrKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = UnlockId * 100 + Level;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class LovePuzzleKey : ExcelRecordCompositeKey, System.IEquatable<LovePuzzleKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             unchecked
    //             {
    //                 if (0 == _hashCode)
    //                 {
    //                     _hashCode = MainRoleId;
    //                     _hashCode = (_hashCode * 397) ^ ObjectRoleId;
    //                     _hashCode = (_hashCode * 397) ^ PuzzleID;
    //                 }
    //
    //                 return _hashCode;
    //             }
    //         }
    //     }
    //
    //     public partial class DatingRoleInitKey : ExcelRecordCompositeKey, System.IEquatable<DatingRoleInitKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = RoleId * 10 + MoodType;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class ManaAwakeKey : ExcelRecordCompositeKey, System.IEquatable<ManaAwakeKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = CharacterID * 100 + AwakeLevel;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class ManaStrengthenKey : ExcelRecordCompositeKey, System.IEquatable<ManaStrengthenKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = CharacterID * 100 + StrengthenNodeID;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class ManaStrengthenGraphKey : ExcelRecordCompositeKey, System.IEquatable<ManaStrengthenGraphKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = this.StrengthenNodeID * 100 + ElementID;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //
    //     public partial class DiningRoleMaterialKey : ExcelRecordCompositeKey, System.IEquatable<DiningRoleMaterialKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = RoleId * 1000 + MaterialId;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class DiningRoleFlavourKey : ExcelRecordCompositeKey, System.IEquatable<DiningRoleFlavourKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = RoleId * 1000 + FlavourId;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class ForceUnlockKey : ExcelRecordCompositeKey, System.IEquatable<ForceUnlockKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = ForceId * 1000 + ForceLevel;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class SubAchievementKey : ExcelRecordCompositeKey, System.IEquatable<SubAchievementKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             if (0 == _hashCode)
    //             {
    //                 _hashCode = AchievementId * 1000 + SubAchievementId;
    //             }
    //
    //             return _hashCode;
    //         }
    //     }
    //
    //     public partial class InitRoleGiftTagKey : ExcelRecordCompositeKey, System.IEquatable<InitRoleGiftTagKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RoleId + TagId * 1000000;
    //         }
    //     }
    //
    //     public partial class TourPerformerKey : ExcelRecordCompositeKey, System.IEquatable<TourPerformerKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             return TourID + PerformerSlot * 10000;
    //         }
    //     }
    //
    //     public partial class TourDailyKey : ExcelRecordCompositeKey, System.IEquatable<TourDailyKey>
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RegionID * 10000 + RegionLevel;
    //         }
    //     }
    //
    //     // public partial class DungeonEntranceKey : ExcelRecordCompositeKey, System.IEquatable<DungeonEntranceKey>
    //     // {
    //     //     public override int GetHashCode()
    //     //     {
    //     //         return DungeonSubType + EntranceID * 10000;
    //     //     }
    //     // }
    //
    //     // public partial class StoryChallengeKey: ExcelRecordCompositeKey, System.IEquatable<StoryChallengeKey>
    //     // {
    //     //     public override int GetHashCode()
    //     //     {
    //     //         // return VolumeID * 1000000 + ChallengeID;
    //     //         return 0;
    //     //     }
    //     // }
    //
    //     public partial class EquipStrengthenListKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RarityID + EquipStrengthenLevel * 10;
    //         }
    //     }
    //
    //     public partial class EquipDecomposeListKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RarityID + DecomposeID * 10;
    //         }
    //     }
    //
    //     public partial class EquipBreakModesKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return EquipBreakLevel + RarityID * 100 + EquipBreakModeID * 1000;
    //         }
    //     }
    //
    //     public partial class CharacterSkillLevelUpKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CharacterSkillID + CharacterSkillLevel * 1000;
    //         }
    //     }
    //
    //     public partial class CharacterSkillLevelModesKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CharacterSkillLevelModeID + CharacterSkillLevel * 10;
    //         }
    //     }
    //
    //     public partial class BundleKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return (BundleID - 500000) * 1000000 + ContainItemID;
    //         }
    //     }
    //
    //     public partial class FragmentsSystemKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return (CharacterID - 1000000) * 10000 + NodeID;
    //         }
    //     }
    //
    //     public partial class FragmentsGameStarKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return ID * 100 + Star;
    //         }
    //     }
    //
    //     public partial class FragmentsDungeonKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return NodeID * 100 + GradeType;
    //         }
    //     }
    //
    //     public partial class KanbanPostionOffsetKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RoleID + KanbanArea * 1000000;
    //         }
    //     }
    //
    //     public partial class RoleHobbyKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RoleId + HobbyId * 1000000;
    //         }
    //     }
    //
    //     
    //
    //     public partial class CharacterRarityUpAttritubeKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return ItemID * 100 + Rarity;
    //         }
    //     }
    //
    //     public partial class RelevantStoryKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RoleID * 1000 + IdolID;
    //         }
    //     }
    //     
    //
    //     //CharSkillLvlUp.xlsx
    //     public partial class CharacterSkillsKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CharacterID * 10 + CharacterSkillID;
    //         }
    //     }
    //
    //     public partial class CharSkillLevelUpKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CharacterSkillID + CharacterSkillLevel * 100000;
    //         }
    //     }
    //
    //     public partial class DancePowerKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CharacterID * 100 + DancePowerLV;
    //         }
    //     }
    //
    //     public partial class MonsterModelKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             unchecked
    //             {
    //                 if (0 == _hashCode)
    //                 {
    //                     _hashCode = MonsterModelID;
    //                     _hashCode = (_hashCode * 397) ^ MonsterLevel;
    //                 }
    //
    //                 return _hashCode;
    //             }
    //         }
    //     }
    //     
    //
    //     //public partial class 
    //     // public partial class OfflineArenaRankScoreKey
    //     // {
    //     //     public override int GetHashCode()
    //     //     {
    //     //         return RankID * 10000 + SubRank;
    //     //     }
    //     // }
    //
    //     public partial class OfflineArenaSeasonRewardKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return SeasonID * 10000 + RankRegion.x;
    //         }
    //     }
    //
    //     public partial class RoguelikePlotTypeKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return PlotTypeId * 100 + PlotSubType;
    //         }
    //     }
    //
    //     public partial class CulFacilityLevelKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return FacilityLevel * 1000 + FacilityId;
    //         }
    //     }
    //
    //     public partial class CulSupportCardLevelUpKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return Level * 1000 + Id;
    //         }
    //     }
    //
    //     public partial class CulSupportSkillKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CardId * 1000 + Id;
    //         }
    //     }
    //
    //     public partial class CulSupportStoryKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CardId * 1000 + Id;
    //         }
    //     }
    //
    //     public partial class CulSupportStoryOptionKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return StoryId * 1000 + OptionId;
    //         }
    //     }
    //
    //     public partial class CulSupportOmamoriKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return Id * 1000 + CardId;
    //         }
    //     }
    //
    //     public partial class CulRelevantStoryKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return RoleId * 100000 + IdolId;
    //         }
    //     }
    //
    //     public partial class CulCalendarKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CalendarId * 100 + Round;
    //         }
    //     }
    //
    //     public partial class CulStoryOptionKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return OptionId * 100000 + StoryId;
    //         }
    //     }
    //
    //     public partial class OpenDayMissionKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return DayID * 100 + MissionID;
    //         }
    //     }
    //
    //     public partial class BattlePassLevelKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CycleID * 100 + Level;
    //         }
    //     }
    //
    //     public partial class OmamoriAttrKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return AttrPatternId * 100 + Level;
    //         }
    //     }
    //
    //     public partial class OmamoriCoreAttrKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return AttrPatternId * 100 + Level;
    //         }
    //     }
    //
    //     public partial class SkillPowerKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return CharacterQuality * 1000 + SkillType;
    //         }
    //     }
    //
    //     public partial class CulMaibeatSongConfigKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             return SongId * 10 + DifficultyLevel;
    //         }
    //     }
    //
    //     public partial class OmamoriLevelKey
    //     {
    //         public sealed override int GetHashCode()
    //         {
    //             return Level * 10 + Quality;
    //         }
    //     }
    //
    //     public partial class OmamoriStarKey
    //     {
    //         public sealed override int GetHashCode()
    //         {
    //             return Star * 10 + Quality;
    //         }
    //     }
    //
    //     public partial class OmamoriDecomposeKey
    //     {
    //         public sealed override int GetHashCode()
    //         {
    //             return OmamoriSuitID * 10 + Quality;
    //         }
    //     }
    //
    //     public partial class LobbyNPCGroupDetailKey
    //     {
    //         public sealed override int GetHashCode()
    //         {
    //             return PerformNPCGroupId * 1000 + PerformNPC;
    //         }
    //     }
    //
    //     public partial class CharacterRarityUpSkillKey
    //     {
    //         public override int GetHashCode()
    //         {
    //             unchecked
    //             {
    //                 if (0 == _hashCode)
    //                 {
    //                     _hashCode = ItemID;
    //                     _hashCode = (_hashCode * 397) ^ Rarity;
    //                 }
    //
    //                 return _hashCode;
    //             }
    //         }
    //     }
    //     public partial class DungeonBossesKey
    //     {
    //         public sealed override int GetHashCode()
    //         {
    //             return ID * 10 + BossID;
    //         }
    //     }
}