using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework.Factory
{
    public class RuntimeAnimatorControllerFactory : BaseResourceFactory<RuntimeAnimatorController>
    {
        public RuntimeAnimatorControllerFactory()
        {
            LoadPath = "Animator/Animator/";
        }
    }
}