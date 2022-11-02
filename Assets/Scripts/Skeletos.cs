using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletos : Enemy
{
    [Header("Skeletos")]
    public float speed;
    public float thinkMin=1;
    public float thinkMax=4;    

    public int facing = 0;
    public int facingLastTime;
    public int facingNum = 0;

    private float thinkTime;
    private float thinkOverTime;

    // Start is called before the first frame update
    void Start()
    {
        thinkTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (thinkTime<Time.time) 
        {
            ChangeDirection();
        }
        rigb.velocity = speed * dirs[facing];
    }

    void ChangeDirection() 
    {
        if (facingNum>3) 
        {
            facingNum = 0;
            return;
        }

        thinkTime = Random.Range(thinkMin, thinkMax) + Time.time;
        facingLastTime = facing;
        facing = Random.Range(0, dirs.Length);

        if (facingLastTime == facing)
        {
            facingNum++;
        }
        else 
        {
            facingNum = 0;
        }        
    }
}
