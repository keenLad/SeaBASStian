using UnityEngine;
using System.Linq;

public static class TransformHelper
{
    public static void ClearChilds(this Transform parent)
    {
        var childs = parent.Cast<Transform>().Select(t => t.gameObject);

        foreach(var child in childs)
        {
            Object.Destroy(child);
        }
    }
}
