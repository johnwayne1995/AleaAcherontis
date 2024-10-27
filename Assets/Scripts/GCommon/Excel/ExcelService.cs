using Excel;

namespace GCommon.Excel
{
    public class ExcelService
    {
        // 私有静态变量，用于保存单例实例
        private static ExcelService _instance;
        private MatchLevelTable table;
    
        // 用于线程锁的对象
        private static readonly object _lock = new object();

        // 私有构造函数，防止外部实例化
        private ExcelService()
        {
            table = UnityEditor.AssetDatabase.LoadAssetAtPath<MatchLevelTable>("Assets/Resources/Configs/StageConfig/" +
                                                                               "MatchLevelTable" + ".asset");
            table.CreateDictionary();
        }

        // 公共静态属性，用于获取单例实例
        public static ExcelService Instance
        {
            get
            {
                // 双重检查锁定
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ExcelService();
                        }
                    }
                }
                return _instance;
            }
        }
        public MatchLevelTable GetMatchLevel()
        {
            return table;
        }
    }
}