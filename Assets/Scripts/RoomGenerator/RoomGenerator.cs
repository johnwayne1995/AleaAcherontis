using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up ,down ,left ,right };
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;

    public LayerMask roomLayer; //用于监测是否相同位置已有房间生成

    public int maxStep;
    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();

    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();


    void Start()
    {
        //生成指定数量的房间
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改变point位置
            ChangePointPos();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor; //显示第一个房间的颜色

        
        endRoom = rooms[0].gameObject;


        //找到最终房间并标记颜色
        foreach (var room in rooms)
        {

            /*            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)

                        {
                            endRoom = room.gameObject;
                        }*/

            SetupRoom(room, room.transform.position);
        }

        FindEndRoom();

        endRoom.GetComponent<SpriteRenderer>().color = endColor;

        // 在所有房间生成并位置确定之后，更新所有房间的信息
        foreach (var room in rooms)
        {
            room.UpdateRoom();
        }

        // 设置初始房间的Text组件的文本
        Room startRoomComponent = rooms[0].GetComponent<Room>();
        if (startRoomComponent.text != null)
        {
            startRoomComponent.text.text = "初始房间" + startRoomComponent.stepToStart.ToString();
        }

        // 设置最终房间的Text组件的文本
        Room endRoomComponent = endRoom.GetComponent<Room>();
        if (endRoomComponent.text != null)
        {
            endRoomComponent.text.text = "最终房间" + endRoomComponent.stepToStart.ToString();
        }

    }


    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            //直接获得当前场景名字，重新加载
        }
    }
 
    public void ChangePointPos()
    {
        do
        {


            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                //四个方向
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));

    }

    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        // 判断上下左右是否有房间，就生成门
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);


        // 表示步数是多少
        newRoom.UpdateRoom();

    }

    public void FindEndRoom()
    {
        //查找每个房间最大的数字是多少
        for(int i=0;i<rooms.Count;i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }

        //获得最大值房间和次大值
        foreach(var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);

        }

        for (int i=0;i< farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);

        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }

        //如果有单个开口的房间，最终房间就是它
        if (oneWayRooms.Count !=0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }

        else  //否则最终房间就在最远的房间里随机
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }



    }
}