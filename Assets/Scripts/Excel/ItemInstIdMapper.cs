// using System.Collections.Generic;
// using System.Runtime.CompilerServices;
// using Arukas.Game;
// using InstanceIdType = System.UInt64;
//
// // ReSharper disable BuiltInTypeReferenceStyle
// namespace Excel
// {
//     /// <summary>
//     /// 所有Item的instanceID反查UniqueID
//     /// </summary>
//     /// <remarks>
//     /// 之前inst_id确实不能100%唯一确定一个物品。
//     /// 我这边现在打算优化一下，把inst_id做成全局唯一的，只通过inst_id就能确定好是哪个物品。
//     /// 其实之前也基本不可能一样 但是是有极小概率重复 因为之前是用的生成时的纳秒时间戳做的inst_id
//     /// 后面打算用snowflake生成inst_id, 能保证100%不重复
//     /// 现在有inst_id的应该就只有角色和装备。inst_id会是全局唯一的，不分是角色还是装备，或哪个玩家哪个角色。全局唯一
//     /// 哦对。物品也有inst_id。也是全局唯一，不分类别
//     /// </remarks>
//     /// <remarks>
//     /// 角色不再使用InstanceID
//     /// </remarks>
//     public class ItemInstIdMapper
//     {
//         public static readonly ItemInstIdMapper Instance = new ItemInstIdMapper();
//
//         private readonly Dictionary<InstanceIdType, ItemUniqueID> _mapper =
//             new Dictionary<InstanceIdType, ItemUniqueID>();
//
//         public void Touch()
//         {
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public bool TryGetItemUId(InstanceIdType key, out ItemUniqueID uniqueID)
//         {
//             return _mapper.TryGetValue(key, out uniqueID);
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public void UpdateCache(ItemUniqueID uniqueID)
//         {
//             _mapper[uniqueID.InstanceID] = uniqueID;
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public void ExpireCache(ItemUniqueID uniqueID)
//         {
//             ExpireCache(uniqueID.InstanceID);
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public void ExpireCache(InstanceIdType key)
//         {
//             _mapper.Remove(key);
//         }
//
//         public void ExpireAll()
//         {
//             _mapper.Clear();
//         }
//     }
//
//     public static class InstanceIdExtensions
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static ItemUniqueID ToItemUId(this InstanceIdType instanceId)
//         {
//             ItemInstIdMapper.Instance.TryGetItemUId(instanceId, out var uniqueID);
//             return uniqueID;
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool ToItemUId(this InstanceIdType instanceId, out ItemUniqueID uniqueID)
//         {
//             return ItemInstIdMapper.Instance.TryGetItemUId(instanceId, out uniqueID);
//         }
//     }
// }