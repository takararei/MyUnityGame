using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    //public GridIndex index;//--
    public GridState gridState;

    private Sprite gridSprite;//格子图片
    private Sprite buildSprite;//建塔点
    private Sprite towerSprite;//已有塔
    
    public GameObject currentTower;//当前格子持有的炮塔
    //格子状态
    [System.Serializable]
    public struct GridState
    {
        public int id;
        public bool isTowerPoint;//建塔点 空的
        public bool hasTower;//塔 已经有塔了
        public int towerID;//持有的塔序号
    }

    private void Awake()
    {
#if Tool
        gridSprite = MapMaker.Instance.gridSprite;
        buildSprite = MapMaker.Instance.buildSprite;
        towerSprite = MapMaker.Instance.towerSprite;
#endif
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        InitGrid();
    }

    public void InitGrid()
    {
        gridState.isTowerPoint = false;
        spriteRenderer.enabled = true;
        gridState.hasTower = false;
        gridState.towerID = -1;
    }
    
    public void UpdateGrid()
    {
        if(gridState.isTowerPoint)
        {
            if(gridState.hasTower)
            {
                //实例化塔 TODO
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
            
    }

    public void OnMouseDown()
    {
        Debug.Log(gridState.id);
    }

#if Tool
    public void UpdateGrid()
    {
        if (gridState.isTowerPoint)
        {
            if(gridState.hasTower)
            {
                spriteRenderer.sprite = towerSprite;
            }
            else
            {
                spriteRenderer.sprite = buildSprite;
            }
        }
        else
        {
            spriteRenderer.sprite = gridSprite;
        }
    }

    void SetTowerPoint()
    {
        gridState.isTowerPoint = !gridState.isTowerPoint;
        if (gridState.isTowerPoint)
        {
            spriteRenderer.sprite = buildSprite;
        }
        else
        {
            spriteRenderer.sprite = gridSprite;
            gridState.isTowerPoint = false;
        }
    }

    void SetTower()
    {
        gridState.towerID = MapMaker.Instance.towerID;
        
        if (gridState.towerID < 0)
        {
            InitGrid();
            Debug.Log("未设置塔的索引");
            return;
        }
        spriteRenderer.sprite = towerSprite;
        gridState.hasTower = true;
        gridState.isTowerPoint = true;
        Debug.Log(gridState.towerID);
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.P))
        {
            SetTowerPoint();
        }
        else if (Input.GetKey(KeyCode.T))
        {
            SetTower();
        }
        //可以建塔的变图，已有塔的也变图。
        //不可以建塔的就格子
        else if (gridState.isTowerPoint)
        {
            InitGrid();
            spriteRenderer.sprite = gridSprite;
        }
    }
#endif
}
