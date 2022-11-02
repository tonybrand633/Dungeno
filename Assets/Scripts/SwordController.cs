using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public TONY tony;
    public GameObject sword;
    public SpriteRenderer swordRenderer;
    public Vector3[] swordPos;

    // Start is called before the first frame update
    void Start()
    {
        swordPos = new Vector3[4];
        tony = GetComponentInParent<TONY>();
        sword = transform.Find("Sword").gameObject;
        swordRenderer = sword.GetComponent<SpriteRenderer>();
        sword.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        swordPos[0] = GameObject.Find("Right").transform.position;
        swordPos[1] = GameObject.Find("Up").transform.position;
        swordPos[2] = GameObject.Find("Left").transform.position;
        swordPos[3] = GameObject.Find("Down").transform.position;
        //transform.rotation = Quaternion.Euler(0, 0, 90 * tony.facing);

        switch (tony.facing) 
        {
            case 0:
                sword.transform.position = swordPos[0];
                sword.transform.rotation = Quaternion.Euler(0, 0, 0);
                swordRenderer.flipY = false;
                swordRenderer.sortingOrder = 2;
                break;
            case 1:
                sword.transform.position = swordPos[1];
                sword.transform.rotation = Quaternion.Euler(0, 0, 90);
                swordRenderer.flipY = false;
                swordRenderer.sortingOrder = 0;
                break;
            case 2:
                sword.transform.position = swordPos[2];
                sword.transform.rotation = Quaternion.Euler(0, 0, 180);
                swordRenderer.flipY = true;
                swordRenderer.sortingOrder = 2;
                break;
            case 3:
                sword.transform.position = swordPos[3];
                sword.transform.rotation = Quaternion.Euler(0, 0, 270);
                swordRenderer.flipY = true;
                swordRenderer.sortingOrder = 2;
                break;
        }


        sword.SetActive(tony.tonyState == TonyState.attack);        
    }
}
