using Assets.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class BaseUIListItem:IBaseUIListItem
    {
        protected GameObject root;
        protected int ID;

        protected T Find<T>(string uiName)
        {
            return UITool.FindChild<T>(root, uiName);
        }

        public virtual void Clear()
        {

        }

        public int GetID()
        {
            return ID;
        }

        public void SetID(int id)
        {
            ID = id;
        }
    }
}
