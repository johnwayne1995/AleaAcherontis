using UnityEngine;
using System.Collections.Generic;
// using GCommon;

namespace Excel
{
    public class ExcelTableBase : ScriptableObject
    {
        protected internal virtual void CreateDictionary()
        {
            string message = string.Format("Please implement {0} 's CreateDictionary()", GetType().Name);
            throw new System.NotImplementedException(message);
        }
        
        protected internal virtual void Clear()
        {
            string message = string.Format("Please implement {0} 's Clear()", GetType().Name);
            throw new System.NotImplementedException(message);
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
            {
                Debug.LogFormat("ExcelTable {0} OnDestroy", name);
                Clear();
            }
        }
    }
    
    public class ExcelTable<RecordType> : ExcelTableBase
        where RecordType : class
    {
        //[EditorGUITable.Table]
        #if UNITY_EDITOR
        [Sirenix.OdinInspector.TableList(HideToolbar = true, AlwaysExpanded = true, ScrollViewHeight = 800)]
        #endif
        [SerializeField]
        protected List<RecordType> list;

#if UNITY_EDITOR
        [SerializeField] private string excelName;
#endif
        [SerializeField] private int version;
        
        public List<RecordType> RecordList
        {
            get
            {
                return list;
            }
        }

        public int RecordCount
        {
            get
            {
                if (null != list) 
                    return list.Count;
                else
                    return 0;
            }
        }

        public RecordType GetRecordAt(int index)
        {
            if (index < RecordCount)
                return list[index];
            else
                return null;
        }

        // internal void UpdateRecord(List<object> lstRecord)
        // {
        //     UpdateRecord(lstRecord,String.Empty);
        // }
        
        internal void UpdateRecord(List<object> lstRecord,string excelName)
        {
            if (null != list)
                list.Clear();

            list = new List<RecordType>(lstRecord.Count);
            for (int index = 0; index != lstRecord.Count; ++index)
            {
                list.Add(lstRecord[index] as RecordType);
            }
#if UNITY_EDITOR
            this.excelName = excelName;
#endif
        }
    }
}
