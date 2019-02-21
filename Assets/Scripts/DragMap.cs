using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Framework.Extension;
public class DragMap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    float dragPosXoffset;
    float dragLastPosX;
    //float dragEndPosX;

    public float MapPosXLeft = -960;
    float MapPosXRight = -1356.5f;

    private void Awake()
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragLastPosX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragPosXoffset = dragLastPosX - eventData.position.x;
        float localPosX = transform.localPosition.x - dragPosXoffset;
        if (localPosX < MapPosXLeft && localPosX > MapPosXRight)
        {
            transform.SetLocalPosX(localPosX);
        }
        
        dragLastPosX = eventData.position.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
