using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Set Dymically")]
    public int x;
    public int y;
    public int tileNUM;
    BoxCollider COL;


    void Awake()
    {
        COL = GetComponent<BoxCollider>();    
    }

    public void SetTile(int eX,int eY,int tileNum = -1) 
    {
        x = eX;
        y = eY;

        transform.localPosition = new Vector3(x, y, 0);
        gameObject.name = x.ToString("D3") + "x" + y.ToString("D3");

        if (tileNum == -1) 
        {
            tileNUM = TileCamera.GET_MAP(x, y);
        }
        GetComponent<SpriteRenderer>().sprite = TileCamera.SPRITES[tileNUM];
        SetCollider();
    }

    void SetCollider() 
    {
        char c = TileCamera.CollisionText[tileNUM];
        COL.enabled = true;

        switch (c) 
        {
            //¶¥²¿Åö×²
            case 'W':
                COL.center = new Vector3(0, 0.25f, 0);
                COL.size = new Vector3(1, 0.5f, 1);
                break;
            //ÍêÈ«Åö×²
            case 'S':
                COL.center = Vector3.zero;
                COL.size = Vector3.one;
                break;
            //×óÅö×²
            case 'A':
                COL.center = new Vector3(-0.25f, 0, 0);
                COL.size = new Vector3(0.5f, 1, 1);
                break;
            //ÓÒÅö×²
            case 'D':
                COL.center = new Vector3(0.25f, 0, 0);
                COL.size = new Vector3(0.5f, 1, 1);
                break;
            default:
                COL.enabled = false;
                break;
        }
    }
}
