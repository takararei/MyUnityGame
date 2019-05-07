using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.Factory
{
    public class ScriptableObjectFactory : BaseResourceFactory<ScriptableObject>
    {
        public ScriptableObjectFactory()
        {
            LoadPath = "AssetData/";
        }

        public T GetDataResource<T>(string resourcePath) where T:ScriptableObject
        {
            T item = null;
            item = (T)GetResource(resourcePath);
            return item;
        }

    }

}
