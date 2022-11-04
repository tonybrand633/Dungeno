using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Dymically Set")]
    public int x;
    public int y;
    public int tileNum;
    public char collisionChar;
    public bool ChangeTile;

    private BoxCollider bColl;


    void Awake()
    {
        bColl = GetComponent<BoxCollider>();        

    }

    void Update()
    {
        if (ChangeTile) 
        {
            ChangeDirectly();
        }
    }

    public void SetTile(int ex,int ey,int etileNum = -1) 
    {
        x = ex;
        y = ey;

        transform.localPosition = new Vector3(x, y, 0);
        gameObject.name = x.ToString("D3") + "x"+y.ToString("D3");

        if (etileNum == -1)
        {
            etileNum = TileCamera.Get_Map(x, y);
        }
        else 
        {
            TileCamera.Set_Map(x, y, etileNum);
        }
        tileNum = etileNum;

        GetComponent<SpriteRenderer>().sprite = TileCamera.SPRITES[tileNum];

        SetCollider();
    }

    void SetCollider() 
    {
        //从DeleverCollision导出碰撞信息
        bColl.enabled = true;

        collisionChar = TileCamera.COLLISIONS[tileNum];
        switch (collisionChar) 
        {
            case 'S'://完全碰撞
                bColl.center = Vector3.zero;
                bColl.size = Vector3.one;
                break;
            case 'W'://顶部碰撞
                bColl.center = new Vector3(0, 0.25f, 0);
                bColl.size = new Vector3(1, 0.5f, 1);
                break;
            case 'A'://左碰撞
                bColl.center = new Vector3(-0.5f, 0, 0);
                bColl.size = new Vector3(0.5f, 1, 1);
                break;
            case 'D'://右碰撞
                bColl.center = new Vector3(0.5f, 0, 0);
                bColl.size = new Vector3(0.5f, 1, 1);
                break;
            default:
                bColl.enabled = false;
                break;                
        }
    }

    void ChangeDirectly() 
    {
        Vector2 curPos = transform.position;
        SetTile((int)curPos.x, (int)curPos.y, tileNum);
        ChangeTile = false;
    }
}
