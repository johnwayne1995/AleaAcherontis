using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;//������Ÿ���λ�õ���

    public bool roomLeft, roomRight, roomUp, roomDown;//�ж����������Ƿ��з���

    public Text text;
    public int stepToStart;//�����ʼ����������

    public int doorNumber;//��ǰ������ŵ�����/�������

    void Start()
    {
        //��Ӧ��������Ƿ���ʾ��������Ӧ�����Ƿ�����������
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    public void UpdateRoom()
    {
        //��������ʼ����������
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 9));

        text.text = stepToStart.ToString();

        //������������з��䣬���ۼ�1�����ڿ��ĸ�����ֻ��һ���ţ�
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