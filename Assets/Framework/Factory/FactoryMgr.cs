using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Framework.Singleton;
using UnityEngine;

namespace Assets.Framework.Factory
{
    //public enum FactoryType
    //{
    //    UIFactory,
    //    GameFactory,
    //}

    public class FactoryMgr:Singleton<FactoryMgr>
    {
        //public Dictionary<FactoryType, IBaseFactory> factoryDict = new Dictionary<FactoryType, IBaseFactory>();
        private IBaseFactory uiFactory;
        private IBaseFactory gameFactory;
        private AudioClipFactory auidoClipFactory;
        private SpriteFactory spriteFactory;
        private RuntimeAnimatorControllerFactory runtimeAnimatorFactory;
        private ScriptableObjectFactory scriptableObjectFactory;
        
        public override void Init()
        {
            //base.Init();
            //factoryDict.Add(FactoryType.UIPanelFactory, new UIPanelFactory());
            //factoryDict.Add(FactoryType.UIFactory, new UIFactory());
            //factoryDict.Add(FactoryType.GameFactory, new GameFactory());
            uiFactory = new UIFactory();
            gameFactory = new GameFactory();
            auidoClipFactory = new AudioClipFactory();
            spriteFactory = new SpriteFactory();
            runtimeAnimatorFactory = new RuntimeAnimatorControllerFactory();
            scriptableObjectFactory = new ScriptableObjectFactory();
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

        public T GetData<T>(string resourcePath) where T:ScriptableObject
        {
            return scriptableObjectFactory.GetDataResource<T>(resourcePath);
        }
        
        public GameObject GetUI(string resourcePath)
        {
            return uiFactory.GetItem(resourcePath);
            //return factoryDict[FactoryType.UIFactory].GetItem(resourcePath);
        }

        public GameObject GetGame(string resourcePath)
        {
            return gameFactory.GetItem(resourcePath);
            //return factoryDict[FactoryType.GameFactory].GetItem(resourcePath);
        }

        public void PushUI(string itemName, GameObject itemGo)
        {
            uiFactory.PushItem(itemName, itemGo);
            //factoryDict[FactoryType.UIFactory].PushItem(itemName, itemGo);
        }

        public void PushGame(string itemName, GameObject itemGo)
        {
            gameFactory.PushItem(itemName, itemGo);
            //factoryDict[FactoryType.GameFactory].PushItem(itemName, itemGo);
        }

    }
}
