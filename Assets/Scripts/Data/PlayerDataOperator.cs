using Assets.Framework;
using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class PlayerDataOperator:Singleton<PlayerDataOperator>
{
    public PlayerData playerData;///<玩家对象

    private string path;///<文件的路径

    public override void Init()
    {
        //base.Init();
        playerData = LoadPlayerData();
        GameRoot.Instance.data = playerData;
    }
    public PlayerDataOperator()
    {
        SetPath();
    }
    public PlayerData LoadPlayerData()
    {
        //如果路径上有文件，就读取文件
        if (File.Exists(path))
        {
            //读取数据
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
        }
        //如果没有文件，就new出一个PlayerData
        else
        {
            playerData = new PlayerData();
        }

        return playerData;
    }

    //保存玩家的数据
    public void SavePlayerData()
    {
        //保存数据      
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        FileStream file = File.Create(path);
        bf.Serialize(file, playerData);
        file.Close();

    }

    //设置文件的路径，在手机上运行时Application.persistentDataPath这个路径才是可以读写的路径
    void SetPath()
    {
        //安卓平台
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath + "/playerData.gd";
        }
        //windows编辑器
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Application.streamingAssetsPath + "/playerData.gd";
        }
    }
}