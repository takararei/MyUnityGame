using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Example
{
    [Serializable]
    public class TestDataObject
    {
        public int id;
        public string name;
        public string path;
        public string layer;
        public List<int> test;
    }
}
