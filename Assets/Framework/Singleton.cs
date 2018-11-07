using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework
{
    public class Singleton<T> where T:class,new()
    {
        private static T _instance;

        // 定义一个标识确保线程同步
        private static readonly object syslock = new object();
       
        public static T GetInstance()
        {
            if(_instance==null)
            {
                lock(syslock)
                {
                    if(_instance==null)
                    {
                        _instance = new T();
                    }
                }
            }

            return _instance;
        }
    }
}
