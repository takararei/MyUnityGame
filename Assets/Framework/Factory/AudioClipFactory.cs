using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework.Factory
{
    public class AudioClipFactory : BaseResourceFactory<AudioClip>
    {
        public AudioClipFactory()
        {
            LoadPath = "AudioClips/";
        }
    }

    //public class AudioClipFactory : IBaseResourceFactory<AudioClip>
    //{
    //    protected Dictionary<string, AudioClip> factoryDict = new Dictionary<string, AudioClip>();
    //    protected string LoadPath;

    //    public AudioClipFactory()
    //    {
    //        LoadPath = "AudioClips/";
    //    }

    //    public AudioClip GetResource(string resourcePath)
    //    {
    //        AudioClip itemGo = null;
    //        string itemLoadPath = LoadPath + resourcePath;
    //        if (factoryDict.ContainsKey(resourcePath))
    //        {
    //            itemGo = factoryDict[resourcePath];
    //        }
    //        else
    //        {
    //            itemGo = Resources.Load<AudioClip>(itemLoadPath);
    //            factoryDict.Add(resourcePath, itemGo);

    //        }

    //        if (itemGo == null)
    //        {
    //            Debug.Log(resourcePath + "获取失败，路径有误" + itemLoadPath);
    //        }

    //        return itemGo;
    //    }
    //}
}