using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletos : Enemy, IFacingMover
{
    [Header("Set From Skeletos")]
    public int health;
    public int thinkMin;
    public int thinkMax;
    public int facing;
    public float thinkOverTime;
    public Vector2 dir;
    public float speed;
    public int duplicateCount;

    private KeepInRoom iRm;
    private int facingNow;

    public bool moving 
    {
        get 
        {
            return true;
        }   
    }

    public float gridMult 
    {
        get 
        {
            return iRm.gridMult;
        }
    }

    public Vector2 roomPos
    {
        get 
        {
            return iRm.roomPos;
        }
        set 
        {
            iRm.roomPos = value; 
        }
    }
    public Vector2 roomNum 
    { 
        get => iRm.roomNum;

        set => iRm.roomNum = value;     
    }

    protected override void Awake()
    {
        iRm = GetComponent<KeepInRoom>();
        base.Awake();
    }
    void Update()
    {
        if (Time.time>thinkOverTime) 
        {   
            ChangeDir();
        }
        dir = dirs[facing];
        rigenemy.velocity = dir * speed;
    }

    void ChangeDir() 
    {
        if (duplicateCount>=2) 
        {
            duplicateCount = 0;
            return;
        }
        facingNow = facing;
        thinkOverTime = Random.Range(thinkMin, thinkMax) + Time.time;

        facing = Random.Range(0, dirs.Length);

        if (facingNow == facing) 
        {
            duplicateCount++;
        }
    }

    public int GetFacing()
    {
        return facing;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public Vector2 GetRoomPosOnGrid(float mult = -1)
    {
        return iRm.GetRoomPosOnGrid(mult);
    }
}
