//****************************************************************************
// Description:
// Author: hiramtan@qq.com
//***************************************************************************

using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using Debug = UnityEngine.Debug;
using HiA;
public class Instance : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var t = typeof(Test);

        Debug.Log(t);

        var tt = Activator.CreateInstance(t);
        var ttt = tt as Test;
        ttt.L();



        Debug.LogError( typeof(Test).FullName);
    }
}

namespace HiA
{
    public class Test
    {
        public void L()
        {
            Debug.Log("execute");
        }
    }


}
