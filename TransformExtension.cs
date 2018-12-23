using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static void Show(this Transform transform)
    {
        transform.gameObject.SetActive(true);
    }

    public static void Hide(this Transform transform)
    {
        transform.gameObject.SetActive(false);
    }

    public static void AddChild(this Transform parent, Transform child)
    {
        child.SetParent(parent);
    }

    #region SetLocalPosition

    public static void SetLocalPosX(this Transform transform, float x)
    {
        var localPos = transform.localPosition;
        localPos.x = x;
        transform.localPosition = localPos;
    }

    public static void SetLocalPosY(this Transform transform, float y)
    {
        var localPos = transform.localPosition;
        localPos.y = y;
        transform.localPosition = localPos;
    }

    public static void SetLocalPosZ(this Transform transform, float z)
    {
        var localPos = transform.localPosition;
        localPos.z = z;
        transform.localPosition = localPos;
    }

    public static void SetLocalPosXY(this Transform transform, float x, float y)
    {
        var localPos = transform.localPosition;
        localPos.x = x;
        localPos.y = y;
        transform.localPosition = localPos;
    }
    public static void SetLocalPosXZ(this Transform transform, float x, float z)
    {
        var localPos = transform.localPosition;
        localPos.x = x;
        localPos.z = z;
        transform.localPosition = localPos;
    }
    public static void SetLocalPosYZ(this Transform transform, float y, float z)
    {
        var localPos = transform.localPosition;
        localPos.y = y;
        localPos.z = z;
        transform.localPosition = localPos;
    }
    #endregion

    public static void ResetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.one;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

}
