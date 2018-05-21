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
        var type = this.GetType();
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
        object[] o = new object[1];
        o[0] = 2;
        mi.Invoke(this, o);


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

    // Update is called once per frame
    void Update()
    {
        int[] x = new[] { 2 };
    }


    void TestMethod(int x)
    {
        Debug.Log(x);
    }
}
