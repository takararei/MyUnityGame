using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.Excel.Test
{
    [Serializable]
    public class TestDataManager : ScriptableObject
    {
        public List<TestDataObject> TestDataList;

        public void Test()
        {
            Debug.Log(TestDataList[0].name);
        }
    }
}
