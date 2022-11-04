using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour
{
    public Transform GridPos;
    public Transform RoomPos;
    public KeepInRoom kimGridMove;


    private IFacingMover mover;

    void Awake()
    {
        mover = GetComponent<IFacingMover>();
        kimGridMove = GetComponent<KeepInRoom>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //oldMovePos.localPosition = mover.roomPos;
        if (!mover.moving) 
        {
            return;
        }

        Vector2 rPos = mover.roomPos;
        Vector2 rPosGrid = mover.GetRoomPosOnGrid();
        //Debug.Log("roomGridPos: " + rPosGrid + "roomPos: " + rPos + "Multgrid: " + kimGridMove.gridMult);

        float delta;
        int facing = mover.GetFacing();

        //调整主角上下移动
        if (facing == 0 || facing == 2)
        {
            delta= rPosGrid.y- rPos.y;
            //Debug.Log("调整主角上下移动" + "rPosGrid: " + rPosGrid + "rPos: " + rPos + "delta: " + delta);
        }
        //调整主角左右移动
        else 
        {
            delta= rPosGrid.x- rPos.x;

            //Debug.Log("调整主角左右移动" +"rPosGrid: "+ rPosGrid +"rPos: "+rPos+"delta: "+ delta);
        }

        if (delta == 0) 
        {
            return;
        }

        float moveDelta = mover.GetSpeed() * Time.fixedDeltaTime;
        //所以这需要使用绝对值
        //另外Speed*Time.fixedDeltaTime应该是让运动更加平滑
        moveDelta = Mathf.Min(moveDelta, Mathf.Abs(delta));
        //moveDelta = Mathf.Abs(delta);

        //Debug.Log("moveDelta" + moveDelta);
        if (delta<0) 
        {
            moveDelta = -moveDelta;
        }

        if (facing == 0 || facing == 2)
        {
            rPos.y += moveDelta;
        }
        else 
        {
            rPos.x += moveDelta;
        }
        mover.roomPos = rPos;
        if (GridPos != null&& RoomPos != null) 
        {
            GridPos.localPosition = Utils.multipyRoomNum(mover.GetRoomPosOnGrid(), mover.roomNum);
            RoomPos.localPosition = Utils.multipyRoomNum(mover.roomPos, mover.roomNum);
        }               
    }
}
