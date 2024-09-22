using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // 相机追踪目标位置
    public Transform farBackground, middleBackground1, middleBackground2, middleBackground3;   // 远距离背景位置和中距离背景位置
    private Vector2 lastPos;    // 最后一次的相机位置

    private void Start()
    {
        lastPos = transform.position;   // 记录相机的初始位置
    }
    private void Update()
    {
        // 将相机的位置设置为相机追踪目标的位置，但限制在一定的垂直范围内
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);


        // 计算相机在上一帧和当前帧之间移动的距离
        Vector2 amountToMoce = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        //根据相机移动的距离，移动远背景和中背景的位置
        farBackground.position += new Vector3(amountToMoce.x, amountToMoce.y, 0f);
        middleBackground1.position += new Vector3(amountToMoce.x * 0.1f, amountToMoce.y * 0.1f);
        middleBackground2.position += new Vector3(amountToMoce.x * 0.3f, amountToMoce.y * 0.3f);
        middleBackground3.position += new Vector3(amountToMoce.x * 0.5f, amountToMoce.y * 0.5f);

        lastPos = transform.position;   // 更新最后一次的相机位置
    }
    
}
