using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TonyState
{
    idle,
    walk,
    attack,
    transition
}

public class TONY : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float Speed;
    public Rigidbody rig;
    public Animator anim;
    public float attackDuration;//攻击持续的时间
    public float attckNextTime;//下一次攻击的时间
    public float attackDone; //攻击结束的时间，用于切换状态
    public float attackFinsh;
    


    [Header("Set Dymically")]
    public int dirNum = -1;
    public int facing = 0;
    public TonyState tonyState = TonyState.idle;
    //public AnimatorStateInfo stateInfo;
    private TonyState currentState;
    private Vector3[] dirs = new Vector3[] { Vector3.right, Vector3.up, Vector3.left, Vector3.down };
    private KeyCode[] KeyCodes = new KeyCode[] { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S };
    //private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();    
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        //AnimatorClipInfo[]clipCur;
        //AnimationClip curAnim;
        dirNum = -1;
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(KeyCodes[i]))
            {
                //anim.SetBool("Attack", false);
                dirNum = i;
                facing = i;
                anim.SetInteger("Facing", facing);
                if (tonyState!=TonyState.attack) 
                {
                    tonyState = TonyState.walk;
                }                
            }
        }

        if (dirNum == -1&&tonyState!=TonyState.attack) 
        {            
            tonyState = TonyState.idle;                     
        }

        if (Input.GetKeyDown(KeyCode.J)&&Time.time>attackDone) 
        {
            attackDone = Time.time + attckNextTime;
            print(attackDone);
            attackFinsh = Time.time + attackDuration;
            anim.SetBool("Attack", true);
            tonyState = TonyState.attack;
        }
              
        switch (tonyState) 
        {
            case TonyState.idle:
                rig.velocity = Vector3.zero;
                anim.speed = 0;
                break;
            case TonyState.walk:
                Vector3 dir = Vector3.zero;
                dir = dirs[dirNum];
                rig.velocity = dir * Speed;
                anim.SetBool("Walk", true);                
                //anim.CrossFade("Tony_Walk_" + facing, 0);
                anim.speed = 1;
                break;
            case TonyState.attack:
                
                if (Time.time > attackFinsh)
                {
                    anim.SetBool("Attack", false);  
                    if (dirNum == -1)
                    {
                        tonyState = TonyState.idle;
                    }
                    else 
                    {
                        tonyState = TonyState.walk;
                    }
                }                
                break;
        }
    }
}
