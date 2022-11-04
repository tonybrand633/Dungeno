using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInRoom : MonoBehaviour
{
    public static int WALL_H = 11;
    public static int WALL_W = 16;
    public static int WALL_T = 2;

    static public int MAX_RM_X = 9;
    static public int MAX_RM_Y = 9;

    static public Vector2[] DOORSPOS = new Vector2[]
    {
        new Vector2(14,5), //0
        new Vector2(7.5f,9f), //1
        new Vector2(1,5), //2
        new Vector2(7.5f,1) //3
    };

    public bool keepItInRoom;
    public float gridMult = 1;

    void LateUpdate()
    {
        if (keepItInRoom) 
        {
            Vector3 pos = roomPos;
            pos.x = Mathf.Clamp(pos.x, WALL_T, WALL_W - WALL_T - 1);
            pos.y = Mathf.Clamp(pos.y, WALL_T, WALL_H - WALL_T - 1);
            roomPos = pos;
        }    
    }

    public Vector2 roomPos 
    {
        get 
        {
            Vector2 vec = transform.position;
            vec.x %= WALL_W;
            vec.y %= WALL_H;
            return vec;
        }
        //获得当前的roomNum,来设置transform.position;
        set 
        {
            Vector2 vec = roomNum;
            vec.x *= WALL_W;
            vec.y *= WALL_H;
            vec += value;
            transform.position = vec;
        }
    }

    public Vector2 roomNum
    {
        get
        {
            Vector2 rNum = transform.position;
            rNum.x = Mathf.Floor(rNum.x / WALL_W);
            rNum.y = Mathf.Floor(rNum.y / WALL_H);
            return rNum;
        }
        set
        {
            Vector2 vec = roomPos;
            Vector2 rn = value;
            rn.x *= WALL_W;
            rn.y *= WALL_H;
            transform.position = roomPos + rn;
        }
    }

    //对坐标取整
    public Vector2 GetRoomPosOnGrid(float mult = -1) 
    {
        if (mult == -1) 
        {
            //以gridMult（0.5）的范围取整
            mult = gridMult;
        }
        Vector2 rPos = roomPos;
        //rPos / mult = 
        rPos /= mult;
        //Mathf.Round - 四舍五入，前后有偶数先取偶数
        rPos.x = Mathf.Round(rPos.x);
        rPos.y = Mathf.Round(rPos.y);
        //rPos * mult = 
        rPos *= mult;        
        return rPos;
    }

}
