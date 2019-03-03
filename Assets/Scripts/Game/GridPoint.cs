﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    public GridIndex index;
    public GridSate gridState;
    private Sprite gridSprite;//格子图片
    private Sprite pathSprite;//自动寻路点

    public GameObject currentItem;//当前格子持有道具
    public GameObject currentTower;//当前格子持有的炮塔

    //格子索引
    public struct GridIndex
    {
        public int x;
        public int y;
    }
    //格子状态
    public struct GridSate
    {
        public bool isPathPoint;//是路径点
        public bool isTowerPoint;//建塔点
        public bool hasItem;//道具 
        public bool hasTower;//塔
        public int itemID;//持有的道具序号
        public int towerID;//持有的塔序号
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gridSprite = MapMaker.Instance.gridSprite;
        pathSprite = MapMaker.Instance.pathSprite;
        InitGrid();
    }

    void InitGrid()
    {
        gridState.isTowerPoint = true;
        gridState.isPathPoint = false;
        spriteRenderer.enabled = true;
        gridState.hasItem = false;
        gridState.hasTower = false;
        gridState.itemID = -1;
        gridState.towerID = -1;
    }

    void SetPathPoint()
    {
        gridState.isPathPoint = !gridState.isPathPoint;
        if (gridState.isPathPoint)
        {
            spriteRenderer.sprite = pathSprite;
        }
        else
        {
            spriteRenderer.sprite = gridSprite;
            gridState.isTowerPoint = true;
        }
    }

    void SetItem()
    {
        if (currentItem == null)
        {

        }
        else
        {
            Destroy(currentItem);

        }
        gridState.hasItem = true;
    }

    private void OnMouseDown()
    {
        //寻路点的设置
        if (Input.GetKey(KeyCode.P))
        {
            SetPathPoint();
        }

        else if(Input.GetKey(KeyCode.I))
        {
            SetItem();
        }
        else if(Input.GetKey(KeyCode.T))
        {

        }

        //非寻路点，
        else if(!gridState.isPathPoint)
        {
            gridState.isPathPoint = false;
            gridState.isTowerPoint = !gridState.isTowerPoint;
            if(gridState.isTowerPoint)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
    }

}
