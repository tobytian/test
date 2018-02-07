//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections.Generic;
using HiFramework;
using UnityEditor;
using UnityEngine;

public class ExportCollider
{
    private static string _file = "ExportCollider/1002.json";

    private static IIO _io;
    private static Collider[] _allCollider;
    private static List<CapsuleCollider> _capsuleCollider = new List<CapsuleCollider>();
    private static float _clientToServer = 100;
    private static int _id = 1;
    [MenuItem("Export/Export", false, 0)]
    private static void Export()
    {
        var objs = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        if (objs.Length != 1)
        {
            Debug.LogError("select error");
            return;
        }
        var go = objs[0] as GameObject;
        Framework.Init();
        _io = Center.Get<IOComponent>();
        _allCollider = go.transform.GetComponentsInChildren<Collider>();
        DebugType();
        Debug.Log("------------ export start");
        WriteHead();
        ProcessCapsuleCollider();
        WriteEnd();
        AssetDatabase.Refresh();
        Debug.Log("------------ export success");
    }



    static void DebugType()
    {
        List<string> _types = new List<string>();
        foreach (var VARIABLE in _allCollider)
        {
            var type = VARIABLE.GetType();
            if (!_types.Contains(type.ToString()))
                _types.Add(type.ToString());
        }
        Debug.Log("total collider count: " + _allCollider + " total type count: " + _types.Count);
        foreach (var VARIABLE in _types)
        {
            Debug.Log("collider type: " + VARIABLE);
        }
    }


    static void ProcessCapsuleCollider()
    {
        foreach (var VARIABLE in _allCollider)
        {
            if (VARIABLE is CapsuleCollider)
                _capsuleCollider.Add(VARIABLE as CapsuleCollider);
        }
        Debug.Log("capsule collider count: " + _capsuleCollider.Count);

        for (int i = 0; i < _capsuleCollider.Count; i++)
        {
            var id = i + 1;
            HiFloat px = new HiFloat(_capsuleCollider[i].gameObject.transform.position.x * _clientToServer);
            px /= _clientToServer;
            HiFloat py = new HiFloat(_capsuleCollider[i].gameObject.transform.position.z * _clientToServer);
            py /= _clientToServer;
            HiFloat radius = new HiFloat(_capsuleCollider[i].radius);
            Debug.Log("id: " + id);
            Debug.Log("px" + px);
            Debug.Log("py" + py);
            Debug.Log("radius" + radius);
            string pxS = px.ToString();
            if (px % 1 == 0)
                pxS += ".0";
            string pyS = py.ToString();
            if (py % 1 == 0)
                pyS += ".0";

            string context = "{" + "\n"
                             + "\"" + "id" + "\"" + ":" + _id + "," + "\n"
                             + "\"" + "type" + "\"" + ":" + 1 + "," + "\n"
                             + "\"" + "layer" + "\"" + ":" + 1 + "," + "\n"
                             + "\"" + "description" + "\"" + ":" + "\"" + "dashu (39)" + "\"" + "," + "\n"
                             + "\"" + "properties" + "\"" + ":" + "{}" + "," + "\n"
                             + "\"" + "px" + "\"" + ":" + pxS + "," + "\n"
                             + "\"" + "py" + "\"" + ":" + pyS + "," + "\n"
                             + "\"" + "radius" + "\"" + ":" + radius + "," + "\n"
                             + "\"" + "scale" + "\"" + ":" + 1 + "," + "\n"
                             + "\"" + "angle" + "\"" + ":" + 0 + "\n"
                             + "}," + "\n";
            if (i == _capsuleCollider.Count - 1)
            {
                context = "{" + "\n"
                          + "\"" + "id" + "\"" + ":" + _id + "," + "\n"
                          + "\"" + "type" + "\"" + ":" + 1 + "," + "\n"
                          + "\"" + "layer" + "\"" + ":" + 1 + "," + "\n"
                          + "\"" + "description" + "\"" + ":" + "\"" + "dashu (39)" + "\"" + "," + "\n"
                          + "\"" + "properties" + "\"" + ":" + "{}" + "," + "\n"
                          + "\"" + "px" + "\"" + ":" + pxS + "," + "\n"
                          + "\"" + "py" + "\"" + ":" + pyS + "," + "\n"
                          + "\"" + "radius" + "\"" + ":" + radius + "," + "\n"
                          + "\"" + "scale" + "\"" + ":" + 1 + "," + "\n"
                          + "\"" + "angle" + "\"" + ":" + 0 + "\n"
                          + "}" + "\n";
            }
            var bytes = System.Text.Encoding.UTF8.GetBytes(context);
            _io.WriteFile(Application.dataPath + "/" + _file, bytes);
        }
    }

    static void WriteHead()
    {
        if (_io.IsFileExist(Application.dataPath + "/" + _file))
            _io.DeleteFile(Application.dataPath + "/" + _file);
        string head = "{\n"
                      + "\"" + "title" + "\"" + ":" + "\"" + "1002" + "\"" + "," + "\n"
                      + "\"" + "size" + "\"" + ":" + "20" + "," + "\n"
                      + "\"" + "nodes" + "\"" + ":" + "[" + "\n";
        var bytes = System.Text.Encoding.UTF8.GetBytes(head);
        _io.WriteFile(Application.dataPath + "/" + _file, bytes);
    }

    static void WriteEnd()
    {
        string end = "]" + "\n"
                     + "}";
        var bytes = System.Text.Encoding.UTF8.GetBytes(end);
        _io.WriteFile(Application.dataPath + "/" + _file, bytes);
    }
}