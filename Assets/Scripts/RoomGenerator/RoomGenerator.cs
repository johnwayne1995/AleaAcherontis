using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up ,down ,left ,right };
    public Direction direction;

    [Header("������Ϣ")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("λ�ÿ���")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;

    public LayerMask roomLayer; //���ڼ���Ƿ���ͬλ�����з�������

    public int maxStep;
    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();

    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType wallType;

 


    void Start()
    {
        //����ָ�������ķ���
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //�ı�pointλ��
            ChangePointPos();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor; //��ʾ��һ���������ɫ

        
        endRoom = rooms[0].gameObject;


        //�ҵ����շ��䲢�����ɫ
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

        // �����з������ɲ�λ��ȷ��֮�󣬸������з������Ϣ
        foreach (var room in rooms)
        {
            room.UpdateRoom();
        }

        // ���ó�ʼ�����Text������ı�
        Room startRoomComponent = rooms[0].GetComponent<Room>();
        if (startRoomComponent.text != null)
        {
            startRoomComponent.text.text = "��ʼ����101" + startRoomComponent.stepToStart.ToString();
        }

        // �������շ����Text������ı�
        Room endRoomComponent = endRoom.GetComponent<Room>();
        if (endRoomComponent.text != null)
        {
            endRoomComponent.text.text = "���շ���199" + endRoomComponent.stepToStart.ToString();
        }

    }


    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            //ֱ�ӻ�õ�ǰ�������֣����¼���
        }
    }
 
    public void ChangePointPos()
    {
        do
        {


            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                //�ĸ�����
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
        // �ж����������Ƿ��з��䣬��������
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);


        // ��ʾ�����Ƕ���
        newRoom.UpdateRoom();

        switch(newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(wallType.singleUp, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleBottom, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(wallType.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomDown)
                    Instantiate(wallType.doubleLB, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.doubleUB, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.doubleRB, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleLRB, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleURB, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.tripleLUB, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.fourDoors, roomPosition, Quaternion.identity);
                break;
        }

    }

    public void FindEndRoom()
    {
        //����ÿ���������������Ƕ���
        for(int i=0;i<rooms.Count;i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }

        //������ֵ����ʹδ�ֵ
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

        //����е������ڵķ��䣬���շ��������
        if (oneWayRooms.Count !=0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }

        else  //�������շ��������Զ�ķ��������
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }



    }
}

[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleBottom,
        doubleLU,doubleLR,doubleLB,doubleUR,doubleUB,doubleRB,
        tripleLUR,tripleLUB,tripleURB,tripleLRB,
        fourDoors;

}