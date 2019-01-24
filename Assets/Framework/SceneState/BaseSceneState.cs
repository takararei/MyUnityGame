using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Framework.SceneState
{
    public class BaseSceneState : IBaseSceneState
    {
        public virtual void EnterScene()
        {
            
        }

        public virtual void ExitScene()
        {
            UIManager.Instance.uiFacade.ClearPanelDict();
            if (SceneManager.GetActiveScene().buildIndex + 1
                <
                SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("当前场景"+"已是最后的场景");
            }
            
        }
    }
}

