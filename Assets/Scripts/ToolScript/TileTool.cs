using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTool : MonoBehaviour
{
    public int x;
    public int y;
    public int tileNum;

    public bool instantiateTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //x = 2;
        //y = 2;
        //这是地图的信息
        //tileNum = TileCamera.Get_Map(x, y);

        if (instantiateTile) 
        {
            
            this.gameObject.GetComponent<SpriteRenderer>().sprite = TileCamera.SPRITES[tileNum];
            instantiateTile = false;
        }
    }
}
