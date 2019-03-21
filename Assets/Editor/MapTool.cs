using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MapMaker))]
public class MapTool : Editor {
    private MapMaker mapMaker;
    private List<LevelMapData> mapDataList;
    private GridPoint[] allGrid;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(Application.isPlaying)
        {
            mapMaker = MapMaker.Instance;
            mapDataList = mapMaker.mapDataList;
            allGrid = mapMaker.allGrid;
            LevelMapDataMgr levelMgr = Resources.Load<LevelMapDataMgr>("AssetData/LevelMapData");
            //如果路径存在,先读取文件
            if (levelMgr != null)
            {
                mapDataList = levelMgr.leveMapDataList;
            }

            EditorGUILayout.BeginHorizontal();
        
            if(GUILayout.Button("搜集地图数据"))
            {
                LevelMapData md = new LevelMapData();
                md.levelID = mapMaker.levelID;
                md.gridStateList = new List<GridPoint.GridSate>();
                foreach (GridPoint gp in allGrid)
                {
                    if(gp.gridState.isTowerPoint)
                    {
                        md.gridStateList.Add(gp.gridState);
                    }
                }
                //如果是新关卡 则添加
                if(md.levelID>mapDataList.Count)
                {
                    mapDataList.Add(md);
                }
                mapDataList[md.levelID - 1] = md;
            }
            if(GUILayout.Button("保存当前地图"))
            {
                LevelMapDataMgr levelMapDataMgr = ScriptableObject.CreateInstance<LevelMapDataMgr>();
                levelMapDataMgr.leveMapDataList = mapMaker.mapDataList;
                UnityEditor.AssetDatabase.CreateAsset(levelMapDataMgr, "Assets/Resources/AssetData/LevelMapData.asset");
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

                foreach(GridPoint.GridSate gs in mapDataList[mapMaker.levelID-1].gridStateList)
                {
                    allGrid[gs.id].gridState = gs;
                    allGrid[gs.id].UpdateGrid();
                }
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
