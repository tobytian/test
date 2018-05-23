using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var an = new Another();
        var type = an.GetType();
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                      BindingFlags.Static);
        Debug.Log("numb" + methods.Length);
        MethodInfo mi = null;
        for (int i = 0; i < methods.Length; i++)
        {
            if (methods[i].Name == "TestMethod")
            {
                mi = methods[i];
            }
        }
        Debug.Log(mi);
        object[] o = new object[2];
        o[0] = 2;
        o[1] = "hello";
        mi.Invoke(an, o);


        //var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        //    .Where(m => m.GetCustomAttributes(typeof(RPCAttribute), true).Length > 0).ToArray();
        //for (int i = 0; i < methods.Length; i++)
        //{
        //    var name = methods[i].Name;
        //    if (rpcHandleMethodInfos.ContainsKey(name))
        //    {
        //        throw new ArgumentException(string.Format("Alread inject this method[{0}]", name));
        //    }
        //    rpcHandleMethodInfos.Add(name, methods[i]);
        //}
    }
}

public class Another
{
    void TestMethod(int x, string str)
    {
        Debug.Log(x);
        Debug.Log(str);
    }
}