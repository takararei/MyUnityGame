using Assets.Framework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class UIManager:Singleton<UIManager>
    {
        private const string UIRootName="Canvas";

        /// <summary>
        /// 面板实例的位置
        /// </summary>
        private Transform _canvasTransform;

        public Transform CanvasTransform
        {
            get
            {
                if(_canvasTransform==null)
                {
                    _canvasTransform = GameObject.Find(UIRootName).transform;
                }
                return _canvasTransform;
            }
        }

        /// <summary>
        /// 存储所有面板 Prefab 路径
        /// </summary>
        private Dictionary<string, string> panelPathDict;

        /// <summary>
        /// 保存所有被实例化的BasePanel组件,BasePanel
        /// </summary>
        private Dictionary<string, BasePanel> panelDict;

        /// <summary>
        /// 管理所有显示的面板 用栈
        /// </summary>
        private Stack<BasePanel> panelStack;


        /// <summary>
        /// 根据面板类型得到实例化面板
        /// </summary>
        private BasePanel GetPanel(string panelName)
        {
            if (panelDict == null)
            {
                panelDict = new Dictionary<string, BasePanel>();
            }
            
            BasePanel panel = panelDict.TryGet(panelName);

            if (panel == null)
            {
                //如果找不到 就实例
                string path = panelPathDict.TryGet(panelName);
                GameObject instPanel=GameObject.Instantiate(Resources.Load(path)) as GameObject;

                instPanel.transform.SetParent(CanvasTransform,false);//TODO
                panelDict.Add(panelName, instPanel.GetComponent<BasePanel>());
                return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }

        }

        /// <summary>
        /// JSON解析对象
        /// </summary>
        [Serializable]
        class UIPanelTypeJson
        {
            public List<UIPanelInfo> infoList;
        }

        /// <summary>
        /// 解析JSON文件
        /// </summary>
        public void ParseUIPanelTypeJson()
        {
            panelPathDict = new Dictionary<string, string>();

            TextAsset ta= Resources.Load<TextAsset>("UIPanelType");

            UIPanelTypeJson jsonObject =JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

            foreach(UIPanelInfo info in jsonObject.infoList)
            {
                panelPathDict.Add(info.panelName, info.path);
            }
        }

        public void PushPanel(string panelName)
        {
            if(panelStack==null)
            {
                panelStack = new Stack<BasePanel>();
            }
            BasePanel panel = GetPanel(panelName);
            panelStack.Push(panel);
        }

        public void PopPanel(string panelName)
        {

        }
    }
}
