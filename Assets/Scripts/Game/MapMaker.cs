using Assets.Framework;
using Assets.Framework.Extension;
using Assets.Framework.Factory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一部分功能是用于配置 levelData数据的 最后用宏处理去掉
/// </summary>
public class MapMaker : MonoBehaviour
{
    
    public GameObject gridGo;//格子资源
    
    private float mapWidth;
    private float mapHeight;
    private float gridWidth;
    private float gridHeight;
    private const int yRow = 9;
    private const int xColumn = 12;

    public int levelID=-1;//当前关卡数 
    

    //路径点的X，Y
    //private GridPoint[,] gridPoints;
    public GridPoint[] allGrid;

    private SpriteRenderer bgSR;//获取背景
    private SpriteRenderer roadSR;//获取地图


    LevelMapDataMgr lvMapMgr;
    private void Awake()
    {
#if Tool
        _instance = this;
        mapDataList = new List<LevelMapData>();
#endif
        lvMapMgr = LevelMapDataMgr.Instance;
        InitAllGrid();//初始化所有的格子
        bgSR = transform.Find("BG").GetComponent<SpriteRenderer>();//背景待定
        roadSR = transform.Find("Road").GetComponent<SpriteRenderer>();
        LoadLevelMap(GameRoot.Instance.pickLevel);
    }
    public void LoadLevelMap(int index)
    {
        //bgSR.sprite = FactoryManager.Instance.GetSprite(LevelInfoMgr.Instance.levelInfoList[index].mapPath);待定
        //roadSR.sprite= FactoryManager.Instance.GetSprite(LevelInfoMgr.Instance.levelInfoList[index].mapPath);TODO
        //加载地图，加载建塔格子，加载怪物
        int count = lvMapMgr.leveMapDataList[index].gridStateList.Count;
        for (int i = 0; i <count;i++)
        {
            GridPoint.GridState state = lvMapMgr.leveMapDataList[index].gridStateList[i];
            allGrid[state.id].gridState = state;
            allGrid[state.id].UpdateGrid();
        }
    }
    //初始化地图中所有的格子
    public void InitAllGrid()
    {
        CalculateSize();
        //--------
        //gridPoints = new GridPoint[xColumn, yRow];
        //---------

        allGrid = new GridPoint[xColumn * yRow];
        int num = 0;
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                GameObject grid = Instantiate(gridGo, transform.position, transform.rotation);
                grid.transform.position = CorretPostion(x * gridWidth, y * gridHeight);
                grid.transform.SetParent(transform);
                GridPoint gridPoint=grid.GetComponent<GridPoint>();
                //------
                //gridPoint.index.x = x;
                //gridPoint.index.y = y;
                //gridPoints[x, y] = gridPoint;
                //--------
                gridPoint.gridState.id = num;
                allGrid[num] = gridPoint;
                num++;
                grid.Hide();
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



#if Tool
    public int towerID = -1;//摆放的炮塔ID
    //资源
    public Sprite gridSprite;
    public Sprite buildSprite;
    public Sprite towerSprite;

    private static MapMaker _instance;
    public static MapMaker Instance { get { return _instance; } }

    public bool drawLine;//是否画线辅助

    public List<LevelMapData> mapDataList;
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

#endif
}
