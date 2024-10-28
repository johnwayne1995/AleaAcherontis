using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;//用来存放各个位置的门

    public bool roomLeft, roomRight, roomUp, roomDown;//判断上下左右是否有房间

    public Text text;
    public int stepToStart;//距离初始点的网格距离

    public int doorNumber;//当前房间的门的数量/入口数量

    void Start()
    {
        //对应方向的门是否显示，关联对应方向是否有其他房间
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    public void UpdateRoom()
    {
        //计算距离初始点的网格距离
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 9));

        text.text = stepToStart.ToString();

        //如果上下左右有房间，则累加1（用于看哪个房间只有一道门）
        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }

}