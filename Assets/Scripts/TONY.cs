using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tMode 
{
    idle,
    move,
    attack,
    transition
}

public class Tony : MonoBehaviour,IFacingMover,IKeyMaster
{
    [Header("Set In Inspector")]
    public float Speed;
    public Rigidbody rig;
    public Animator animator;
    public CameraFollowTony cameraScript;

    [Header("Attack Variable")]
    public float attackDuration; //攻击持续时间
    public float startAttackTime; 
    public float nextAttackTime; //攻击间隔    
    public float knockbackSpeed = 10;
    public float knockbackDuration = 0.25f;
    public float invincibleDuration = 0.5f;
    public bool invincible = false;


    [Header("Move Variable")]
    public int dirNum = -1;
    public int facing = 0;
    public tMode mode = tMode.idle;
    public bool isWalk;
    public AnimatorStateInfo stateInfo;
    public float transitionDelay = 0.5f; //房间转换间隔   

    [Header("Game Variable")]
    public int keyCount;
    public int maxHealth = 10;


    [SerializeField]
    private int _health;

    private float knockbackDone = 0;
    private float invincibleDone = 0;
    private Vector3 knockVel;

    private SpriteRenderer sRender;

    public int health 
    {    
        get { return _health; } 
        set { _health = value; } 
    }

    
    private float attackDone;
    private KeepInRoom inRm;
    private KeyCode[] KeyCodes = new KeyCode[] { KeyCode.D,KeyCode.W,KeyCode.A,KeyCode.S};
    private Vector3[] dirs = new Vector3[] { Vector3.right, Vector3.up, Vector3.left, Vector3.down };
    private float transitionDone = 0;
    private Vector2 transitionPos;

    //--------------------------------------------[[[[[[[From IKeyMaster]]]]]]]-----------------------------------
    public int KeyCount { get => keyCount; set => keyCount = value; }

    //--------------------------------------------[[[[[[[From IFacingMover]]]]]]]--------------------------------
    public Vector2 roomPos { get => inRm.roomPos; set => inRm.roomPos = value; }    
    public Vector2 roomNum { get => inRm.roomNum; set => inRm.roomNum = value; }
    public float GetSpeed()
    {
        return Speed;
    }
    public int GetFacing()
    {
        return facing;
    }
    public float gridMult
    {
        get
        {
            return inRm.gridMult;
        }
    }
    public Vector2 GetRoomPosOnGrid(float mult = -1)
    {
        return inRm.GetRoomPosOnGrid(mult);
    }
    public bool moving
    {
        get { return mode == tMode.move; }
    }



    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inRm = GetComponent<KeepInRoom>();
        cameraScript = Camera.main.GetComponent<CameraFollowTony>();
        health = maxHealth;
        sRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible&&Time.time>invincibleDone) 
        {
            invincible = false;
        }
        if (invincible)
        {
            sRender.color = Color.red;
        }
        else 
        {
            sRender.color = Color.white;
        }


        if (mode == tMode.transition)
        {
            rig.velocity = Vector3.zero;
            animator.speed = 0;
            if (Time.time<transitionDone||!cameraScript.MoveComplete) 
            {
                return;
            }
            if (cameraScript.MoveComplete) 
            {
                mode = tMode.idle;
                cameraScript.MoveComplete = false;
            }
        }

        dirNum = -1;
        for (int i = 0; i < KeyCodes.Length; i++)
        {
            if (Input.GetKey(KeyCodes[i]) && mode != tMode.attack)
            {
                dirNum = i;
                facing = i;
                animator.SetInteger("Facing", facing);
                mode = tMode.move;                
            }
        }
        if (Input.GetKeyDown(KeyCode.J)&&Time.time>attackDone)
        {
            attackDone = Time.time + nextAttackTime;
            startAttackTime = Time.time;
            mode = tMode.attack;
        }
        Vector3 dir;

        if (dirNum == -1 && mode != tMode.attack&&mode!=tMode.transition)
        {
            mode = tMode.idle;
        }

        switch (mode)
        {
            case tMode.idle:
                animator.SetBool("Walk", false);
                animator.speed = 0;
                rig.velocity = Vector3.zero;
                break;

            case tMode.move:

                dir = dirs[dirNum];
                animator.SetBool("Walk", true);
                animator.speed = 1;
                rig.velocity = dir * Speed;
                break;

            case tMode.attack:
                animator.SetBool("Attack",true);
                animator.speed = 1;
                if (Time.time - startAttackTime>attackDuration) 
                {
                    animator.SetBool("Attack", false);
                    if (dirNum == -1)
                    {
                        mode = tMode.idle;
                    }
                    else 
                    {
                        mode = tMode.move;
                    }
                }              
                break;
        }
    }

    void LateUpdate() 
    {
        Vector2 rPos = GetRoomPosOnGrid(0.5f);

        int rNum;
        for (rNum = 0; rNum < KeepInRoom.DOORSPOS.Length; rNum++)
        {
            if (rPos == KeepInRoom.DOORSPOS[rNum]) 
            {
                break;
            }
        }

        if (rNum!=facing||rNum>3) 
        {
            //说明循环没有被break;
            return;
        }

        //房间移动
        Vector2 rm = roomNum;
        switch (rNum) 
        {
            case 0:
                rm.x += 1;
                break;
            case 1:
                rm.y += 1;
                break;
            case 2:
                rm.x -= 1;
                break;
            case 3:
                rm.y -= 1;
                break;            
        }
        //确认有效移动
        if (rm.x >= 0 && rm.x <= KeepInRoom.MAX_RM_X)
        {
            if (rm.y >= 0 && rm.y <= KeepInRoom.MAX_RM_Y)
            {
                roomNum = rm;
                transitionPos = KeepInRoom.DOORSPOS[(rNum + 2) % 4];
                roomPos = transitionPos;
                mode = tMode.transition;
                cameraScript.Transitioning = true;
                transitionDone = Time.time + transitionDelay;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (invincible)
        {
            return;
        }
        DamageEffect dEff = col.gameObject.GetComponent<DamageEffect>();
        if (dEff == null)
        {
            return;
        }

        health -= dEff.damage;
        invincible = true;
        invincibleDone = Time.time + invincibleDuration;
    }
}
