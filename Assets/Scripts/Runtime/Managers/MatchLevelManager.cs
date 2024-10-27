using Excel;
using GCommon.Excel;
using UnityEngine;
namespace Managers
{
    public class MatchLevelManager : TGameManager<MatchLevelManager>
    {
        private MatchLevelTable table;
        public int curRoom;

        protected override void OnAwake()
        {
            base.OnAwake();
            table = ExcelService.Instance.GetMatchLevel();
            curRoom = table.RecordList[0].RoomID;
        }
        
        public MatchLevelTable GetMatchLevelTable()
        {
            return table;
        }
    }
}
