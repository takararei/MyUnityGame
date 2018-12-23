using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Example
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
