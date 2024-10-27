// using System;
// using System.Collections.Generic;
// using System.Linq;
//
// namespace Excel
// {
//     public class ItemRecordDatabase : Singleton<ItemRecordDatabase>
//     {
//         private Dictionary<int, ItemRecord> _mapItemRecord = new Dictionary<int, ItemRecord>();
//
//         public ItemRecordDatabase()
//         {
//             try
//             {
//                 if (ExcelManager.Instance.ReadyToUse)
//                 {
//                     AddItems(ExcelManager.Instance.ItemTable.RecordList);
//                     AddItems(ExcelManager.Instance.HeadFrameTable.RecordList);
//                     AddItems(ExcelManager.Instance.HeadPicTable.RecordList);
//                     AddItems(ExcelManager.Instance.CharacterTable.RecordList);
//                     AddItems(ExcelManager.Instance.LimitedCardTable.RecordList);
//                     AddItems(ExcelManager.Instance.CoreEquipTable.RecordList);
//                     AddItems(ExcelManager.Instance.SubEquipTable.RecordList);
//                     AddItems(ExcelManager.Instance.RoguelikeBuffRuneTable.RecordList);
//                     AddItems(ExcelManager.Instance.RoguelikeTrialCharTable.RecordList);
//                 }
//                 else
//                 {
//                     throw new SystemException("ExcelManager s not ready to use");
//                 }
//             }
//             catch (Exception e)
//             {
//                 throw new Exception($"ItemDatabase initialize error : {e.Message}");
//             }
//         }
//         protected override void OnDestroy()
//         {
//             _mapItemRecord.Clear();
//             _mapItemRecord = null;
//         }
//         
//
//         private void AddItems<T>(in List<T> recordList) where T : ItemRecord
//         {
//             for (int i = 0; i < recordList.Count; i++)
//             {
//                 T t = recordList[i];
//                 if (!_mapItemRecord.ContainsKey(t.ItemID))
//                 {
//                     _mapItemRecord.Add(t.ItemID, t);
//                 }
//                 else
//                 {
//                     var existItem = _mapItemRecord[t.ItemID];
//                     GameDebug.LogError($"ItemDatabase add {t.GetType().Name}({t.ItemID}) error : duplication id in {existItem.GetType().Name}({existItem.ItemID})");
//                 }
//             }
//         }
//         
//         public ItemRecord GetItem(int id)
//         {
//             ItemRecord record = null;
//             if (_mapItemRecord.TryGetValue(id, out record))
//             {
//                 return record;
//             }
//             else
//             {
//                 GameDebug.LogError($"ItemDatabase get ({id}) error : do not exist");
//             }
//             return null;
//         }
//         
//         public T GetItem<T>(int id) where T : ItemRecord
//         {
//             ItemRecord record = null;
//             if (_mapItemRecord.TryGetValue(id, out record))
//             {
//                 return record as T;
//             }
//             else
//             {
//                 GameDebug.LogError($"ItemDatabase get {typeof(T).Name}({id}) error : do not exist");
//             }
//             return null;
//         }
//
//         public IEnumerable<BundleRecord> GetBundleItems(int id)
//         {
//             if (_mapItemRecord.TryGetValue(id, out var record))
//             {
//                 if (record.Type != ItemType.Bundle)
//                 {
//                     // GameDebug.Log("ItemDatabase get item list of bundle ({0}) error : this item is not bundle!", id);
//                     return null;
//                 }
//
//                 var result = ExcelManager.Instance.BundleTable.RecordList.Where(r => r.Key.BundleID == id);
//                 return result;
//             }
//             GameDebug.LogError("ItemDatabase get item list of bundle ({0}) error : do not exist bundle!", id);
//             return null;
//         }
//     }
// }