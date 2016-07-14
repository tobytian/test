//*********************************************************************
// Description:使用流程
// 1.生成lightingmap
// 2.在场景中点击将要制作prefab的物体
// 3.点击菜单栏Lightmap->Save
// 4.完成
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using UnityEditor;

public class PrefabLightmapDataEditor : Editor
{
    [MenuItem("Lightmap/Save", false, 0)]
    static void SaveLightmapInfo()
    {
        GameObject go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.LogError("Please select a object");
            return;
        }
        PrefabLightmapData data = go.GetComponent<PrefabLightmapData>();
        if (data == null)
        {
            data = go.AddComponent<PrefabLightmapData>();
        }
        data.SaveLightmap();
        EditorUtility.SetDirty(go);
    }

    [MenuItem("Lightmap/Load", false, 0)]
    static void LoadLightmapInfo()
    {
        GameObject go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.LogError("Please select a object");
            return;
        }
        PrefabLightmapData data = go.GetComponent<PrefabLightmapData>();
        if (data == null)
        {
            Debug.LogError("the object you selected have no lighting info");
            return;
        }
        data.LoadLightmap();
        EditorUtility.SetDirty(go);
    }

    [MenuItem("Lightmap/Clear", false, 0)]
    static void ClearLightmapInfo()
    {
        GameObject go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.LogError("Please select a object");
            return;
        }
        PrefabLightmapData data = go.GetComponent<PrefabLightmapData>();
        if (data == null)
        {
            Debug.LogError("the object you selected have no lighting info");
            return;
        }
        data.m_RendererInfo.Clear();
        EditorUtility.SetDirty(go);
    }
}