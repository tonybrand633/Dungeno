using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRoom : MonoBehaviour
{
    static public int Room_H = 11;
    static public int Room_W = 16;
    static public int Wall_T = 2;

    static public int roomMaxNumX = 9;
    static public int roomMaxNumY = 9;

    [Header("Set in Inspector")]
    public bool keepInRoom = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (keepInRoom) 
        {
            Vector2 rPos = roomPos;
            rPos.x = Mathf.Clamp(rPos.x, Wall_T, Room_W - Wall_T -1);
            rPos.y = Mathf.Clamp(rPos.y, Wall_T, Room_H - Wall_T -1);
            roomPos = rPos;
        }
    }


    //目标在房间的相对位置
    public Vector2 roomPos 
    {
        get 
        {
            Vector2 tPos = transform.position;
            tPos.x %= Room_W;
            tPos.y %= Room_H;
            return tPos;
        }
        set 
        {
            Vector2 curRoomPosition = roomNum;
            curRoomPosition.x *= Room_W;
            curRoomPosition.y *= Room_H;
            curRoomPosition += value;
            transform.position = curRoomPosition;
        }
    }

    //目标的房间编号
    public Vector2 roomNum 
    {
        //放回房间的x,y坐标
        get 
        {
            Vector2 tPos = transform.position;
            tPos.x = Mathf.Floor(tPos.x / Room_W);
            tPos.y = Mathf.Floor(tPos.y / Room_H);
            return tPos;
        }
        set 
        {
            Vector2 curPos = transform.position;
            Vector2 roomPosition = value;
            roomPosition.x *= Room_W;
            roomPosition.y *= Room_H;
            transform.position = curPos + roomPosition;
        }
    }
}
