using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelButonMaker))]
public class LevelButtonTool : Editor
{
    private LevelButonMaker maker;
    private List<Vector3> posList;
    private LevelInfoMgr lvInfoMgr;
    private void Awake()
    {
        maker = LevelButonMaker.Instance;
        posList = new List<Vector3>();
        lvInfoMgr = LevelInfoMgr.Instance;
    }

    public void Init()
    {
        int num = maker.parent.childCount;
        for (int i = 0; i < num; i++)
        {
            DestroyImmediate(maker.parent.GetChild(i).gameObject);
        }
        maker.posList = null;
        posList.Clear();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (Application.isPlaying)
        {
            
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("读取"))
            {
                Init();   
                int count = lvInfoMgr.levelInfoList.Count;
                for(int i=0;i<count;i++)
                {
                    Vector3 pos = lvInfoMgr.levelInfoList[i].levelPos;
                    if ( pos == Vector3.zero)
                        continue;
                    else
                    {
                        GameObject go=GameObject.Instantiate(maker.levelGO, maker.parent);
                        go.transform.localPosition = pos;
                    }
                }
            }
            if (GUILayout.Button("搜集"))
            {
                maker.posList = null;
                posList.Clear();

                int num = maker.parent.childCount;
                for (int i = 0; i < num; i++)
                {
                    posList.Add(maker.parent.GetChild(i).localPosition);
                }
                maker.posList = posList;
            }

            if(GUILayout.Button("重置"))
            {
                Init();
            }

            if(GUILayout.Button("保存"))
            {
                if (posList.Count != lvInfoMgr.levelInfoList.Count)
                    Debug.LogError("数目不对 toolcount "+posList.Count+", dataCount "+lvInfoMgr.levelInfoList.Count);
                else
                {
                    for (int i = 0; i < posList.Count; i++)
                    {
                        if(lvInfoMgr.levelInfoList[i].levelPos!=posList[i])
                        {
                            lvInfoMgr.levelInfoList[i].levelPos = posList[i];
                        }
                    }
                    LevelInfoMgr infoMgr = ScriptableObject.CreateInstance<LevelInfoMgr>();
                    infoMgr.levelInfoList = lvInfoMgr.levelInfoList;
                    UnityEditor.AssetDatabase.CreateAsset(infoMgr, "Assets/Resources/AssetData/LevelInfo.asset");
                    UnityEditor.AssetDatabase.SaveAssets();
                    UnityEditor.AssetDatabase.Refresh();
                    Debug.Log("保存成功");
                }
                

            }

            EditorGUILayout.EndHorizontal();
        }

    }
}