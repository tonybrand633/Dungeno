using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCamera : MonoBehaviour
{
    static private int W, H;
    static private int[,] MAP_Info;

    static public Transform tileAnchor;
    static public Sprite[] SPRITES;
    static public Tile[,] Tiles;
    static public string COLLISIONS;

    [Header("Set In Inspector")]
    public TextAsset mapInfos;
    public Texture2D mapSpritePNG;
    public TextAsset mapCollision;
    public Tile TilePrefab;
    



    void Awake()
    {
        COLLISIONS = Utils.RemoveLineEndings(mapCollision.text);
        //COLLISIONS is ___WWWWWWWWWWWWWSSSS_SSS_SS__S_S_SSSSSSS_SSSSSS__SSSADSSSSSSADS_SSS_AD_SSSSS__SSSSSSADSSSSSSADSS_S____________S_SS____________SS_S____________S_SS____________SSSSSSSSSSSSSSSSSSSSS__SSS_SS__S_S________________________________________________________________
        //int i = Utils.GetStringCount(COLLISIONS);
        //Debug.Log(i);

        Load_Map();
    }

    public void Load_Map() 
    {
        string[] lines = mapInfos.text.Split('\n');
        //66
        H = lines.Length;

        string[] tiles = lines[0].Split(' ');
        //96
        W = tiles.Length;

        GameObject ANCHOR = new GameObject("TileAnchor");
        tileAnchor = ANCHOR.transform;

        SPRITES = Resources.LoadAll<Sprite>(mapSpritePNG.name);
        System.Globalization.NumberStyles hexNumber;
        hexNumber = System.Globalization.NumberStyles.HexNumber;

        MAP_Info = new int[W, H];

        for (int i = 0; i < H; i++)
        {
            string[] curLine = lines[i].Split(' ');
            for (int j = 0; j < W; j++)
            {
                string tileInfo = curLine[j];
                //Debug.Log("MapInfo: "+ tileInfo);
                if (tileInfo!="..") 
                {
                    //b0 -> 176
                    int mapInfo = int.Parse(tileInfo, hexNumber);
                    //Debug.Log("TileNum"+mapInfo);  
                    MAP_Info[j, i] = mapInfo;
                }                                
            }
        }

        //Debug.Log("InitMap W: " + W + "H: " + H);
        //Debug.Log("InitSprites:" + SPRITES.Length);

        ShowMap();
    }

    static public int Get_Map(int x,int y) 
    {
        if (x<0||x>=W||y<0||y>=H) 
        {
            return -1;
        }
        int i = MAP_Info[x, y];
        return MAP_Info[x, y];
    }

    static public int Get_Map(float x,float y) 
    {
        if (x < 0 || x >= W || y < 0 || y >= H)
        {
            return -1;
        }
        int eX = Mathf.RoundToInt(x);
        int eY = Mathf.RoundToInt(y-0.25f);
        return MAP_Info[eX, eY];
    }

    static public void Set_Map(int x,int y,int tNum = -1) 
    {
        if (x < 0 || x >= W || y < 0 || y >= H)
        {
            return;
        }
        //存储整个地图的信息
        MAP_Info[x, y] = tNum;
    }

    void ShowMap() 
    {
        Tiles = new Tile[W, H];
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (MAP_Info[j,i]!=0) 
                {
                    Tile t = Instantiate<Tile>(TilePrefab);
                    //transform.SetParent比transform.parent效率要高
                    t.transform.SetParent(tileAnchor);
                    t.SetTile(j, i);
                    Tiles[j, i] = t;
                }              
            }
        }
    }

}
