using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Tony tony;
    public GameObject sword;    
    public Transform Right;
    public Transform Up;
    public Transform Left;
    public Transform Down;

    private SpriteRenderer swordSR;

    void Start()
    {
        tony = GetComponentInParent<Tony>();
        sword = transform.Find("Sword").gameObject;
        Right = transform.Find("Right");
        Up = transform.Find("Up");
        Left = transform.Find("Left");
        Down = transform.Find("Down");
        swordSR = sword.GetComponent<SpriteRenderer>();
        sword.SetActive(false);
    }

    void Update()
    {
        sword.SetActive(tony.mode == tMode.attack);
        switch (tony.facing) 
        {
            case 0:
                swordSR.sortingOrder = 2;
                sword.transform.rotation = Quaternion.Euler(0, 0, tony.facing * 90);
                sword.transform.localPosition = Right.localPosition;
                //swordSR.flipX = false;
                break;
            case 1:
                swordSR.sortingOrder = 0;
                sword.transform.rotation = Quaternion.Euler(0, 0, tony.facing * 90);
                sword.transform.localPosition = Up.localPosition;
                break;
            case 2:
                swordSR.sortingOrder = 2;
                sword.transform.rotation = Quaternion.Euler(0, 0, tony.facing * 90);
                sword.transform.localPosition = Left.localPosition;
                //swordSR.flipX = true;
                break;
            case 3:
                swordSR.sortingOrder = 2;
                sword.transform.rotation = Quaternion.Euler(0, 0, tony.facing * 90);
                sword.transform.localPosition = Down.localPosition;
                break;
        }

        //transform.rotation = Quaternion.Euler(0, 0, tony.facing*90);
        //T.transform.rotation = Quaternion.Euler(0, 0, tony.facing * 90);
        //Debug.Log(T.transform.position);
    }
}
