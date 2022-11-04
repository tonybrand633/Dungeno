using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("From Scripts Enemy")]
    public int MaxHealth;
    protected static Vector2[] dirs = new Vector2[4] { Vector2.right, Vector2.up, Vector2.down, Vector2.left };
    protected SpriteRenderer enemySR;
    protected Rigidbody rigenemy;
    protected Animator animator;


    protected virtual void Awake()
    {
        enemySR = GetComponent<SpriteRenderer>();
        rigenemy = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); 
    }    
}
