
using Assets.Framework.Singleton;
using Assets.Framework.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Framework.SceneState
{
    public class SceneStateManager:Singleton<SceneStateManager>
    {

        public IBaseSceneState lastSceneState;
        public IBaseSceneState currentSceneState;

        //遮罩相关
        private GameObject mask;
        private Image maskImage;
        private float maskTime = 1.5f;

        public override void Init()
        {
            base.Init();
            InitMask();
            currentSceneState = new StartLoadSceneState();
            currentSceneState.EnterScene();
        }

        private void InitMask()
        {
            mask = UIManager.Instance.uiFacade.CreateUIAndSetUIPosition("Img_Mask");
            maskImage = mask.GetComponent<Image>();
        }

        private void ShowMask()
        {
            mask.transform.SetSiblingIndex(10);
            Tween t =
                DOTween.To(() => maskImage.color,
                toColor => maskImage.color = toColor,
                new Color(0, 0, 0, 1),
                maskTime);
            t.OnComplete(ExitSceneComplete);
        }

        private void HideMask()
        {
            DOTween.To(() => maskImage.color,
                toColor => maskImage.color = toColor,
                new Color(0, 0, 0, 0),
                maskTime);
        }

        public void ChangeSceneState(IBaseSceneState baseSceneState)
        {
            lastSceneState = currentSceneState;
            ShowMask();
            currentSceneState = baseSceneState;
        }

        private void ExitSceneComplete()
        {
            lastSceneState.ExitScene();
            currentSceneState.EnterScene();
            HideMask();
        }

    }
}
