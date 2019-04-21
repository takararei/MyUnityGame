using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlayerData : MonoBehaviour
{
    public PlayerData data;
    PlayerDataOperator po;
    // Use this for initialization
    void Start()
    {
        //data = new PlayerData();
        po = new PlayerDataOperator();
        data = po.LoadPlayerData();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            po.playerData = data;
            po.SavePlayerData();
            Debug.Log("保存");
        }
    }
}
