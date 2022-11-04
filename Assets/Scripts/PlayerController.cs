using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float playerHeight;
    public Rigidbody2D playerRig;
    public LayerMask layerMask;
    public ContactFilter2D contactFilter;

    public int hitCounts;

    public RaycastHit2D hit;
    public RaycastHit2D[] hits = new RaycastHit2D[20];

    public bool isGround,isMove,isRuning,isJumping;


    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float inputX, inputY;

        //inputX = Input.GetAxis("Horizontal");
        //inputY = Input.GetAxis("Vertical");

        //playerRig.velocity = new Vector2(inputX * Speed*Time.deltaTime, inputY * Speed*Time.deltaTime);

        
        hitCounts = Physics2D.Raycast(transform.position, Vector2.down,contactFilter,hits,playerHeight);
        hit = Physics2D.Raycast(transform.position, Vector2.down, playerHeight, layerMask);

        GameObject hitGo = hit.collider.gameObject;

        if (hitGo!=null) 
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        if (hitCounts>0) 
        {
            for (int i = 0; i < hitCounts; i++)
            {
                Debug.Log(hits[i].collider.gameObject);
            }
        }        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject go = collision.gameObject;
    //    Debug.Log(go.name);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector2.down*playerHeight);
        Gizmos.color = Color.red;
    }
}
