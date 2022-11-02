using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Vector3[] dirs = new Vector3[] { Vector3.right, Vector3.up, Vector3.left, Vector3.down };

    [Header("Set in Inspector:Enemy")]
    public float maxHealth = 1;

    [Header("Set in Dymically")]
    public float health;

    protected Animator anim;
    protected Rigidbody rigb;
    protected SpriteRenderer sr;

    protected virtual void Awake()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rigb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }    
}
