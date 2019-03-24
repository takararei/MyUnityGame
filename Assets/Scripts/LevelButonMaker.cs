using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// LevelInfo中 levelPos数据配置，后期可以移除
/// </summary>
public class LevelButonMaker : MonoBehaviour, IPointerClickHandler {

    public GameObject levelGO;
    [HideInInspector]
    public Transform parent;
    public List<Vector3> posList;
    private static LevelButonMaker _instance;
    public static LevelButonMaker Instance
    {
        get
        {
            return _instance;
        }
    }
       
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Instantiate(levelGO, eventData.position, Quaternion.identity, parent);
    }

    private void Awake()
    {
        _instance = this;
        parent = this.transform;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


