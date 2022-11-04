using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper : MonoBehaviour
{
    //使用const用于switch...case语句

    //锁住的门
    const int LockedR = 73;
    const int LockedUR = 57;
    const int LockedUL = 56;
    const int LockedL = 72;
    const int LockedDR = 89;
    const int LockedDL = 88;

    //打开的门
    const int OpenR = 70;
    const int OpenUR = 53;
    const int OpenUL = 52;
    const int OpenL = 67;
    const int OpenDR = 85;
    const int OpenDL = 84;


    IKeyMaster keyMaster;

    void Awake()
    {
        keyMaster = GetComponent<IKeyMaster>();    
    }

    void OnCollisionEnter(Collision collision)
    {
        Tile t = collision.gameObject.GetComponent<Tile>();
        int keyCount = keyMaster.KeyCount;
        if (keyCount<=0||t==null) 
        {
            return;
        }

        int tileNum = t.tileNum;
        
        Vector2 tilePos = t.transform.localPosition;
        Tile t2;
        //Debug.Log(tilePos);
        //Debug.Log(t.tileNum);
        switch (tileNum) 
        {            
            case LockedR:
                t.SetTile( (int)tilePos.x, (int)tilePos.y, OpenR);
                break;
            case LockedL:
                t.SetTile((int)tilePos.x, (int)tilePos.y, OpenL);
                break;
            case LockedUR:
                t.SetTile((int)tilePos.x, (int)tilePos.y, OpenUR);
                t2 = TileCamera.Tiles[(int)tilePos.x - 1, (int)tilePos.y];
                Debug.Log(t2.name);
                t2.SetTile((int)tilePos.x - 1, (int)tilePos.y, OpenUL);
                break;
            case LockedUL:
                t.SetTile((int)tilePos.x, (int)tilePos.y, OpenUL);
                t2 = TileCamera.Tiles[(int)tilePos.x + 1, (int)tilePos.y];
                t2.SetTile((int)tilePos.x + 1, (int)tilePos.y, OpenUR);
                break;
            case LockedDR:
                t.SetTile((int)tilePos.x, (int)tilePos.y, OpenDR);
                t2 = TileCamera.Tiles[(int)tilePos.x - 1, (int)tilePos.y];
                t2.SetTile((int)tilePos.x - 1, (int)tilePos.y, OpenDL);
                break;
            case LockedDL:
                t.SetTile((int)tilePos.x, (int)tilePos.y, OpenDL) ;
                t2 = TileCamera.Tiles[(int)tilePos.x + 1, (int)tilePos.y];
                t2.SetTile((int)tilePos.x + 1, (int)tilePos.y, OpenDR); ;
                break;
            default:
                return;
        }
        keyMaster.KeyCount--;
        Debug.Log(t.tileNum);
    }
}
