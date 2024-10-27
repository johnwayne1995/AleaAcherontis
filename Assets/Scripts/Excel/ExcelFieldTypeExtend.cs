using UnityEngine;
using System.Runtime.CompilerServices;

namespace Excel
{
    [System.Serializable]
    [AttributExcelExtendType(typeof(SkillPair), "SkillPair")]
    public struct SkillPair
    {
        public int id;
        public short level;

        public SkillPair(int id, short level)
        {
            this.id = id;
            this.level = level;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(EffectPair), "EffectPair")]
    public struct EffectPair
    {
        public string effectName;
        public float delayTime;
        public bool stopWithSource;
        public bool useSourceSpeed;

        public EffectPair(string effectName, float delayTime, bool stopWithSource, bool useSourceSpeed)
        {
            this.effectName = effectName;
            this.delayTime = delayTime;
            this.stopWithSource = stopWithSource;
            this.useSourceSpeed = useSourceSpeed;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(AnimationClipTestPair), "AnimationClipTestPair")]
    public struct AnimationClipTestPair
    {
        public string clipName;
        public int duration;
        public AnimationClipTestPair(string clipName, int duration)
        {
            this.clipName = clipName;
            this.duration = duration;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(AttributeStatusPair), "AttributeStatusPair")]
    public struct AttributeStatusPair
    {
        public byte attribute_type;
        public byte compare;
        public float attribute_value;
        public int status_id;
        public short status_level;

        public AttributeStatusPair(byte attribute_type, byte compare, float attribute_value, int status_id, short status_level)
        {
            this.attribute_type = attribute_type;
            this.compare = compare;
            this.attribute_value = attribute_value;
            this.status_id = status_id;
            this.status_level = status_level;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(StringKey), "StringKey")]
    public struct StringKey
    {
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(FlagInt), "flagInt")]
    public struct FlagInt
    {
        [SerializeField] private int value;
        public int Value => value;

        public FlagInt(int v) => value = v;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(FlagInt v) => v.value;
    }

    // [System.Serializable]
    // [AttributExcelExtendType(typeof(AttributePair),"AttributePair")]
    // public struct AttributePair
    // {
    //     public AttributeType attributeType;
    //     public int value;
    //
    //     public AttributePair(AttributeType attributeType, int value)
    //     {
    //         this.attributeType = attributeType;
    //         this.value = value;
    //     }
    // }

    [System.Serializable]
    [AttributExcelExtendType(typeof(ItemPair), "ItemPair")]
    public struct ItemPair
    {
        public ERewardType rewardType;
        public int id;
        public int count;

        public ItemPair(int id, int count)
        {
            this.rewardType = ERewardType.Item;
            this.id = id;
            this.count = count;
        }

        public ItemPair(ERewardType rewardType, int id, int count)
        {
            this.rewardType = rewardType;
            this.id = id;
            this.count = count;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(UnlockPair), "UnlockPair")]
    public struct UnlockPair
    {
        public EUnlockType unlockType;
        public int id;
        public int count;

        public UnlockPair(EUnlockType unlockType, int id, int count)
        {
            this.unlockType = unlockType;
            this.id = id;
            this.count = count;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(ResourceDisplayPair), "ResourceDisplayPair")]
    public struct ResourceDisplayPair
    {
        public int displayType;
        public int itemID;

        public ResourceDisplayPair(int displayType, int itemID)
        {
            this.displayType = displayType;
            this.itemID = itemID;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(PerformerAbilityPair), "PerformerAbilityPair")]
    public struct PerformerAbilityPair
    {
        public ERequiryInfoType reqType;
        public int relation;
        public int param;

        public PerformerAbilityPair(ERequiryInfoType reqType, int relation, int param)
        {
            this.reqType = reqType;
            this.relation = relation;
            this.param = param;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(ParamTriple), "ParamTriple")]
    public struct ParamTriple
    {
        public int x;
        public int y;
        public int triggerId;

        public ParamTriple(int x, int y, int triggerId)
        {
            this.x = x;
            this.y = y;
            this.triggerId = triggerId;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(ParamPair), "ParamPair")]
    public struct ParamPair
    {
        public int param1;
        public int param2;

        public ParamPair(int param1, int param2)
        {
            this.param1 = param1;
            this.param2 = param2;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(SpritePair), "SpritePair")]
    public struct SpritePair
    {
        public string atlasName;
        public string spriteName;

        public SpritePair(string atlasName, string spriteName)
        {
            this.atlasName = atlasName;
            this.spriteName = spriteName;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(TexturePair), "TexturePair")]
    public struct TexturePair
    {
        public string textureName;

        public TexturePair(string textureName)
        {
            this.textureName = textureName;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(Scope), "Scope")]
    public struct Scope
    {
        public int x;
        public int y;

        public bool isLeftOpen;
        public bool isRightOpen;

        public Scope(int x, int y) : this(x, y, false, false)
        {
        }

        public Scope(int x, int y, bool isLeftOpen, bool isRightOpen)
        {
            this.x = x;
            this.y = y;
            this.isLeftOpen = isLeftOpen;
            this.isRightOpen = isRightOpen;
        }

        public bool IsInRegion(int value)
        {
            if (isLeftOpen)
            {
                if (value <= x)
                {
                    return false;
                }
            }
            else
            {
                if (value < x)
                {
                    return false;
                }
            }
            if (isRightOpen)
            {
                if (value >= y)
                {
                    return false;
                }
            }
            else
            {
                if (value > y)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(Scope r1, Scope r2)
        {
            return !(r1 == r2);
        }

        public static bool operator ==(Scope r1, Scope r2)
        {
            if (r1.x == r2.x && r1.y == r2.y && r1.isLeftOpen == r2.isLeftOpen && r1.isRightOpen == r2.isRightOpen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(WeaponSkillPair), "WeaponSkillPair")]
    public struct WeaponSkillPair
    {
        public uint weaponId;
        public uint skillId;

        public WeaponSkillPair(uint weaponId, uint skillId)
        {
            this.weaponId = weaponId;
            this.skillId = skillId;
        }
    }

    [System.Serializable]
    [AttributExcelExtendType(typeof(GuideTriggerPair), "GuideTriggerPair")]
    public struct GuideTriggerPair
    {
        public const char GuideElementSplit = '&';
        public uint triggerId;
        public string[] param;

        public GuideTriggerPair(uint triggerId, string param)
        {
            this.triggerId = triggerId;
            this.param = param.Split(GuideElementSplit);
        }

        public void TryGetParamInt(out int param1, out int param2, out int param3)
        {
            param1 = 0;
            param2 = 0;
            param3 = 0;
            if (this.param.Length > 0)
                int.TryParse(this.param[0], out param1);
            if (this.param.Length > 1)
                int.TryParse(this.param[1], out param2);
            if (this.param.Length > 2)
                int.TryParse(this.param[2], out param3);
        }
    }
}
