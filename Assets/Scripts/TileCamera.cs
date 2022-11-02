using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCamera : MonoBehaviour
{
    static private int W, H;
    static private int[,] MAP;
    static public Sprite[] SPRITES;
    static public Transform TileAnchor;
    static public Tile[,] TILES;
    static public string CollisionText;

    [Header("Set In Inspector")]
    public TextAsset mapInfos;
    public Texture2D mapPNG;
    public Tile tilePrefab;
    public TextAsset mapCollision;



    void Awake()
    {
        CollisionText = Utils.RemoveLineEndings(mapCollision.text);

        Debug.Log(CollisionText.Length);
        LoadMap();    
    }

    public void LoadMap() 
    {
        GameObject anchor = new GameObject("anchor");
        TileAnchor = anchor.transform;

        SPRITES = Resources.LoadAll<Sprite>(mapPNG.name);

        string[] lines = mapInfos.text.Split('\n');
        H = lines.Length;
        string[] tileNums = lines[0].Split(' ');
        W = tileNums.Length;

        System.Globalization.NumberStyles hexNumber;
        hexNumber = System.Globalization.NumberStyles.HexNumber;

        MAP = new int[W, H];

        for (int i = 0; i < H; i++)
        {
            tileNums = lines[i].Split(' ');
            for (int j = 0; j < W; j++)
            {
                if (tileNums[j] == "..")
                {
                    MAP[j, i] = 0;
                }
                else 
                {
                    //��һ������ʮ������ת��Ϊ16���Ƶ����֣��Ӷ���ӦSPRITES���sprite�ı��
                    int mapValue = int.Parse(tileNums[j], hexNumber);
                    //Debug.Log(mapValue);
                    MAP[j, i] = mapValue;
                }
            }
        }

        //Debug.Log("Parse :" + SPRITES.Length + " sprites");
        //Debug.Log("MapSize: W:" + W + " H:" + H);

        ShowMap();
    }

    public static int GET_MAP(int x,int y) 
    {
        if (x<0||x>=W||y<0||y>=H) 
        {
            return -1;
        }
        return MAP[x, y];
    }

    public static int GET_MAP(float x,float y) 
    {
        int ex = Mathf.RoundToInt(x);
        //�����y-0.25f�����������Ϸǿ��͸�ӵ�ԭ�������ͼ������ʾ
        //���ǵ��ϰ���,�����ڸ�ͼ������Ȼ���ڿ���״̬.        
        int ey = Mathf.RoundToInt(y-0.25f); 
        if (ex < 0 || ex >= W || ey < 0 || ey >= H)
        {
            return -1;
        }
        return MAP[ex, ey];
    }

    public static void Set_MAP(int x,int y,int tileNum) 
    {
        if (x < 0 || x >= W || y < 0 || y >= H)
        {
            return;
        }
        MAP[x, y] = tileNum;
    }

    void ShowMap() 
    {
        TILES = new Tile[W, H];
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (MAP[j,i]!=0) 
                {
                    
                    Tile ti = Instantiate<Tile>(tilePrefab);
                    ti.transform.SetParent(TileAnchor);
                    ti.SetTile(j, i);
                    TILES[j, i] = ti;
                }                
            }
        }
    }
}
