using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    public int x;
    public int y;
    public int TileNum;
    SpriteRenderer spriteRenderer;

    public bool instantiateTile;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instantiateTile) 
        {
            Tile ti = TileCamera.TILES[x, y];
            TileNum = TileCamera.GET_MAP(x, y);
            spriteRenderer.sprite = TileCamera.SPRITES[TileNum];
            instantiateTile = false;
        }
    }
}
