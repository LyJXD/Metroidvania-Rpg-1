using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalarraxBackground : MonoBehaviour // �Ӳ��
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);  // ������ĵ��뱳�����ĵ�ľ���
        float distanceToMove = cam.transform.position.x * parallaxEffect;       // �����ƶ�����

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if(distanceMoved > xPosition + length)
        {
            xPosition += length;
        }
        else if(distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
    }
}
