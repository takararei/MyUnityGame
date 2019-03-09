using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    private static MapMaker _instance;
    public static MapMaker Instance { get { return _instance; } }

    public bool drawLine;//是否画线辅助
    public GameObject gridGo;//格子资源
    
    public float mapWidth;
    private float mapHeight;
    [HideInInspector]
    public float gridWidth;
    [HideInInspector]
    public float gridHeight;
    private const int yRow = 9;
    private const int xColumn = 12;

    public int levelID;//当前关卡数 
    public int difficulty;//当前难度

    public int itemID;//摆放的道具ID
    public int itemSize;
    public int towerID;//摆放的炮塔ID

    //路径点的X，Y
    private GridPoint[,] gridPoints;

    private SpriteRenderer bgSR;//获取背景
    private SpriteRenderer roadSR;//获取地图
    
    //资源
    public Sprite gridSprite;
    public Sprite pathSprite;
    public GameObject[] itemPrefabs;//道具数组
    public GameObject[] towerPrefabs;//建塔数组
    
    private void Awake()
    {
        _instance = this;
        InitAllGrid();//初始化所有的格子
        //获取所有的道具
        //获取所有的塔
        bgSR = transform.Find("BG").GetComponent<SpriteRenderer>();
        roadSR = transform.Find("Road").GetComponent<SpriteRenderer>();


    }

    //初始化地图中所有的格子
    public void InitAllGrid()
    {
        CalculateSize();
        gridPoints = new GridPoint[xColumn, yRow];
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                GameObject grid = Instantiate(gridGo, transform.position, transform.rotation);
                grid.transform.position = CorretPostion(x * gridWidth, y * gridHeight);
                grid.transform.SetParent(transform);
                GridPoint gridPoint=grid.GetComponent<GridPoint>();
                gridPoint.index.x = x;
                gridPoint.index.y = y;

                gridPoints[x, y] = gridPoint;
            }
        }
    }

    //纠正格子位置
    public Vector3 CorretPostion(float x, float y)
    {
        return new Vector3(x - mapWidth / 2 + gridWidth / 2, y - mapHeight / 2 + gridHeight / 2);
    }

    //计算格子和地图的大小
    private void CalculateSize()
    {
        //取屏幕对角线两点，左下 右上，计算地图长宽
        Vector3 leftDown = new Vector3(0, 0);
        Vector3 rightUp = new Vector3(1, 1);
        Vector3 posOne = Camera.main.ViewportToWorldPoint(leftDown);
        Vector3 posTwo = Camera.main.ViewportToWorldPoint(rightUp);

        mapWidth = posTwo.x - posOne.x;
        mapHeight = posTwo.y - posOne.y;

        gridWidth = mapWidth / xColumn;
        gridHeight = mapHeight / yRow;
    }

    //画出蓝线格子
    private void OnDrawGizmos()
    {
        if (drawLine)
        {
            CalculateSize();
            Gizmos.color = Color.blue;

            for (int y = 0; y <= yRow; y++)
            {
                Vector3 startPos = new Vector3(-mapWidth / 2, -mapHeight / 2 + y * gridHeight);
                Vector3 endPos = new Vector3(mapWidth / 2, -mapHeight / 2 + y * gridHeight);
                Gizmos.DrawLine(startPos, endPos);
            }

            for(int x=0;x<=xColumn;x++)
            {
                Vector3 startPos = new Vector3(-mapWidth / 2 + gridWidth * x, mapHeight / 2);
                Vector3 endPos = new Vector3(-mapWidth / 2 + x * gridWidth, -mapHeight / 2);
                Gizmos.DrawLine(startPos, endPos);
            }
        }
    }




}
