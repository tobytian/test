////****************************************************************************
//// Description:
//// Author: hiramtan@live.com
////****************************************************************************

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class ExportCollision : MonoBehaviour
//{
//    private IIO _io;
//    private Collider[] _allCollider;
//    private List<CapsuleCollider> _capsuleCollider = new List<CapsuleCollider>();

//    private float _clientToServer = 100;

//    private string _file = "1003.json";

//    private int _id = 1;
//    // Use this for initialization
//    void Start()
//    {
//        Framework.Init();
//        _io = Center.Get<IOComponent>();
//        _allCollider = transform.GetComponentsInChildren<Collider>();
//        DebugType();

//        WriteHead();
//        ProcessCapsuleCollider();


//        WriteEnd();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    void DebugType()
//    {
//        List<string> _types = new List<string>();
//        foreach (var VARIABLE in _allCollider)
//        {
//            var type = VARIABLE.GetType();
//            if (!_types.Contains(type.ToString()))
//                _types.Add(type.ToString());
//        }
//        Debug.LogError("total collider count: " + _types.Count);
//        foreach (var VARIABLE in _types)
//        {
//            Debug.Log("map contain this type collider: " + VARIABLE);
//        }
//    }


//    void ProcessCapsuleCollider()
//    {
//        foreach (var VARIABLE in _allCollider)
//        {
//            if (VARIABLE is CapsuleCollider)
//                _capsuleCollider.Add(VARIABLE as CapsuleCollider);
//        }
//        Debug.LogError("capsule collider count: " + _capsuleCollider.Count);

//        for (int i = 0; i < _capsuleCollider.Count; i++)
//        {
//            var id = i + 1;
//            var px = _capsuleCollider[i].center.x * _clientToServer;
//            var py = _capsuleCollider[i].center.z * _clientToServer;
//            var radius = _capsuleCollider[i].radius;
//            Debug.Log("id: " + id);
//            Debug.Log("px" + px);
//            Debug.Log("py" + py);
//            Debug.Log("radius" + radius);

//            string context = "{" + "\n"
//                             + "\"" + "id" + "\"" + ":" + _id + "," + "\n"
//                             + "\"" + "type" + "\"" + ":" + 1 + "," + "\n"
//                             + "\"" + "layer" + "\"" + ":" + 1 + "," + "\n"
//                             + "\"" + "description" + "\"" + ":" + "\"" + "dashu (39)" + "\"" + "," + "\n"
//                             + "\"" + "properties" + "\"" + ":" + "{}" + "," + "\n"
//                             + "\"" + "px" + "\"" + ":" + px + "," + "\n"
//                             + "\"" + "px" + "\"" + ":" + py + "," + "\n"
//                             + "\"" + "radius" + "\"" + ":" + radius + "," + "\n"
//                             + "\"" + "scale" + "\"" + ":" + 1 + "," + "\n"
//                             + "\"" + "angle" + "\"" + ":" + 0 + "\n"
//                             + "}," + "\n";
//            if (i == _capsuleCollider.Count - 1)
//            {
//                context = "{" + "\n"
//                                + "\"" + "id" + "\"" + ":" + _id + "," + "\n"
//                                + "\"" + "type" + "\"" + ":" + 1 + "," + "\n"
//                                + "\"" + "layer" + "\"" + ":" + 1 + "," + "\n"
//                                + "\"" + "description" + "\"" + ":" + "\"" + "dashu (39)" + "\"" + "," + "\n"
//                                + "\"" + "properties" + "\"" + ":" + "{}" + "," + "\n"
//                                + "\"" + "px" + "\"" + ":" + px + "," + "\n"
//                                + "\"" + "px" + "\"" + ":" + py + "," + "\n"
//                                + "\"" + "radius" + "\"" + ":" + radius + "," + "\n"
//                                + "\"" + "scale" + "\"" + ":" + 1 + "," + "\n"
//                                + "\"" + "angle" + "\"" + ":" + 0 + "\n"
//                                + "}" + "\n";
//            }
//            var bytes = System.Text.Encoding.UTF8.GetBytes(context);
//            _io.WriteFile(Application.dataPath + "/" + _file, bytes);
//        }
//    }

//    void WriteHead()
//    {
//        if (_io.IsFileExist(Application.dataPath + "/" + _file))
//            _io.DeleteFile(Application.dataPath + "/" + _file);
//        string head = "{\n"
//                      + "\"" + "title" + "\"" + ":" + "\"" + "1002" + "\"" + "," + "\n"
//                      + "\"" + "size" + "\"" + ":" + "20" + "," + "\n"
//                      + "\"" + "nodes" + "\"" + ":" + "[" + "\n";
//        Debug.LogError(head);
//        var bytes = System.Text.Encoding.UTF8.GetBytes(head);
//        _io.WriteFile(Application.dataPath + "/" + _file, bytes);
//    }

//    void WriteEnd()
//    {
//        string end = "]" + "\n"
//                     + "}";
//        var bytes = System.Text.Encoding.UTF8.GetBytes(end);
//        _io.WriteFile(Application.dataPath + "/" + _file, bytes);
//    }
//}
