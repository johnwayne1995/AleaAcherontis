using Excel;
using GCommon.Excel;
using UnityEngine;
namespace Managers
{
    public class MatchLevelManager : TGameManager<MatchLevelManager>
    {
        private MatchLevelTable table;
        public int curRoom;
        private int curRoomIndex;

        protected override void OnAwake()
        {
            base.OnAwake();
            curRoomIndex = 0;
            table = ExcelService.Instance.GetMatchLevel();
            curRoom = table.RecordList[curRoomIndex].RoomID;
        }
        
        public void ResetCurRoom()
        {
            curRoomIndex = 0;
            curRoom = table.RecordList[curRoomIndex].RoomID;
        }
        public void SkipCurRoom()
        {
            curRoomIndex++;
            curRoom = table.RecordList[curRoomIndex].RoomID;
        }
        public MatchLevelTable GetMatchLevelTable()
        {
            return table;
        }
    }
}
