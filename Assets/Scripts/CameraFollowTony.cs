using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTony : MonoBehaviour
{
    [SerializeField]
   
    public Vector2 roomPosition = new Vector2(7.5f,5f);
    public Tony tonyScripts;
    public bool MoveComplete;
    public bool Transitioning;
    public float timeDuration;
    public Vector3 pLerp;
    public float u;
    Vector3 OldValuePos;
    float timeStart;
    Vector3 transitionOffset;
    Vector3 currentPos;


    void Awake()
    {
        GameObject T = GameObject.Find("Tony");
        tonyScripts = T.GetComponent<Tony>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OldValuePos = transform.position;
    }

    // Update is called once per frame
    void Update()   
    {
        

        if (tonyScripts.mode == tMode.transition) 
        {
            MoveComplete = false;
            Vector3 rm = tonyScripts.roomNum;
            rm.x *= KeepInRoom.WALL_W;
            rm.y *= KeepInRoom.WALL_H;
            rm.z = -10f;
            OldValuePos.x %= KeepInRoom.WALL_W;
            OldValuePos.y %= KeepInRoom.WALL_H;
            
            //currentPos = transform.position;
            if (Transitioning) 
            {
                transitionOffset = rm + OldValuePos;
                currentPos = transform.position;
                timeStart = Time.time;                
                Transitioning = false;                
            }
            StartTransition(transitionOffset, currentPos);
        }               
    }

    void StartTransition(Vector3 target,Vector3 start) 
    {
        u = (Time.time - timeStart) / timeDuration;
        //Debug.Log("TimeStart: " + timeStart);
        if (u >= 1)
        {
            u = 1;            
            MoveComplete = true;
        }
        if (!MoveComplete) 
        {
            pLerp = u * target + (1 - u) * start;
            //Debug.Log("StartPos: " + start + " TargetPos: " + target);
            transform.position = pLerp;
        }
        
    }
}
