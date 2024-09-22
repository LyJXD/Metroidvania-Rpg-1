using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // ���׷��Ŀ��λ��
    public Transform farBackground, middleBackground1, middleBackground2, middleBackground3;   // Զ���뱳��λ�ú��о��뱳��λ��
    private Vector2 lastPos;    // ���һ�ε����λ��

    private void Start()
    {
        lastPos = transform.position;   // ��¼����ĳ�ʼλ��
    }
    private void Update()
    {
        // �������λ������Ϊ���׷��Ŀ���λ�ã���������һ���Ĵ�ֱ��Χ��
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);


        // �����������һ֡�͵�ǰ֮֡���ƶ��ľ���
        Vector2 amountToMoce = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        //��������ƶ��ľ��룬�ƶ�Զ�������б�����λ��
        farBackground.position += new Vector3(amountToMoce.x, amountToMoce.y, 0f);
        middleBackground1.position += new Vector3(amountToMoce.x * 0.1f, amountToMoce.y * 0.1f);
        middleBackground2.position += new Vector3(amountToMoce.x * 0.3f, amountToMoce.y * 0.3f);
        middleBackground3.position += new Vector3(amountToMoce.x * 0.5f, amountToMoce.y * 0.5f);

        lastPos = transform.position;   // �������һ�ε����λ��
    }
    
}
