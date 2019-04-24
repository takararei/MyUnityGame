using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
#if Tool
[CustomEditor(typeof(MapMaker))]
public class MapTool : Editor {
    private MapMaker mapMaker;
    //private List<LevelMapData> mapDataList;
    private GridPoint[] allGrid;
    LevelMapData md;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(Application.isPlaying)
        {
            mapMaker = MapMaker.Instance;
            //mapDataList = mapMaker.mapDataList;
            allGrid = mapMaker.allGrid;
            //LevelMapDataMgr levelMgr = Resources.Load<LevelMapDataMgr>("AssetData/LevelMapData");
            ////如果路径存在,先读取文件
            //if (levelMgr != null)
            //{
            //    mapDataList = levelMgr.leveMapDataList;
            //}

            EditorGUILayout.BeginHorizontal();
        
            if(GUILayout.Button("搜集地图数据"))
            {
                md = new LevelMapData();
                md.levelID = mapMaker.levelID;
                md.gridStateList = new List<GridPoint.GridState>();
                foreach (GridPoint gp in allGrid)
                {
                    if(gp.gridState.isTowerPoint)
                    {
                        md.gridStateList.Add(gp.gridState);
                    }
                }
                mapMaker.mapData = md;
            }
            if(GUILayout.Button("保存当前地图"))
            {
                //如果是新关卡 则添加
                LevelMapDataMgr mgrP = Resources.Load<LevelMapDataMgr>("AssetData/LevelMapData");
                if (md.levelID > mgrP.leveMapDataList.Count)
                {
                    //mapDataList.Add(md);
                    mgrP.leveMapDataList.Add(md);
                }
                else
                {
                    mgrP.leveMapDataList[md.levelID - 1] = md;
                }
                LevelMapDataMgr mgr = ScriptableObject.CreateInstance<LevelMapDataMgr>();
                //LevelMapDataMgr mgr = new LevelMapDataMgr();
                mgr.leveMapDataList = new List<LevelMapData>();
                for (int i = 0; i < mgrP.leveMapDataList.Count; i++)
                {
                    mgr.leveMapDataList.Add(mgrP.leveMapDataList[i]);
                }
                UnityEditor.AssetDatabase.CreateAsset(mgr, "Assets/Resources/AssetData/LevelMapData.asset");
                UnityEditor.AssetDatabase.SaveAssets();
                UnityEditor.AssetDatabase.Refresh();
                Debug.Log("保存成功");
            }

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("读取当前关卡"))
            {
                foreach (GridPoint gp in allGrid)
                {
                    gp.InitGrid();
                }

                foreach(GridPoint.GridState gs in LevelMapDataMgr.Instance.leveMapDataList[mapMaker.levelID-1].gridStateList)
                {
                    allGrid[gs.id].gridState = gs;
                    allGrid[gs.id].UpdateGrid();
                }

                mapMaker.roadSR.sprite = Resources.Load<Sprite>("Pictures/Game/Map/Map_" + mapMaker.levelID);
            }
            if(GUILayout.Button("重置操作"))
            {
                foreach(GridPoint gp in allGrid)
                {
                    gp.InitGrid();
                    gp.UpdateGrid();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

}
#endif