using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Framework.Singleton;
using UnityEngine;

namespace Assets.Framework.Factory
{
    public enum FactoryType
    {
        UIPanelFactory,
        UIFactory,
        GameFactory,
    }

    public class FactoryManager:Singleton<FactoryManager>
    {
        public Dictionary<FactoryType, IBaseFactory> factoryDict = new Dictionary<FactoryType, IBaseFactory>();
        public AudioClipFactory auidoClipFactory;
        public SpriteFactory spriteFactory;
        public RuntimeAnimatorControllerFactory runtimeAnimatorFactory;

        public void Init()
        {
            factoryDict.Add(FactoryType.UIPanelFactory, new UIPanelFactory());
            factoryDict.Add(FactoryType.UIFactory, new UIFactory());
            factoryDict.Add(FactoryType.GameFactory, new GameFactory());
            auidoClipFactory = new AudioClipFactory();
            spriteFactory = new SpriteFactory();
            runtimeAnimatorFactory = new RuntimeAnimatorControllerFactory();
        }

        public Sprite GetSprite(string resourcePath)
        {
            return spriteFactory.GetResource(resourcePath);
        }

        public AudioClip GetAudioClip(string resourcePath)
        {
            return auidoClipFactory.GetResource(resourcePath);
        }

        public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
        {
            return runtimeAnimatorFactory.GetResource(resourcePath);
        }

        public GameObject GetGameObjectResource(FactoryType factoryType, string resourcePath)
        {
            return factoryDict[factoryType].GetItem(resourcePath);
        }

        public void PushGameObjectToFactory(FactoryType factoryType, string itemName, GameObject itemGo)
        {
            factoryDict[factoryType].PushItem(itemName, itemGo);
        }

        public GameObject GetUIPanel(string resourcePath)
        {
            return factoryDict[FactoryType.UIPanelFactory].GetItem(resourcePath);
        }

        public GameObject GetUI(string resourcePath)
        {
            return factoryDict[FactoryType.UIFactory].GetItem(resourcePath);
        }

        public GameObject GetGame(string resourcePath)
        {
            return factoryDict[FactoryType.GameFactory].GetItem(resourcePath);
        }

        public void PushUIPanel(string itemName, GameObject itemGo)
        {
            factoryDict[FactoryType.UIPanelFactory].PushItem(itemName, itemGo);
        }

        public void PushUI(string itemName, GameObject itemGo)
        {
            factoryDict[FactoryType.UIFactory].PushItem(itemName, itemGo);
        }

        public void PushGame(string itemName, GameObject itemGo)
        {
            factoryDict[FactoryType.GameFactory].PushItem(itemName, itemGo);
        }

    }
}
